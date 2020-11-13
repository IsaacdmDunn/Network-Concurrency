using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Client
    {
        Socket socket;
        NetworkStream stream;
        StreamReader reader;
        StreamWriter writer;
        object readLock;
        object writeLock;

        public Client(Socket socket)
        {
            object mWriteLock = new object();
            Socket mSocket = socket;
            NetworkStream mStream = stream;
            StreamReader mReader = reader;
            StreamWriter mWriter = writer;
        }

        public void Close()
        {
            stream.Close();
            reader.Close();
            writer.Close();
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
            writer.WriteLine("hello");
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