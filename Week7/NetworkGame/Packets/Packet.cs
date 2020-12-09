using System;
using System.Net;

namespace Packets
{

    
    //enum stores all types of packets that can be sent
    public enum PacketType
    {
        chatMessage,
        privateMessage,
        disconnectMessage,
        connectMessage,
        onlineData,
        positionData,
        login,
        empty
    }

    //empty packet 
    [Serializable]
    public class Packet
    {
        public PacketType mPacketType { get; set; }

        public Packet()
        {
            mPacketType = PacketType.empty;
        }


    }

    //positon data for players
    [Serializable]
    public class PositionPacket : Packet
    {
        public float mPosX;
        public float mPosY;

        public PositionPacket(float posX, float posY)
        {
            mPosX = posX;
            mPosY = posY;
            mPacketType = PacketType.positionData;
        }
    }

    //chat message packet for messages sent to all clients takes values for sender(username) and message user sent
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

    //private message packet require sender(username), message to be sent and id for recipient
    [Serializable]
    public class PrivateMessagePacket : Packet
    {
        public string mMessage;
        public string mSender;
        public int mReceiver;

        public PrivateMessagePacket(string sender, string message, int receiver)
        {
            mMessage = message;
            mSender = sender;
            mReceiver = receiver;
            mPacketType = PacketType.privateMessage;
        }
    }

    //disconnect message packet for disconnect message only needs sender 
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

    //connect message packet for connect message only needs sender 
    [Serializable]
    public class ConnectMessagePacket : Packet
    {
        public string mSender;

        public ConnectMessagePacket(string sender)
        {
            mSender = sender;
            mPacketType = PacketType.connectMessage;
        }
    }

    //connect message packet for connect message only needs sender 
    [Serializable]
    public class OnlineDataPacket : Packet
    {
        public int mOnlineCount;

        public OnlineDataPacket(int onlineCount)
        {
            mOnlineCount = onlineCount;
            mPacketType = PacketType.onlineData;
        }
    }

    //sends login packet to th server
    [Serializable]
    public class LoginPacket : Packet
    {
        public IPEndPoint mEndPoint;

        public LoginPacket(IPEndPoint endPoint)
        {
            mEndPoint = endPoint;
            mPacketType = PacketType.login;
        }
    }
}