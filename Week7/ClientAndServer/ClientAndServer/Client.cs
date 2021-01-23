using ClientAndServer;
using Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    //client class
    public class Client
    {
        ClientForm mClientForm;
        TcpClient tcpClient;
        NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;
        BinaryFormatter formatter;
        bool isConnected = false;

        //constructor
        public Client()
        {
            tcpClient = new TcpClient();
        }

        //connects to server
        public bool Connect(string ipAddreess, int port)
        {
            try
            {
                //connects to server
                tcpClient.Connect(ipAddreess, port);
                stream = tcpClient.GetStream(); 
                writer = new BinaryWriter(stream, Encoding.UTF8);
                reader = new BinaryReader(stream, Encoding.UTF8);
                formatter = new BinaryFormatter();

                //send connect message
                SendConnectMessage();
                return true;
            }
            //if client fails to connect
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        //starts client
        public void Run()
        {
            mClientForm = new ClientForm(this);
            
            //sets up new thread
            Thread threads = new Thread(ProcessServerResponse);
            threads.Start();
            mClientForm.ShowDialog();

        }

        //sends connect message
        public void SendConnectMessage()
        {
            ConnectMessagePacket connectPacket = new ConnectMessagePacket("user");
            MemoryStream msgStream = new MemoryStream();
            formatter.Serialize(msgStream, connectPacket);
            byte[] buffer = msgStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
            isConnected = true;
        }

        //sends diconnect message
        public void Disconnect(string username)
        {
            DisconnectMessagePacket disconnectPacket = new DisconnectMessagePacket(username);
            MemoryStream msgStream = new MemoryStream();
            formatter.Serialize(msgStream, disconnectPacket);
            byte[] buffer = msgStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
            isConnected = false;
        }

        //processes server responce
        private void ProcessServerResponse()
        {
            int numberOfBytes;
            //if connected to server
            while (isConnected == true)
            {
                //read data from server and deserialize
                if ((numberOfBytes = reader.ReadInt32()) != 0)
                {
                    byte[] buffer = reader.ReadBytes(numberOfBytes);
                    MemoryStream _stream = new MemoryStream(buffer);
                    Packet packet = formatter.Deserialize(_stream) as Packet;

                    //if packet type is...
                    switch (packet.mPacketType)
                    {
                        //send message to all
                        case PacketType.chatMessage:
                            ChatMessagePacket chatPacket = (ChatMessagePacket)packet;
                            mClientForm.UpdateChatWindow(chatPacket.mSender + ": " + chatPacket.mMessage);
                            break;
                        //send disconnect message to all
                        case PacketType.disconnectMessage:
                            DisconnectMessagePacket disconnectPacket = (DisconnectMessagePacket)packet;
                            mClientForm.UpdateChatWindow(disconnectPacket.mSender + ": has disconnected.");
                            break;
                        //send connect message to all
                        case PacketType.connectMessage:
                            ConnectMessagePacket connectPacket = (ConnectMessagePacket)packet;
                            mClientForm.UpdateChatWindow(connectPacket.mSender + ": has connected.");
                            break;
                        //send private message to user
                        case PacketType.privateMessage:
                            PrivateMessagePacket privateMessagePacket = (PrivateMessagePacket)packet;
                            mClientForm.UpdateChatWindow(privateMessagePacket.mSender + "(" + privateMessagePacket.mReceiver + ")" + " Wispers: " + privateMessagePacket.mMessage);
                            break;

                    }
                }
            }
            
            reader.Close();
            writer.Close();
            tcpClient.Close();
        }

        //sends public chat message
        public void SendChatMessage(string message, string username)
        {
            ChatMessagePacket messagePacket = new ChatMessagePacket(username, message);
            MemoryStream msgStream = new MemoryStream();
            formatter.Serialize(msgStream, messagePacket);
            byte[] buffer = msgStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }

        //sends private chat packet
        public void SendPrivateMessage(string message, string username, int receiver)
        {
            PrivateMessagePacket messagePacket = new PrivateMessagePacket(username, message, receiver);
            MemoryStream msgStream = new MemoryStream();
            formatter.Serialize(msgStream, messagePacket);
            byte[] buffer = msgStream.GetBuffer();
            writer.Write(buffer.Length);
            writer.Write(buffer);
            writer.Flush();
        }

    }
}







