using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;
using PacChat.Utils;
using PacChat.Network.Packets;
using PacChat.Network.Protocol;
using PacChat.Network;
using PacChat.Network.Packets.Login;
using PacChat.Network.Packets.Register;

namespace PacChat.Windows.Login
{
    public class UserLoginData
    {
        private string _name;
        private string _password;
        public bool rememberMe { get; private set; }

        public string name
        {
            get { return _name; }
            set { _name = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = checkValidPassword(value) ? value : _password; }
        }

        // This function is to check a valid password: from 6 chars, uppercase, lowercase, digits, ...
        private bool checkValidPassword(string password)
        {
            return true;
        }

        public UserLoginData(string name, string passw, bool rememberMe)
        {
            _name = name;
            _password = passw;
            this.rememberMe = rememberMe;
        }
    }

    public class UserRegisterData
    {
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public string email { get; private set; }
        public string password { get; private set; }
        public DateTime dob { get; private set; }
        public Gender gender { get; private set; }

        public UserRegisterData(string fName, string lName, string email, string password, DateTime dob, Gender gender)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.email = email;
            this.password = password;
            this.dob = dob;
            this.gender = gender;
        }
    }

    public class LoginModel : Model<LoginApp>
    {
        public UserLoginData loginData;
        public UserRegisterData registerData;

        public void DoLogin()
        {
            try
            {
                LoginData data = new LoginData();
                data.Username = loginData.name;
                data.Passhash = HashUtils.MD5(loginData.password);
                _ = ChatConnection.Instance.Send(data);
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        
        public void DoRegister()
        {
            try
            {
                RegisterData packet = new RegisterData();
                packet.Email = registerData.email;
                packet.PassHashed = HashUtils.MD5(registerData.password);
                packet.FirstName = registerData.firstName;
                packet.LastName = registerData.lastName;
                packet.DoB = registerData.dob;
                packet.Gender = registerData.gender;
                _ = ChatConnection.Instance.Send(packet);
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
