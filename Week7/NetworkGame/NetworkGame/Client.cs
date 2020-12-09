using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Packets;

namespace Server
{
    public class Client
    {
        public IPEndPoint mEndPoint;
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

        public Packet TCPRead()
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
        }

        public void TCPSend(Packet message)
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
        }

        public void UDPSend(Packet message)
        {
            lock (writeLock)
            {
                MemoryStream memoryStream = new MemoryStream();
                formatter.Serialize(memoryStream, message);
                byte[] buffer = memoryStream.GetBuffer();

                writer.Write(buffer.Length);
                writer.Write(buffer);
                writer.Flush();
            }
        }


    }
}
