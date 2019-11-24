using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Utils
{
    public class HashStack<T> : List<T>
    {
        new public void Add(T item)
        {
            if (this.Contains(item)) this.RemoveAll(t => t.Equals(item));
            this.Insert(0, item);
        }
    }
}
