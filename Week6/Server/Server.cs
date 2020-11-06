using System;
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
        TcpListener tcpListener;
        

        public Server(string IPaddress, int port)
        {
            IPAddress localAddr = IPAddress.Parse(IPaddress);
            tcpListener = new TcpListener(localAddr, port);
        }

        public void Start()
        {
            Socket socket;
            tcpListener.Start();
            Console.WriteLine("Searching");
            socket = tcpListener.AcceptSocket();
            Console.WriteLine("Connected");
            ClientMethod(socket);
        }

        public void Stop()
        {
            tcpListener.Stop();
        }

        private void ClientMethod(Socket socket)
        {
            try
            {

            
                string receiveMessage;
                NetworkStream stream = new NetworkStream(socket, true);
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);
                StreamWriter writer = new StreamWriter(stream, Encoding.UTF8);

                writer.WriteLine("welcome");
                writer.Flush();

                while((receiveMessage = reader.ReadLine()) != null)
                {
                    string serverMessage = GetReturnMessage(receiveMessage);

                    if (receiveMessage == "end")
                    {
                        break;
                    }
                    else
                    {
                        writer.WriteLine(GetReturnMessage(receiveMessage));
                        writer.Flush();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error:" + e.Message);
            }
            finally
            {
                socket.Close();
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
