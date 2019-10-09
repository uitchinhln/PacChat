using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.MVC
{
    public class Model : Entity
    {
        
    }

    public class Model<T> : Model where T : App
    {
        new public T app
        {
            get
            {
                return (T)base.app;
            }
        }
    }
}
