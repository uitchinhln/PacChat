using CNetwork;
using PacChat.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Utils
{
    public class Packets
    {
        public static void SendPacket<T>() where T : IPacket
        {
            try
            {
                T data = Activator.CreateInstance<T>();
                _ = ChatConnection.Instance.Send(data);
            } catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
