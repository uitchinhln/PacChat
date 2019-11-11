using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PacChat.MVC;
using PacChat.Resources.CustomControls.Dialogs;

namespace PacChat.Windows.Login
{
    public class LoginView : View<LoginApp>
    {
        // Bind the xaml Controls from LoginWindow.xaml to C# and put them here, then process
        private string lgUsername;
        public string LgUserName { get => lgUsername; set { lgUsername = value; OnPropertyChanged(); } }
        private bool lgRemember;
        public bool LgRemember { get => lgRemember; set { lgRemember = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }
        public ICommand RegisterCommand { get; set; }

        LoginModel loginModel;
        LoginController loginController;
        LoginApp loginApp;

        public LoginView()
        {
            loginModel = new LoginModel();
            loginController = new LoginController();

            loginApp = new LoginApp();
            loginApp.InitializeMVC(loginModel, this, loginController);

            LoginCommand = new RelayCommand<LoginWindow>(CanLogin, Login);
            RegisterCommand = new RelayCommand<LoginWindow>(CanRegister, Register);
        }

        public bool CanLogin(LoginWindow wnd)
        {
            if (LgUserName == null || LgUserName.Trim().Length < 1)
            {
                return false;
            }
            if (wnd.LgPassword == null || wnd.LgPassword.Password.Length < 1)
            {
                return false;
            }
            return true;
        }

        public void Login(LoginWindow wnd)
        {
            // Process UI
            Console.WriteLine("OnLogin in view");
            // Then notify to controller
            app.controller.OnLogin(LgUserName, wnd.LgPassword.Password, LgRemember);
        }

        public bool CanRegister(LoginWindow wnd)
        {
            return true;
        }

        public void Register(Window wnd)
        {
            // Process UI
            Console.WriteLine("OnRegister in view");
            // Then notify to controller
            app.controller.OnRegister();
        }
    }
}
