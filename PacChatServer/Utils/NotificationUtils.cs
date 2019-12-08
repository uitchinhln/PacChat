using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChatServer.Utils
{
    public class NotificationPrefixes
    {
        public static string AddFriend = "mkfriend";
        public static string AcceptedFriend = "acfriend";
    }

    public class NotificationInfo
    {
        public string Prefix { get; set; }
        public string SenderID { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public bool IsRead { get; set; }
    }

    public class NotificationDecoder
    {
        public static bool Analyze(string encNoti, out string prefix, out string senderID, out string name, out string title, out string subtitle, out bool isRead)
        {
            prefix = senderID = name = title = subtitle = "";
            isRead = false;

            var splitted = encNoti.Split(':');

            if (splitted.Length < 6) return false;

            prefix = splitted[0];
            senderID = splitted[1];
            name = splitted[2];
            title = splitted[3];
            subtitle = splitted[4];
            isRead = splitted[5] == "1" ? true : false;

            return true;
        }

        public static NotificationInfo Analyze(string encNoti)
        {
            var result = new NotificationInfo();
            string prefix, senderID, name, title, subtitle;
            bool isRead;

            if (Analyze(encNoti, out prefix, out senderID, out name,
                out title, out subtitle, out isRead))
            {
                result.Prefix = prefix;
                result.SenderID = senderID;
                result.Name = name;
                result.Title = title;
                result.Subtitle = subtitle;
                result.IsRead = isRead;
            }

            return result;
        }
    }

    public class NotificationEncoder
    {
        public static string Assemble(string prefix, string senderID, string name, string title, string subtitle, bool isRead)
        {
            string result = prefix + ":" + senderID + ":" + 
                name + ":" + title + ":" + subtitle + ":" + 
                (isRead ? "1" : "0");
            return result;
        }

        public string Assemble(NotificationInfo info)
        {
            return Assemble(info.Prefix, info.SenderID, info.Name, info.Title, info.Subtitle, info.IsRead);
        }
    }
}
