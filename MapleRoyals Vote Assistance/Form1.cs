using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace MapleRoyalsVoteAssistance
{
    public partial class mainWindow : Form
    {
        string AppName = "MapleRoyals Vote Assistance";
        string GithubLink = "https://github.com/FlafyDev/MapleRoyals-Vote-Assistance";
        MRVoter Voter = new MRVoter();
        bool Closable = false;

        public mainWindow()
        {
            this.ShowInTaskbar = false;
            this.Opacity = 0;
            InitializeComponent();
        }

        private void mainWindow_Load(object sender, EventArgs e)
        {
            notifyIcon.Text = AppName;
            this.Text = AppName + " - Dashboard";

            if (IsApplicationInStartup())
            {
                btnAddStartup.Visible = false;
            }

            Voter.VotingThreadChangedEvent += VotingThreadChanged;
            Voter.VotingThreadTimeLeftEvent += VotingThreadTimeLeft;

            var contextMenu = new ContextMenu();
            var menuItem = new MenuItem();
            contextMenu.MenuItems.AddRange(new MenuItem[] { menuItem });
            menuItem.Index = 0;
            menuItem.Text = "E&xit";
            menuItem.Click += new EventHandler(CloseHandler);

            notifyIcon.ContextMenu = contextMenu;

            if (Properties.Settings.Default.FirstLaunch)
            {
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipText = "Press here or the MapleRoyals tray icon to open the dashboard.";
                notifyIcon.BalloonTipTitle = AppName;
                notifyIcon.ShowBalloonTip(2000);
                Properties.Settings.Default.FirstLaunch = false;
            }

            if (Properties.Settings.Default.Username != "")
            {
                txtBoxEnterUsername.Text = (Properties.Settings.Default.Username);
                btnCheckUsername.PerformClick();
                Unfocus();
            }
        }

        private void VotingThreadChanged(object sender, Thread thread)
        {
            if (thread == null)
            {
                lblVotingThreadCondition.Text = "Off";
            } else
            {
                lblVotingThreadCondition.Text = "On";
            }
        }

        private void VotingThreadTimeLeft(object sender, DateTime timeLeft)
        {
            this.Invoke((MethodInvoker)delegate () {
                lblVoteTimeLeftNumber.Text = timeLeft.ToString();
            });
        }

        private void mainWindow_Shown(object sender, EventArgs e)
        {
            Hide();
            this.ShowInTaskbar = true;
            this.Opacity = 1;
        }

        private void CloseHandler(object sender, EventArgs e)
        {
            Closable = true;
            this.Close();
        }

        private void ShowForm(object sender, EventArgs e)
        {
            ShowMe();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.WindowsShutDown || Closable) return;

            e.Cancel = true;
            Hide();
        }

        private void lblLastUsername_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txtBoxEnterUsername.Text = Voter.Username;
            txtBoxEnterUsername.ForeColor = Color.Green;
        }

        private void txtBoxEnterUsername_TextChanged(object sender, EventArgs e)
        {
            bool valid = Voter.ValidUsername(txtBoxEnterUsername.Text);
            if (valid)
            {
                txtBoxEnterUsername.ForeColor = Color.Black;
            } else
            {
                txtBoxEnterUsername.ForeColor = Color.Red;
            }
        }

        private void btnCheckUsername_Click(object sender, EventArgs e)
        {
            if (Voter.GtopVoting)
            {
                MessageBox.Show("Cannot change usernames while the browser waits for you to vote.", "Can't change usernames", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            var username = txtBoxEnterUsername.Text;
            if (Voter.ChangeUsername(username))
            {
                lblLastUsername.Text = username;
                txtBoxEnterUsername.ForeColor = Color.Green;

                Properties.Settings.Default.Username = username;
                Properties.Settings.Default.Save();
            } else
            {
                txtBoxEnterUsername.ForeColor = Color.Red;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Process.Start(Voter.BaseUrl);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Process.Start(GithubLink);
        }

        private void Unfocus(object sender = null, EventArgs e = null)
        {
            lblLastUsername.Focus();
        }

        private bool IsApplicationInStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                return (string) key.GetValue(AppName) == Application.ExecutablePath.ToString();
            }
        }

        private void AddApplicationToStartup()
        {
            using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
            {
                key.SetValue(AppName, Application.ExecutablePath.ToString());
            }
        }

        private void btnAddStartup_Click(object sender, EventArgs e)
        {
            AddApplicationToStartup();
            btnAddStartup.Visible = false;
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == NativeMethods.WM_SHOWME)
            {
                ShowMe();
            }
            base.WndProc(ref m);
        }

        private void ShowMe()
        {
            Show();
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            // get our current "TopMost" value (ours will always be false though)
            bool top = TopMost;
            // make our form jump to the top of everything
            TopMost = true;
            // set it back to whatever it was
            TopMost = top;
        }
    }
}
