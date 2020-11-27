using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class Client
    {
        Socket socket;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        object readLock;
        object writeLock;

        public Client(Socket _socket)
        {
            socket = _socket;
            stream = new NetworkStream(_socket);
            writer = new StreamWriter(stream, Encoding.UTF8);
            reader = new StreamReader(stream, Encoding.UTF8);
            readLock = new object();
            writeLock = new object();

        }

        public void Close()
        {
            reader.Close();
            writer.Close();
            stream.Close();
            socket.Close();
        }

        public string Read()
        {
            lock (readLock) ;
            return reader.ReadLine();
        }

        public void Send(string message)
        {
            lock (writeLock) ;
            writer.WriteLine(message);
            writer.Flush();
        }


    }
}







//TcpClient tcpClient;
//NetworkStream stream;
//StreamWriter writer;
//StreamReader reader;

//public Client()
//{
//    tcpClient = new TcpClient();
//}

//public bool Connect(string ipAddreess, int port)
//{
//    try
//    {
//        tcpClient.Connect(ipAddreess, port);
//        tcpClient.GetStream();

//        //Socket socket;
//        stream = tcpClient.GetStream();
//        writer = new StreamWriter(stream, Encoding.UTF8);
//        reader = new StreamReader(stream, Encoding.UTF8);

//        return true;
//    }
//    catch (Exception e)
//    {
//        Console.WriteLine("Exception: " + e.Message);
//        return false;
//    }
//}

//public void Run()
//{
//    string userInput;
//    ProcessServerResponse();
//    Console.WriteLine("Enter data");

//    while ((userInput = Console.ReadLine()) != null)
//    {
//        writer.WriteLine(userInput);
//        writer.Flush();

//        ProcessServerResponse();

//        if (userInput == "end")
//        {
//            break;
//        }

//        tcpClient.Close();
//    }
//}

//private void ProcessServerResponse()
//{
//    Console.WriteLine("Server says: " + reader.ReadLine());
//    Console.WriteLine();

//}