using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MaterialDesignThemes.Wpf;
using PacChat.MVC;
using PacChat.Network;
using PacChat.Network.Packets.AfterLoginRequest;
using PacChat.Resources.CustomControls.Dialogs;
using PacChat.Utils;

namespace PacChat.Windows.Login
{
    public class LoginView : View<LoginApp>
    {
        // Login Data
        private string lgUsername;
        public string LgUserName { get => lgUsername; set { lgUsername = value; OnPropertyChanged(); } }
        private bool lgRemember;
        public bool LgRemember { get => lgRemember; set { lgRemember = value; OnPropertyChanged(); } }

        public ICommand LoginCommand { get; set; }

        // Register Data
        private string regFirstName;
        public string RegFirstName { get => regFirstName; set { regFirstName = value; OnPropertyChanged(); } }
        private string regLastName;
        public string RegLastName { get => regLastName; set { regLastName = value; OnPropertyChanged(); } }
        private string regUserName;
        public string RegUserName { get => regUserName; set { regUserName = value; OnPropertyChanged(); } }
        private DateTime regDoB = DateTime.Now.AddYears(-13);
        public DateTime RegDoB { get => regDoB; set { regDoB = value; OnPropertyChanged(); } }
        private Gender regGender;
        public Gender RegGender { get => regGender; set { regGender = value; OnPropertyChanged(); } }
        private bool regToUAgrement;
        public bool RegToUAgrement { get => regToUAgrement; set { regToUAgrement = value; OnPropertyChanged(); } }

        public ICommand RegisterCommand { get; set; }

        public Action CloseAction { get; set; }
        public Action<int> MoveToTab { get; set; }

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
            DialogHost.Show(new WaitingDialog());
            app.controller.OnLogin(LgUserName, wnd.LgPassword.Password, LgRemember);
        }

        public void EnterMainWindow()
        {
            MainWindow main = new MainWindow();
            Application.Current.MainWindow = main;
            main.Show();
            CloseAction();
        }

        public void LoginResponse(int code)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            if (code == 200)
            {
                EnterMainWindow();
            }
            else
            {
                AnnouncementDialog dialog = null;
                switch (code)
                {
                    case 404:
                        dialog = new AnnouncementDialog("Invalid username or password", "Check your info and try again");
                        break;
                    case 401:
                        dialog = new AnnouncementDialog("Invalid username or password", "Check your info and try again");
                        break;
                    case 403:
                        dialog = new AnnouncementDialog("Your account got banned", "Please contact the admin to get more information");
                        break;
                }
                DialogHost.Show(dialog);
            }
        }

        public bool CanRegister(LoginWindow wnd)
        {
            if (RegUserName == null || RegUserName.Trim().Length < 1) return false;
            if (wnd.RegPassword == null || wnd.RegPassword.Password.Trim().Length < 1) return false;
            if (RegFirstName == null || RegFirstName.Trim().Length < 1) return false;
            if (RegLastName == null || RegLastName.Trim().Length < 1) return false;
            return !(RegGender == Gender.None || !RegToUAgrement);
        }

        public void Register(LoginWindow wnd)
        {
            // Process UI
            Console.WriteLine("OnRegister in view");
            // Then notify to controller
            DialogHost.Show(new WaitingDialog());
            app.controller.OnRegister(wnd.RegPassword.Password);
        }

        public async void RegisterResponse(int code)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
            if (code == 200)
            {
                _ = await DialogHost.Show(new AnnouncementDialog("Your account has been activated successfully", "You can now login."));
                MoveToTab(0);
            }
            else
            {
                AnnouncementDialog dialog = null;
                switch (code)
                {
                    case 404:
                        dialog = new AnnouncementDialog("Unknow error", "Please contact administrator for support", "Error Code: #RGDBE1");
                        break;
                    case 409:
                        dialog = new AnnouncementDialog("Your email early exist in PacChat system");
                        break;
                }
                _ = await DialogHost.Show(dialog);
            }
        }
    }
}
