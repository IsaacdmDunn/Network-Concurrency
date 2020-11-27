using ClientAndServer;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    public class Client
    {
        ClientForm mClientForm;
        TcpClient tcpClient;
        NetworkStream stream;
        StreamWriter writer;
        StreamReader reader;

        public Client()
        {
            tcpClient = new TcpClient();
        }

        public bool Connect(string ipAddreess, int port)
        {
            try
            {
                tcpClient.Connect(ipAddreess, port);
                tcpClient.GetStream();

                //Socket socket;
                stream = tcpClient.GetStream();
                writer = new StreamWriter(stream, Encoding.UTF8);
                reader = new StreamReader(stream, Encoding.UTF8);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: " + e.Message);
                return false;
            }
        }

        public void Run()
        {
            mClientForm = new ClientForm(this);
            
            Thread threads = new Thread(ProcessServerResponse);
            
            threads.Start();
            mClientForm.ShowDialog();

            //Console.WriteLine("Enter data");

            //while ((userInput = Console.ReadLine()) != null)
            //{
            //    writer.WriteLine(userInput);
            //    writer.Flush();

            //    ProcessServerResponse();

            //    if (userInput == "end")
            //    {
            //        break;
            //    }

            //}
            tcpClient.Close();
        }

        private void ProcessServerResponse()
        {
            while (reader != null)
            {
                mClientForm.UpdateChatWindow(reader.ReadLine());
            }
            

        }

        public void SendMessage(string message, string username)
        {
            writer.WriteLine(username + " says: " + message);
            writer.Flush();
        }
    }
}







