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
            int i = 0; 
            while (i == clients.Count())
            {
                Console.WriteLine("Searching");
                socket = tcpListener.AcceptSocket();
                Console.WriteLine("Connected");
                Client client = new Client(socket);
                clients.Add(client);
                Thread thread = new Thread(() => { ClientMethod(client); });
                thread.Start();
                i++;
            }

            
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        private void ClientMethod(Client client)
        {
            try
            {


                string receiveMessage = "";
                //NetworkStream stream = new NetworkStream(socket, true);
                //StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                //StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);

                client.Send(receiveMessage);

                while ((receiveMessage = client.Read()) != null)
                {
                    string serverMessage = GetReturnMessage(receiveMessage);

                    if (receiveMessage == "end")
                    {
                        break;
                    }
                    else
                    {
                        client.Send(GetReturnMessage(receiveMessage));
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
