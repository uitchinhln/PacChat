using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;

namespace PacChat.Windows.Login
{
    public class LoginController : Controller<LoginApp>
    {
        public void OnLogin(string username, string password, bool remember)
        {
            Console.WriteLine("OnLogin in controller");
            // Here update login model, access to login model as following example
            app.model.loginData = new UserLoginData(username, password, remember);
            app.model.DoLogin();
        }

        public void OnRegister()
        {
            Console.WriteLine("OnRegister in controller");
            // Here update login model, access to login model as following example
            app.model.registerData = new UserRegisterData
                (
                "Bach", "Nguyen", "nlebachnlb@gmail.com",
                new DateTime(2000, 11, 11),
                Utils.Gender.Male
                );
        }
    }
}
