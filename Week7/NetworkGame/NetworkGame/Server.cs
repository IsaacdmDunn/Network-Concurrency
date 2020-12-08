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
    //server class
    class Server
    {
        ConcurrentBag<Client> clients;
        TcpListener tcpListener;

        //constructor
        public Server(string IPaddress, int port)
        {
            //sets up tcp listener
            IPAddress localAddr = IPAddress.Parse(IPaddress);
            tcpListener = new TcpListener(localAddr, port);
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
                Thread thread = new Thread(() => { ClientMethod(client); });
                thread.Start();
            }


        }

        //stops the server
        public void Stop()
        {
            tcpListener.Stop();
        }

        //client method sends data from server program to client program
        private void ClientMethod(Client client)
        {
            Packet packet;

            try
            {
                //while client is reading data
                while ((packet = client.Read()) != null)
                {
                    switch (packet.mPacketType)
                    {
                        //send position to everyone
                        case PacketType.positionData:
                            PositionPacket positionDataPacket = (PositionPacket)packet;
                            foreach (Client onlineClient in clients)
                            {
                                onlineClient.Send(packet);
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
