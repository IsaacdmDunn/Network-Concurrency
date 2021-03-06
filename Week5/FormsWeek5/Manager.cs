﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace FormsWeek5
{
    public class Manager
    {
        Form1 m_Terminal1;
        Form1 m_Terminal2;
        public Manager()
        {
            m_Terminal1 = new Form1(this);
            Thread FormThread1 = new Thread(() => { CreateForm(m_Terminal1); } );

            m_Terminal2 = new Form1(this);
            Thread FormThread2 = new Thread(() => { CreateForm(m_Terminal2); });

            FormThread1.Start();
            FormThread2.Start();

            for (int i = 0; i < 10; i++)
            {
                if (i % 2 ==0)
                {
                    SendMessageToAll("Message", "ted");
                    Thread.Sleep(500);
                }
                else
                {
                    SendMessageToAll("Message", "greg");
                    Thread.Sleep(500);
                }
                
            }

            FormThread1.Join();
            FormThread2.Join();
        }

        void CreateForm(Form1 terminal)
        {
            terminal.ShowDialog();
        }

        void SendMessageToAll(string message, string name )
        {
            string finalMessage = name + ": " + message;
            m_Terminal1.UpdateChatWindow(finalMessage);
            m_Terminal2.UpdateChatWindow(finalMessage);
        }
    }
}