using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FormsWeek5
{
    public partial class Form1 : Form
    {
        public delegate void UpdateChatWindowDelegate(string messageReceived);
        public UpdateChatWindowDelegate updateChatWindowDelegate;
        Manager Manager;
        public Form1(Manager manager)
        {
            Manager = manager;
            InitializeComponent();
            updateChatWindowDelegate += new UpdateChatWindowDelegate(UpdateChatWindow);
        }

        private void TerminalTemplate_Load(object sender, EventArgs e)
        {

        }

        public void UpdateChatWindow(string messageReceived)
        {
            if (MessageWindow.InvokeRequired)
            {
                Invoke(updateChatWindowDelegate, messageReceived);
            }
            else
            {
                MessageWindow.Text += messageReceived += "\n";
                MessageWindow.SelectionStart = MessageWindow.Text.Length;
                MessageWindow.ScrollToCaret();
            }
        }
    }
}
