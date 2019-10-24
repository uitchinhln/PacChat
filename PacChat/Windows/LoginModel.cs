using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;
using PacChat.Utils;
using PacChat.Network.Packets;
using PacChat.Network.Protocol;

namespace PacChat.Windows
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
        public DateTime dob { get; private set; }
        public Gender gender { get; private set; }

        public UserRegisterData(string fName, string lName, string email, DateTime dob, Gender gender)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.email = email;
            this.dob = dob;
            this.gender = gender;
        }
    }

    public class LoginModel : Model<LoginApp>
    {
        public UserLoginData loginData;
        public UserRegisterData registerData;
    }
}
