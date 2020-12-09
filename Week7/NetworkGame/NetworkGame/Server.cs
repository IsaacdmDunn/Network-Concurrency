using Packets;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    //server class
    class Server
    {
        ConcurrentBag<Client> clients;
        UdpClient udpListener;
        TcpListener tcpListener;

        //constructor
        public Server(string IPaddress, int port)
        {
            //sets up tcp listener
            IPAddress localAddr = IPAddress.Parse(IPaddress);
            udpListener = new UdpClient(port);
            tcpListener = new TcpListener(localAddr, port);
            Thread listen = new Thread(() => { UDPListen(); });
            listen.Start();
        }

        private void UDPListen()
        {
            try
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 0);

                while (true)
                {
                    byte[] buffer = udpListener.Receive(ref endPoint);
                    MemoryStream stream = new MemoryStream(buffer);
                    BinaryFormatter binaryFormatter = new BinaryFormatter();
                    Packet packet = binaryFormatter.Deserialize(stream) as Packet;
                    foreach (Client onlineClient in clients)
                    {
                        if (onlineClient.mEndPoint != null && endPoint.ToString() != onlineClient.mEndPoint.ToString())
                        {
                            switch (packet.mPacketType)
                            {
                                case PacketType.positionData:
                                    //onlineClient.TCPSend(packet);
                                    
                                    MemoryStream memoryStream = new MemoryStream();
                                    binaryFormatter.Serialize(memoryStream, packet);
                                    buffer = memoryStream.GetBuffer();

                                    udpListener.Send(buffer, buffer.Length, onlineClient.mEndPoint);
                                    
                                    break;

                            }
                        }
                    }
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("Client UDP Read Method Exception: " + e.Message);
            }
        }

        //starts server
        public void Start()
        {

            clients = new ConcurrentBag<Client>();
            Socket socket;
            tcpListener.Start();

            //searches for clients and adds then to the concurrent bag
            while (true)
            {
                Console.WriteLine("Searching");
                socket = tcpListener.AcceptSocket();
                Console.WriteLine("Connected");

                //adds client to concurrent bag
                Client client = new Client(socket);
                clients.Add(client);

                //creates and starts new thread
                Thread thread = new Thread(() => { TCPClientMethod(client); });
                thread.Start();
            }


        }

        //stops the server
        public void Stop()
        {
            tcpListener.Stop();
        }

        //client method sends data from server program to client program
        private void TCPClientMethod(Client client)
        {
            Packet packet;

            try
            {
                //while client is reading data
                while ((packet = client.TCPRead()) != null)
                {
                    switch (packet.mPacketType)
                    {
                        
                        case PacketType.login:
                            LoginPacket loginPacket = (LoginPacket)packet;
                            client.mEndPoint = loginPacket.mEndPoint;
                            foreach (Client onlineClient in clients)
                            {
                                
                                onlineClient.TCPSend(packet);
                            }
                            break;
                        
                    }
                }
            }
            //if try catch fails send error message
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
            //close client and remove from concurrent bag
            finally
            {
                client.Close();
                clients.TryTake(out client);
            }

        }
    }
}
