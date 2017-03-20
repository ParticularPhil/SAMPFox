namespace SAMPExtender_Client
{
    partial class MainForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.CheckForProcessTimer = new System.Windows.Forms.Timer(this.components);
            this.portTB = new System.Windows.Forms.TextBox();
            this.addressTB = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.connectBtn = new System.Windows.Forms.Button();
            this.connectionLabel = new System.Windows.Forms.Label();
            this.networkThread = new System.ComponentModel.BackgroundWorker();
            this.manconnectlabel = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.statusLabel = new System.Windows.Forms.Label();
            this.logRTB = new System.Windows.Forms.RichTextBox();
            this.logoPB = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.forcedPortTB = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.versionCB = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.logoPB)).BeginInit();
            this.SuspendLayout();
            // 
            // CheckForProcessTimer
            // 
            this.CheckForProcessTimer.Interval = 50;
            this.CheckForProcessTimer.Tick += new System.EventHandler(this.clientTimer_Tick);
            // 
            // portTB
            // 
            this.portTB.Location = new System.Drawing.Point(112, 183);
            this.portTB.Name = "portTB";
            this.portTB.Size = new System.Drawing.Size(114, 20);
            this.portTB.TabIndex = 2;
            this.portTB.Text = "42690";
            // 
            // addressTB
            // 
            this.addressTB.Location = new System.Drawing.Point(112, 157);
            this.addressTB.Name = "addressTB";
            this.addressTB.Size = new System.Drawing.Size(114, 20);
            this.addressTB.TabIndex = 3;
            this.addressTB.Text = "127.0.0.1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 160);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Server address:";
            // 
            // connectBtn
            // 
            this.connectBtn.Location = new System.Drawing.Point(25, 209);
            this.connectBtn.Name = "connectBtn";
            this.connectBtn.Size = new System.Drawing.Size(201, 23);
            this.connectBtn.TabIndex = 5;
            this.connectBtn.Text = "Connect";
            this.connectBtn.UseVisualStyleBackColor = true;
            this.connectBtn.Click += new System.EventHandler(this.connectBtn_Click);
            // 
            // connectionLabel
            // 
            this.connectionLabel.AutoSize = true;
            this.connectionLabel.Location = new System.Drawing.Point(68, 98);
            this.connectionLabel.Name = "connectionLabel";
            this.connectionLabel.Size = new System.Drawing.Size(166, 13);
            this.connectionLabel.TabIndex = 6;
            this.connectionLabel.Text = "Not connected, no process found";
            // 
            // networkThread
            // 
            this.networkThread.WorkerReportsProgress = true;
            this.networkThread.WorkerSupportsCancellation = true;
            this.networkThread.DoWork += new System.ComponentModel.DoWorkEventHandler(this.networkThread_DoWork);
            this.networkThread.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.networkThread_ProgressChanged);
            this.networkThread.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.networkThread_RunWorkerCompleted);
            // 
            // manconnectlabel
            // 
            this.manconnectlabel.AutoSize = true;
            this.manconnectlabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.manconnectlabel.Location = new System.Drawing.Point(22, 132);
            this.manconnectlabel.Name = "manconnectlabel";
            this.manconnectlabel.Size = new System.Drawing.Size(153, 13);
            this.manconnectlabel.TabIndex = 7;
            this.manconnectlabel.Text = "Manual connection utility:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 186);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Server port:";
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(22, 98);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 13);
            this.statusLabel.TabIndex = 9;
            this.statusLabel.Text = "Status:";
            // 
            // logRTB
            // 
            this.logRTB.Location = new System.Drawing.Point(25, 238);
            this.logRTB.Name = "logRTB";
            this.logRTB.Size = new System.Drawing.Size(201, 96);
            this.logRTB.TabIndex = 10;
            this.logRTB.Text = "";
            // 
            // logoPB
            // 
            this.logoPB.Image = global::SAMPExtender_Client.Properties.Resources.SAMPFox_Wikilogo_350x232;
            this.logoPB.Location = new System.Drawing.Point(71, 3);
            this.logoPB.Name = "logoPB";
            this.logoPB.Size = new System.Drawing.Size(122, 83);
            this.logoPB.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.logoPB.TabIndex = 11;
            this.logoPB.TabStop = false;
            this.logoPB.Click += new System.EventHandler(this.logoPB_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(27, 350);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Forced port:";
            // 
            // forcedPortTB
            // 
            this.forcedPortTB.Location = new System.Drawing.Point(112, 347);
            this.forcedPortTB.Name = "forcedPortTB";
            this.forcedPortTB.Size = new System.Drawing.Size(114, 20);
            this.forcedPortTB.TabIndex = 13;
            this.forcedPortTB.TextChanged += new System.EventHandler(this.forcedPortTB_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(27, 376);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 14;
            this.label4.Text = "Version:";
            // 
            // versionCB
            // 
            this.versionCB.FormattingEnabled = true;
            this.versionCB.Items.AddRange(new object[] {
            "0.3e",
            "0.3x",
            "0.3z R1",
            "0.3.7"});
            this.versionCB.Location = new System.Drawing.Point(112, 373);
            this.versionCB.Name = "versionCB";
            this.versionCB.Size = new System.Drawing.Size(114, 21);
            this.versionCB.TabIndex = 15;
            this.versionCB.SelectedIndexChanged += new System.EventHandler(this.versionCB_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 405);
            this.Controls.Add(this.versionCB);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.forcedPortTB);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.logoPB);
            this.Controls.Add(this.logRTB);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.manconnectlabel);
            this.Controls.Add(this.connectionLabel);
            this.Controls.Add(this.connectBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addressTB);
            this.Controls.Add(this.portTB);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(275, 444);
            this.MinimumSize = new System.Drawing.Size(275, 444);
            this.Name = "MainForm";
            this.Text = "SAMPFox v1.4";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.logoPB)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer CheckForProcessTimer;
        private System.Windows.Forms.TextBox portTB;
        private System.Windows.Forms.TextBox addressTB;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button connectBtn;
        private System.Windows.Forms.Label connectionLabel;
        private System.ComponentModel.BackgroundWorker networkThread;
        private System.Windows.Forms.Label manconnectlabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.RichTextBox logRTB;
        private System.Windows.Forms.PictureBox logoPB;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox forcedPortTB;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox versionCB;
    }
}

