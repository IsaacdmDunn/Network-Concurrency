using Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientAndServer
{
    public partial class ClientForm : Form
    {

        private Client mClient;
        bool isConnected = true;

        public void UpdateChatWindow(string message)
        {
            if (MessageWindow.InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    UpdateChatWindow(message);
                }));
            }
            else
            {
                MessageWindow.Text += message + Environment.NewLine;
                MessageWindow.SelectionStart = MessageWindow.Text.Length;
                MessageWindow.ScrollToCaret();
            }
        }


        private void SubmitButtonClick(object sender, EventArgs e)
        {
            mClient.SendChatMessage(InputField.Text, UsernameInput.Text);
            InputField.Clear();
        }

        public ClientForm(Client client)
        {
            mClient = client;
            InitializeComponent();
        }

        private void MessageWindow_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void DisconnectButtonClick(object sender, EventArgs e)
        {
            mClient.Disconnect(UsernameInput.Text);
            Submit.Enabled = false;
            DisconnectBtn.Enabled = false;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            //mClient.SendChatMessage(InputField.Text, UsernameInput.Text);
            //InputField.Clear();
        }

        private void ClientForm_Load(object sender, EventArgs e)
        {
            
            ActivityTimer.Tick += new System.EventHandler(ActivityTimer_Tick);
            ActivityTimer.Start();
            
        }

        private void ActivityTimer_Tick(object sender, EventArgs e)
        {
            if (isConnected == true)
            {
                mClient.Disconnect(UsernameInput.Text);
                Submit.Enabled = false;
                DisconnectBtn.Enabled = false;
                isConnected = false;
            }
            
        }
    }
}
