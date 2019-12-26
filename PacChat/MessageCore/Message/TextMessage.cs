using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CNetwork.Sessions;
using CNetwork.Utils;
using DotNetty.Buffers;

namespace PacChat.MessageCore.Message
{
    public class TextMessage : AbstractMessage
    {
        private string _message = String.Empty;
        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                while (_message.Contains(Environment.NewLine + Environment.NewLine) || _message.Contains("\n\n"))
                {
                    _message = _message.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine).Replace("\n\n", "\n");
                }
            }
        }

        public override void DecodeFromBuffer(IByteBuffer buffer)
        {
            Message = ByteBufUtils.ReadUTF8(buffer);
        }

        public override IByteBuffer EncodeToBuffer(IByteBuffer buffer)
        {
            buffer.WriteInt(GetPreviewCode());
            ByteBufUtils.WriteUTF8(buffer, Message);
            return buffer;
        }

        public override int GetPreviewCode()
        {
            return 4;
        }
    }
}
