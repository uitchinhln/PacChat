using PacChatServer.Entities.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Entities
{
    public class UserEnt
    {
        String FirtName { get; set; }
        String LastName { get; set; }
        String Username { get; }
        String HashedPassword { get; set; }
        String Email { get; set; }
        DateTime DayOfBirth { get; set; }
        Gender Gender { get; set; }
        List<int> Friends { get; }
        List<int> Blockeds { get; }
    }
}
