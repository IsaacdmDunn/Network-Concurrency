using System;

namespace ClientAndServer
{
    partial class ClientForm
    {
        
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ChatForm()
        {
            InitializeComponent();
        }

        

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.MessageWindow = new System.Windows.Forms.TextBox();
            this.InputField = new System.Windows.Forms.TextBox();
            this.Submit = new System.Windows.Forms.Button();
            this.DisconnectBtn = new System.Windows.Forms.Button();
            this.UsernameInput = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.OnlineList = new System.Windows.Forms.TextBox();
            this.OnlineCounter = new System.Windows.Forms.Label();
            this.UpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.ActivityTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // MessageWindow
            // 
            this.MessageWindow.Location = new System.Drawing.Point(16, 12);
            this.MessageWindow.Multiline = true;
            this.MessageWindow.Name = "MessageWindow";
            this.MessageWindow.Size = new System.Drawing.Size(775, 354);
            this.MessageWindow.TabIndex = 0;
            this.MessageWindow.TextChanged += new System.EventHandler(this.MessageWindow_TextChanged);
            // 
            // InputField
            // 
            this.InputField.Location = new System.Drawing.Point(95, 384);
            this.InputField.Name = "InputField";
            this.InputField.Size = new System.Drawing.Size(616, 20);
            this.InputField.TabIndex = 1;
            // 
            // Submit
            // 
            this.Submit.Location = new System.Drawing.Point(717, 384);
            this.Submit.Name = "Submit";
            this.Submit.Size = new System.Drawing.Size(74, 23);
            this.Submit.TabIndex = 2;
            this.Submit.Text = "Submit";
            this.Submit.UseVisualStyleBackColor = true;
            this.Submit.Click += new System.EventHandler(this.SubmitButtonClick);
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.Location = new System.Drawing.Point(717, 415);
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(75, 23);
            this.DisconnectBtn.TabIndex = 3;
            this.DisconnectBtn.Text = "Disconnect";
            this.DisconnectBtn.UseVisualStyleBackColor = true;
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectButtonClick);
            // 
            // UsernameInput
            // 
            this.UsernameInput.Location = new System.Drawing.Point(95, 411);
            this.UsernameInput.Name = "UsernameInput";
            this.UsernameInput.Size = new System.Drawing.Size(199, 20);
            this.UsernameInput.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 384);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Message";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 415);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Username";
            // 
            // OnlineList
            // 
            this.OnlineList.Location = new System.Drawing.Point(805, 29);
            this.OnlineList.Multiline = true;
            this.OnlineList.Name = "OnlineList";
            this.OnlineList.Size = new System.Drawing.Size(182, 409);
            this.OnlineList.TabIndex = 7;
            // 
            // OnlineCounter
            // 
            this.OnlineCounter.AutoSize = true;
            this.OnlineCounter.Location = new System.Drawing.Point(805, 13);
            this.OnlineCounter.Name = "OnlineCounter";
            this.OnlineCounter.Size = new System.Drawing.Size(49, 13);
            this.OnlineCounter.TabIndex = 8;
            this.OnlineCounter.Text = "Online: 0";
            // 
            // UpdateTimer
            // 
            this.UpdateTimer.Tick += new System.EventHandler(this.UpdateTimer_Tick);
            // 
            // ActivityTimer
            // 
            this.ActivityTimer.Interval = 6000;
            this.ActivityTimer.Tick += new System.EventHandler(this.ActivityTimer_Tick);
            // 
            // ClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(999, 450);
            this.Controls.Add(this.OnlineCounter);
            this.Controls.Add(this.OnlineList);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.UsernameInput);
            this.Controls.Add(this.DisconnectBtn);
            this.Controls.Add(this.Submit);
            this.Controls.Add(this.InputField);
            this.Controls.Add(this.MessageWindow);
            this.Name = "ClientForm";
            this.Text = "Chat";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox MessageWindow;
        private System.Windows.Forms.TextBox InputField;
        private System.Windows.Forms.Button Submit;
        private System.Windows.Forms.Button DisconnectBtn;
        private System.Windows.Forms.TextBox UsernameInput;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox OnlineList;
        private System.Windows.Forms.Label OnlineCounter;
        private System.Windows.Forms.Timer UpdateTimer;
        private System.Windows.Forms.Timer ActivityTimer;
    }
}