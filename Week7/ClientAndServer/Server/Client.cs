using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace Server
{
    public class Client
    {
        Socket socket;
        NetworkStream stream;
        BinaryReader reader;
        BinaryWriter writer;
        BinaryFormatter formatter;
        object readLock;
        object writeLock;

        public Client(Socket _socket)
        {
            socket = _socket;
            stream = new NetworkStream(_socket);
            writer = new BinaryWriter(stream, Encoding.UTF8);
            reader = new BinaryReader(stream, Encoding.UTF8);
            formatter = new BinaryFormatter();
            readLock = new object();
            writeLock = new object();

        }

        public void Close()
        {
            socket.Shutdown(SocketShutdown.Both);
            reader.Close();
            writer.Close();
            stream.Close();
            socket.Close();
        }

        public Packet Read()
        {
            lock (readLock)
            {
                int numberOfBytes = reader.ReadInt32();
                if (numberOfBytes != -1)
                {
                    byte[] buffer = reader.ReadBytes(numberOfBytes);

                    MemoryStream stream = new MemoryStream(buffer);
                    return formatter.Deserialize(stream) as Packet;
                }
                else
                {
                    return null;
                }
            }
            //return reader.ReadLine();
        }

        public void Send(Packet message)
        {
            lock (writeLock)
            {
                MemoryStream memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, message);
                byte[] bufffer = memoryStream.GetBuffer();

                writer.Write(bufffer.Length);
                writer.Write(bufffer);
                writer.Flush();
            }
            //writer.WriteLine(message);
            //writer.Flush();
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