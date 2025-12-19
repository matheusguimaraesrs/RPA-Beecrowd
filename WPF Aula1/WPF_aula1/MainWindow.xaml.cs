using Microsoft.CSharp.RuntimeBinder;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Input;


namespace WPF_aula1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Executar(object sender, RoutedEventArgs e)
        {
            Usuario usuario = new Usuario(txt_Email.Text, txt_Senha.Password);
            Automacao automacao = new Automacao(entidade =>
            {
                Dispatcher.BeginInvoke(new Action(() =>
                {
                    txt_detalhes.AppendText(entidade + Environment.NewLine);
                    txt_detalhes.ScrollToEnd();
                }));  
            });

            automacao.Iniciar(usuario.Email, usuario.Senha);
            if ((bool)rd_consutarSomente.IsChecked)
                automacao.ExercicioFeito();
            else
            {
                automacao.ExercicioFeito();
                automacao.ExercicioNaoFeito();
            }
        }
        private void Encerrar(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
