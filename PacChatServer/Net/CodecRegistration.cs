using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PacChatServer.Net.Interface;

namespace PacChatServer.Net
{
    internal class CodecRegistration
    {
        public int Opcode { get; set; }
        public Codec<Message> Codec { get; set; }

        public CodecRegistration(int opcode, Codec<Message> codec)
        {
            this.Codec = codec;
            this.Opcode = opcode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            else if (obj is CodecRegistration)
            {
                return false;
            }
            else
            {
                CodecRegistration other = (CodecRegistration)obj;
                return this.Opcode == other.Opcode;
            }
        }

        public override int GetHashCode()
        {
            var hashCode = 5;
            hashCode = hashCode * 67 + Opcode;
            return hashCode;
        }
    }
}
