using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using PulseSpamDesktop.Model;
using Newtonsoft.Json;
using System.Net.Http.Json;
using PulseSpamDesktop.View;
using System.Windows;
using PulseSpamDesktop.Converters;
using System.Data;
using PulseSpamDesktop.Service;

namespace PulseSpamDesktop.ViewModel
{
    public class LoginVM:BaseVM
    {
        private string _email = "lola@example.com";
        private string _password = "lola1234";
        private string _error;
        private bool _isViewVisible = true;

        private LoginService _loginService;

        public string Email
        {
            get
            {
                return _email;
            }

            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public string Error
        {
            get
            {
                return _error;
            }

            set
            {
                _error = value;
                OnPropertyChanged(nameof(Error));
            }
        }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;
            }

            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public ICommand LoginCommand { get; }

        public LoginVM()
        {
            LoginCommand = new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
            _loginService = new LoginService();
        }

        private void ExecuteLoginCommand()
        {
            UsuarioLogin u = new UsuarioLogin();
            u.Email = _email;
            u.Password = _password;
            string resultado = _loginService.ExecuteLoginCommand(u);
            if (resultado != null)
            {
                _error = resultado;
            }
        }

        private bool CanExecuteLoginCommand()
        {
            bool ok = true;

            if(string.IsNullOrWhiteSpace(Email) || !_loginService.IsEmailValid(Email) || Password == null) {
                ok = false;
            }
            return ok;
        }
    }
}
