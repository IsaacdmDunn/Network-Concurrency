using System;

namespace Packets
{
    public enum PacketType
    {
        chatMessage,
        privateMessage,
        disconnectMessage,        
        empty
    }

    [Serializable]
    public class Packet
    {
        public PacketType mPacketType { get; set; }

        public Packet()
        {
            mPacketType = PacketType.empty;
        }


    }


    [Serializable]
    public class ChatMessagePacket : Packet
    {
        public string mMessage;
        public string mSender;

        public ChatMessagePacket(string sender, string message)
        {
            mMessage = message;
            mSender = sender;
            mPacketType = PacketType.chatMessage;
        }
    }

    [Serializable]
    public class DisconnectMessagePacket : Packet
    {
        public string mSender;

        public DisconnectMessagePacket(string sender)
        {
            mSender = sender;
            mPacketType = PacketType.disconnectMessage;
        }
    }
}