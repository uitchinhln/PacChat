using CNetwork;
using CNetwork.Sessions;
using DotNetty.Buffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Packets.AfterLoginRequest.Profile
{
    /// <summary>
    /// This packet gets user-self profile in order to allow him/her modify his/her private information
    /// The information will be displayed in Setting tab
    /// </summary>
    public class GetSelfProfile : IPacket
    {
        public void Decode(IByteBuffer buffer)
        {

        }

        public IByteBuffer Encode(IByteBuffer byteBuf)
        {
            throw new NotImplementedException();
        }

        public void Handle(ISession session)
        {
            throw new NotImplementedException();
        }
    }
}
