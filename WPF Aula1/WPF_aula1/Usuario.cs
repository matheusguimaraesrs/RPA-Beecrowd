using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;


namespace WPF_aula1
{
    internal class Usuario
    {
        private string _email;
        private string _senha;
        public Usuario(string email, string senha) 
        {
            if (email != null && senha != null)
            {
                email = email.Trim();
                _email = email;
                _senha = senha;
            }
        }
        public string Email 
        { 
            get { return _email; } 
        }
        public string Senha 
        { 
            get { return _senha; } 
        }
    }
}
