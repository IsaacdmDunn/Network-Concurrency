using System;
using System.Net;

namespace Packets
{

    
    //enum stores all types of packets that can be sent
    public enum PacketType
    {
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