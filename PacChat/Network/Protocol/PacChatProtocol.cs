using CNetwork;
using CNetwork.Exceptions;
using CNetwork.Protocols;
using CNetwork.Services;
using CNetwork.Utils;
using DotNetty.Buffers;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PacChat.Network.Protocol
{
    public abstract class PacChatProtocol : AbstractProtocol
    {
        PacketLookupService inboundCodecs;
        PacketLookupService outboundCodecs;

        public PacChatProtocol(string name) : base(name)
        {
            inboundCodecs = new PacketLookupService();
            outboundCodecs = new PacketLookupService();
        }

        protected void Inbound(int opcode, IPacket packet)
        {
            try
            {
                inboundCodecs.Bind(opcode, packet);
            }
            catch (Exception e)
            {
                Logger.Error("Error registering inbound " + opcode + " in " + Name, e);
            }
        }

        protected void Outbound(int opcode, IPacket packet)
        {
            try
            {
                outboundCodecs.Bind(opcode, packet);
            }
            catch (Exception e)
            {
                Logger.Error("Error registering outbound " + opcode + " in " + Name, e);
            }
        }

        public override PacketRegistration GetPacketRegistration(IPacket packet)
        {
            PacketRegistration reg = outboundCodecs.Find(packet);
            if (reg == null)
            {
                Logger.Warn("No codec to write: " + packet.GetType().Name + " in " + Name);
            }
            return reg;
        }

        public override IPacket ReadHeader(IByteBuffer buf)
        {
            int length = -1;
            int opcode = -1;
            try
            {
                length = ByteBufUtils.ReadVarInt(buf);

                // mark point before opcode
                buf.MarkReaderIndex();

                opcode = ByteBufUtils.ReadVarInt(buf);

                return inboundCodecs.Find(opcode);
            }
            catch (IOException)
            {
                throw new UnknownPacketException("Failed to read packet data (corrupt?)", opcode, length);
            }
            catch (IllegalOpcodeException)
            {
                // go back to before opcode, so that skipping length doesn't skip too much
                buf.ResetReaderIndex();
                throw new UnknownPacketException("Opcode received is not a registered codec on the server!", opcode, length);
            }
        }

        public override IByteBuffer WriteHeader(IByteBuffer header, PacketRegistration packet, IByteBuffer data)
        {
            IByteBuffer opcodeBuffer = Unpooled.Buffer(5);
            ByteBufUtils.WriteVarInt(opcodeBuffer, packet.Opcode);
            ByteBufUtils.WriteVarInt(header, opcodeBuffer.ReadableBytes + data.ReadableBytes);
            opcodeBuffer.Release();
            ByteBufUtils.WriteVarInt(header, packet.Opcode);
            return header;
        }

        public IPacket NewReadHeader(IByteBuffer header)
        {
            int opcode = ByteBufUtils.ReadVarInt(header);
            return inboundCodecs.Find(opcode);
        }
    }
}
