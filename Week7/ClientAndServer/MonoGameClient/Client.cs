//using Packets;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Sockets;
//using System.Runtime.Serialization.Formatters.Binary;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace Server
//{
//    public class Client
//    {
//        ClientForm mClientForm;
//        TcpClient tcpClient;
//        NetworkStream stream;
//        BinaryReader reader;
//        BinaryWriter writer;
//        BinaryFormatter formatter;
//        bool isConnected = false;

//        public Client()
//        {
//            tcpClient = new TcpClient();
//        }

//        public bool Connect(string ipAddreess, int port)
//        {
//            try
//            {
//                tcpClient.Connect(ipAddreess, port);
//                stream = tcpClient.GetStream(); 
//                writer = new BinaryWriter(stream, Encoding.UTF8);
//                reader = new BinaryReader(stream, Encoding.UTF8);
//                formatter = new BinaryFormatter();
//                SendConnectMessage();
//                return true;
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine("Exception: " + e.Message);
//                return false;
//            }
//        }

//        public void Run()
//        {
//            mClientForm = new ClientForm(this);
            
//            Thread threads = new Thread(ProcessServerResponse);
//            threads.Start();
//            mClientForm.ShowDialog();

//        }

//        public void SendConnectMessage()
//        {
//            ConnectMessagePacket connectPacket = new ConnectMessagePacket("user");
//            MemoryStream msgStream = new MemoryStream();
//            formatter.Serialize(msgStream, connectPacket);
//            byte[] buffer = msgStream.GetBuffer();
//            writer.Write(buffer.Length);
//            writer.Write(buffer);
//            writer.Flush();
//            isConnected = true;
//        }

//        public void Disconnect(string username)
//        {
//            DisconnectMessagePacket disconnectPacket = new DisconnectMessagePacket(username);
//            MemoryStream msgStream = new MemoryStream();
//            formatter.Serialize(msgStream, disconnectPacket);
//            byte[] buffer = msgStream.GetBuffer();
//            writer.Write(buffer.Length);
//            writer.Write(buffer);
//            writer.Flush();
//            isConnected = false;
//        }

//        private void ProcessServerResponse()
//        {
//            int numberOfBytes;
//            while (isConnected == true)
//            {
//                if ((numberOfBytes = reader.ReadInt32()) != 0)
//                {
//                    byte[] buffer = reader.ReadBytes(numberOfBytes);
//                    MemoryStream _stream = new MemoryStream(buffer);
//                    Packet recievedPackage = formatter.Deserialize(_stream) as Packet;

//                    switch (recievedPackage.mPacketType)
//                    {
//                        case PacketType.chatMessage:
//                            ChatMessagePacket chatPacket = (ChatMessagePacket)recievedPackage;
//                            mClientForm.UpdateChatWindow(chatPacket.mSender + ": " + chatPacket.mMessage);
//                            break;
//                        case PacketType.disconnectMessage:
//                            DisconnectMessagePacket disconnectPacket = (DisconnectMessagePacket)recievedPackage;
//                            mClientForm.UpdateChatWindow(disconnectPacket.mSender + ": has disconnected.");
//                            break;
//                        case PacketType.connectMessage:
//                            ConnectMessagePacket connectPacket = (ConnectMessagePacket)recievedPackage;
//                            mClientForm.UpdateChatWindow(connectPacket.mSender + ": has connected.");
//                            break;
//                    }
//                }
//            }
            
//            reader.Close();
//            writer.Close();
//            tcpClient.Close();
//        }

//        public void SendChatMessage(string message, string username)
//        {
//            ChatMessagePacket messagePacket = new ChatMessagePacket(username, message);
//            MemoryStream msgStream = new MemoryStream();
//            formatter.Serialize(msgStream, messagePacket);
//            byte[] buffer = msgStream.GetBuffer();
//            writer.Write(buffer.Length);
//            writer.Write(buffer);
//            writer.Flush();
//        }

//    }
//}







