using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Network
{
    public class ResponeCode
    {
        public static readonly int NotFound = 404;
        public static readonly int Unauthorized = 401;
        public static readonly int Forbidden = 403;
        public static readonly int OK = 200;
        public static readonly int Conflict = 409;
    }
}
