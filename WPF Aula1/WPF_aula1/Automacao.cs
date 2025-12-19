using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_aula1
{
    internal class Automacao
    {
        public ChromeDriver Driver {  get; set; }
        private readonly Action<BecrowdEntity> _acao;
        
        public Automacao() { }

        public Automacao(Action<BecrowdEntity> acao = null)
        {
            _acao = acao;
        }

        public void EspereCarregar(ChromeDriver driver)
        {
            Thread.Sleep(3000);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(300));
            wait.Until(wd => driver.ExecuteScript("return document.readyState").ToString() == "complete");
        }

        public void RolarTela(ChromeDriver driver, int pixels)
        {
            if (driver is IJavaScriptExecutor js)
            {
                string script = $"window.scrollBy(0, {pixels});";
                js.ExecuteScript(script);

            }
        }

        public void Enviar(BecrowdEntity e)
        {
            _acao?.Invoke(e);
        }

        public ChromeDriver Iniciar(string email, string senha)
        {
            ChromeOptions options = new ChromeOptions();//configurações antes de abrir o navegador
            options.AddArgument("--start-maximized"); //maximiza o navegador
            //options.AddArgument("--headless=new"); // opcional: se quiser o navegador sem interface
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true; //mantem terminal oculto

            ChromeDriver driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("https://judge.beecrowd.com/pt/login");
            driver.FindElement(By.Id("email")).Clear();
            driver.FindElement(By.Id("email")).SendKeys(email);
            driver.FindElement(By.Id("password")).Clear();
            driver.FindElement(By.Id("password")).SendKeys(senha);
            driver.FindElement(By.Id("submit-btn")).Click();
            EspereCarregar(driver);
            driver.FindElement(By.XPath("//*[@id=\"menu\"]/li[8]/a")).Click();
            EspereCarregar(driver);
            driver.FindElement(By.XPath("//*[@id=\"category-list\"]/ul/li[1]")).Click();
            EspereCarregar(driver);

            return Driver = driver;
        }

        public void ExercicioFeito()
        {
            var listaExercicios = Driver.FindElements(By.XPath("//table/tbody/tr"));
            int tr = 1;
            foreach (var linha in listaExercicios)
            {
                var check = linha.FindElements(By.XPath(".//img[(contains(@src, 'solved') or contains(@src, 'tick')) and not(contains(@src, 'not'))]"));

                if (check.Count > 0)
                {
                    string idExe = linha.FindElement(By.XPath($"//tr[{tr}]/td[1]/a")).Text.Trim();
                    BecrowdEntity entidade = new BecrowdEntity 
                    {
                        Id = idExe,
                        Resolvido = true
                    };
                    Enviar(entidade);
                    RolarTela(Driver, 10);
                    tr++;
                }
                else
                {
                    string idExe = linha.FindElement(By.XPath($"//tr[{tr}]/td[1]/a")).Text.Trim();
                    BecrowdEntity entidade = new BecrowdEntity
                    {
                        Id = idExe,
                        Resolvido = false
                    };
                    Enviar(entidade);
                    RolarTela(Driver, 10);
                    tr++;
                }
            }
        }

        public void ExercicioNaoFeito()
        {
            var listaExercicios = Driver.FindElements(By.XPath("//table/tbody/tr"));
            int tr = 1;
            foreach(var linha in listaExercicios)
            {
                var check = linha.FindElements(By.XPath(".//img[(contains(@src, 'solved') or contains(@src, 'tick')) and not(contains(@src, 'not'))]"));
                if (!(check.Count > 0))
                {
                    try
                    {
                        EspereCarregar(Driver);
                        var linkExercicio = linha.FindElement(By.TagName("a"));
                        linkExercicio.Click();
                        return;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro inesperado: " + ex.Message);
                        continue;
                    }
                }
            }
        }

    }
}
