using System;
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
                SendMessageToAll("Message");
                Thread.Sleep(500);
            }

            FormThread1.Join();
            FormThread2.Join();
        }

        void CreateForm(Form1 terminal)
        {
            terminal.ShowDialog();
        }

        void SendMessageToAll(string message)
        {
            m_Terminal1.UpdateChatWindow(message);
            m_Terminal2.UpdateChatWindow(message);
        }
    }
}