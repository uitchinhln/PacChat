using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PacChat.MVC;

namespace PacChat.Windows
{
    public class LoginView : View<LoginApp>
    {
        // Bind the xaml Controls from LoginWindow.xaml to C# and put them here, then process
        
        public void Login()
        {
            // Process UI
            Console.WriteLine("OnLogin in view");
            // Then notify to controller
            app.controller.OnLogin();
        }

        public void Register()
        {
            // Process UI
            Console.WriteLine("OnRegister in view");
            // Then notify to controller
            app.controller.OnRegister();
        }
    }
}
