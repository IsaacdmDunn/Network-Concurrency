﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientAndServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Client client = new Client();
            client.Connect("127.0.0.1", 4444);
            client.Run();
        }
    }
}
