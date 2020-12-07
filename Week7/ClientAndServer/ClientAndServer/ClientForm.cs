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

        //updates chatbox with new message
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
                //adds new message on the end of the textbox text
                MessageWindow.Text += message + Environment.NewLine;
                MessageWindow.SelectionStart = MessageWindow.Text.Length;
                MessageWindow.ScrollToCaret();
            }
        }

        //submit button clicked
        private void SubmitButtonClick(object sender, EventArgs e)
        {
            //if message is set to send to all then send username and message to all clients
            if (privateMessageBox.SelectedIndex == 0)
            {
                mClient.SendChatMessage(InputField.Text, UsernameInput.Text);
            }
            //send message and username to the user ID selected
            else
            {
                mClient.SendPrivateMessage(InputField.Text, UsernameInput.Text, privateMessageBox.SelectedIndex - 1);
            }
            
            //clear input text
            InputField.Clear();

            //reset activity timer
            ActivityTimer.Stop();
            ActivityTimer.Start();
        }

        //constructor
        public ClientForm(Client client)
        {
            mClient = client;
            InitializeComponent();
        }

        //disconnect button clicked
        private void DisconnectButtonClick(object sender, EventArgs e)
        {
            //disconnects client and sends disconnect message to all users 
            mClient.Disconnect(UsernameInput.Text);

            //disables buttons so user cant attempt to send data while disconnected
            Submit.Enabled = false;
            DisconnectBtn.Enabled = false;
            
        }

        //update timer for refreshing online list/ counter
        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            //mClient.SendChatMessage(InputField.Text, UsernameInput.Text);
            //InputField.Clear();
        }

        //on client form load
        private void ClientForm_Load(object sender, EventArgs e)
        {
            //set up activity timer
            ActivityTimer.Tick += new System.EventHandler(ActivityTimer_Tick);
            ActivityTimer.Start();

            //defaults message recipient to all
            privateMessageBox.SelectedIndex = 0;
            
        }

        //if activity timer reaches 0 then disconnect user
        private void ActivityTimer_Tick(object sender, EventArgs e)
        {
            //if statement stops user from being disconnected twice
            if (isConnected == true)
            {
                mClient.Disconnect(UsernameInput.Text);
                Submit.Enabled = false;
                DisconnectBtn.Enabled = false;
                isConnected = false;
            }
            
        }

        public void UpdateOnlineCounter(int onlineCount)
        {
            OnlineCounter.Text = "Online: " + onlineCount.ToString();
        }
    }
}
