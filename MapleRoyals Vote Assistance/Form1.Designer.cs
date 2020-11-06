namespace MapleRoyalsVoteAssistance
{
    partial class mainWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainWindow));
            this.lblVoteTimeLeft = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtBoxEnterUsername = new System.Windows.Forms.TextBox();
            this.lblVoteTimeLeftNumber = new System.Windows.Forms.Label();
            this.btnCheckUsername = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.lblVotingThreadCondition = new System.Windows.Forms.Label();
            this.lblLastUsername = new System.Windows.Forms.LinkLabel();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btnAddStartup = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblVoteTimeLeft
            // 
            resources.ApplyResources(this.lblVoteTimeLeft, "lblVoteTimeLeft");
            this.lblVoteTimeLeft.Name = "lblVoteTimeLeft";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtBoxEnterUsername
            // 
            resources.ApplyResources(this.txtBoxEnterUsername, "txtBoxEnterUsername");
            this.txtBoxEnterUsername.BackColor = System.Drawing.Color.WhiteSmoke;
            this.txtBoxEnterUsername.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtBoxEnterUsername.Name = "txtBoxEnterUsername";
            this.txtBoxEnterUsername.TextChanged += new System.EventHandler(this.txtBoxEnterUsername_TextChanged);
            // 
            // lblVoteTimeLeftNumber
            // 
            resources.ApplyResources(this.lblVoteTimeLeftNumber, "lblVoteTimeLeftNumber");
            this.lblVoteTimeLeftNumber.Name = "lblVoteTimeLeftNumber";
            // 
            // btnCheckUsername
            // 
            resources.ApplyResources(this.btnCheckUsername, "btnCheckUsername");
            this.btnCheckUsername.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCheckUsername.FlatAppearance.BorderSize = 0;
            this.btnCheckUsername.Name = "btnCheckUsername";
            this.btnCheckUsername.UseVisualStyleBackColor = false;
            this.btnCheckUsername.Click += new System.EventHandler(this.btnCheckUsername_Click);
            this.btnCheckUsername.Enter += new System.EventHandler(this.Unfocus);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // lblVotingThreadCondition
            // 
            resources.ApplyResources(this.lblVotingThreadCondition, "lblVotingThreadCondition");
            this.lblVotingThreadCondition.Name = "lblVotingThreadCondition";
            // 
            // lblLastUsername
            // 
            resources.ApplyResources(this.lblLastUsername, "lblLastUsername");
            this.lblLastUsername.LinkColor = System.Drawing.Color.Black;
            this.lblLastUsername.Name = "lblLastUsername";
            this.lblLastUsername.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblLastUsername_LinkClicked);
            // 
            // notifyIcon
            // 
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.BalloonTipClicked += new System.EventHandler(this.ShowForm);
            this.notifyIcon.Click += new System.EventHandler(this.ShowForm);
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::MapleRoyalsVoteAssistance.Properties.Resources.MRoyalsBanner;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Name = "label4";
            // 
            // pictureBox2
            // 
            resources.ApplyResources(this.pictureBox2, "pictureBox2");
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::MapleRoyalsVoteAssistance.Properties.Resources.GithubIcon;
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // btnAddStartup
            // 
            resources.ApplyResources(this.btnAddStartup, "btnAddStartup");
            this.btnAddStartup.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnAddStartup.FlatAppearance.BorderSize = 0;
            this.btnAddStartup.Name = "btnAddStartup";
            this.btnAddStartup.UseVisualStyleBackColor = false;
            this.btnAddStartup.Click += new System.EventHandler(this.btnAddStartup_Click);
            this.btnAddStartup.Enter += new System.EventHandler(this.Unfocus);
            // 
            // mainWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.btnAddStartup);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblLastUsername);
            this.Controls.Add(this.lblVotingThreadCondition);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnCheckUsername);
            this.Controls.Add(this.lblVoteTimeLeftNumber);
            this.Controls.Add(this.txtBoxEnterUsername);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblVoteTimeLeft);
            this.Name = "mainWindow";
            this.Load += new System.EventHandler(this.mainWindow_Load);
            this.Shown += new System.EventHandler(this.mainWindow_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblVoteTimeLeft;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtBoxEnterUsername;
        private System.Windows.Forms.Label lblVoteTimeLeftNumber;
        private System.Windows.Forms.Button btnCheckUsername;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblVotingThreadCondition;
        private System.Windows.Forms.LinkLabel lblLastUsername;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Button btnAddStartup;
    }
}

