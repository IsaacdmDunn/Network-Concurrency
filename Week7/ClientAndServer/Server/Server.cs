using Packets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    
    class Server
    {
        ConcurrentBag<Client> clients;

        TcpListener tcpListener;


        public Server(string IPaddress, int port)
        {
            IPAddress localAddr = IPAddress.Parse(IPaddress);
            tcpListener = new TcpListener(localAddr, port);
        }

        public void Start()
        {
            clients = new ConcurrentBag<Client>();
            Socket socket;
            tcpListener.Start();
            while (true)
            {
                Console.WriteLine("Searching");
                socket = tcpListener.AcceptSocket();
                Console.WriteLine("Connected");
                Client client = new Client(socket);
                clients.Add(client);
                Thread thread = new Thread(() => { ClientMethod(client); });
                thread.Start();
            }

            
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        private void ClientMethod(Client client)
        {
            Packet packet;
            
            try
            {
                while ((packet = client.Read()) != null)
                {
                    switch (packet.mPacketType)
                    {
                        case PacketType.chatMessage:
                            ChatMessagePacket chatMessagePacket = (ChatMessagePacket)packet;
                            foreach (Client onlineClient in clients)
                            {
                                onlineClient.Send(packet);
                            }
                            break;
                        case PacketType.disconnectMessage:
                            DisconnectMessagePacket disconnectPacket = (DisconnectMessagePacket)packet;
                            foreach (Client onlineClient in clients)
                            {
                                onlineClient.Send(packet);
                            }
                            break;
                        case PacketType.connectMessage:
                            ConnectMessagePacket connectMessage = (ConnectMessagePacket)packet;
                            foreach (Client onlineClient in clients)
                            {
                                onlineClient.Send(packet);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
            finally
            {
                client.Close();
                clients.TryTake(out client);
            }

        }

        private string GetReturnMessage(string code)
        {
            if (code == "hi")
            {
                return "hello";
            }
            return code;
        }
    }
}
