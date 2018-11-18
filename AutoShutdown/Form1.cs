using Microsoft.Win32;
using System;
using System.Reflection;
using System.Windows.Forms;

namespace AutoShutdown
{
    /// <summary>
    /// http://www.blackwasp.co.uk/DetectPowerEvents.aspx
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private MenuStrip menuStrip1;
        private PictureBox pictureBox1;
        private Logger logger;
        private ToolStripMenuItem fileStripMenuItem;
        private ToolStripMenuItem minimizeToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private NotifyIcon notifyIcon1;
        private System.ComponentModel.IContainer components;
        private bool Offline;
        private String status = "Offline";

        public Form1()
        {
            logger = new Logger(String.Format("{0}.log", System.Reflection.Assembly.GetEntryAssembly().Location));
            logger.Info("Starting AutoShutdown.","");
            SetPowerStatus();
            InitializeComponent();
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged); //https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.systemevents.powermodechanged
            this.ResumeLayout(false);
            ShowPowerStatus();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            SetPowerStatus();
            ShowPowerStatus();
            if (Offline)
            {
                logger.Info("Shutting down Windows.");
                Utility.Shutdown();
            }
        }
        /// <summary>
        /// Sets power status
        /// </summary>
        private void SetPowerStatus()
        {
            // https://docs.microsoft.com/en-us/dotnet/api/system.windows.powerlinestatus?view=netframework-4.7.2
            Type t = typeof(System.Windows.Forms.PowerStatus);
            PropertyInfo[] pi = t.GetProperties();
            PropertyInfo prop = null;
            for (int i = 0; i < pi.Length; i++)
                if (pi[i].Name == "PowerLineStatus")
                {
                    prop = pi[i];
                    break;
                }

            object propval = prop.GetValue(SystemInformation.PowerStatus, null);
            logger.Info(String.Format("PowerLineStatus is set to {0}", propval.ToString()));
            if (propval.ToString() == "Offline")
            {
                status = "Offline";
                Offline = true;
            }
            else
            {
                status = "Online";
                Offline = false;
            }
        }
        /// <summary>
        /// Displays power indicator
        /// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.powerstatus?view=netframework-4.7.2
        /// </summary>
        private void ShowPowerStatus()
        {
            if (Offline)
            {
                // Show shutdown image
                pictureBox1.Image = global::AutoShutdown.Properties.Resources.Power___Shut_Down_128x128;
            }
            else
            {
                // Show power on image
                pictureBox1.Image = global::AutoShutdown.Properties.Resources.power_128;
            }
            this.notifyIcon1.BalloonTipText = status;
            this.notifyIcon1.Text = status;
            notifyIcon1.ShowBalloonTip(500);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(21, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 129);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(167, 24);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileStripMenuItem
            // 
            this.fileStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minimizeToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileStripMenuItem.Name = "fileStripMenuItem";
            this.fileStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileStripMenuItem.Text = "File";
            // 
            // minimizeToolStripMenuItem
            // 
            this.minimizeToolStripMenuItem.Name = "minimizeToolStripMenuItem";
            this.minimizeToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.minimizeToolStripMenuItem.Text = "Minimize";
            this.minimizeToolStripMenuItem.Click += new System.EventHandler(this.minimizeToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = global::AutoShutdown.Properties.Resources.Icon1;
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(167, 186);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = global::AutoShutdown.Properties.Resources.Icon1;
            this.MainMenuStrip = this.menuStrip1;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void MinimizeToTray()
        {
            notifyIcon1.Visible = true;
            this.ShowInTaskbar = false;
            this.Hide();
        }
        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MinimizeToTray();
            notifyIcon1.ShowBalloonTip(500);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("AutoShutdown by Frank Refol\n© 2018 Frank Refol All Rights Reserved","About");
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            logger.Info("Exiting application");
            this.notifyIcon1.Visible = false;
            System.Windows.Forms.Application.Exit();
        }

        private void notifyIcon1_MouseClick(object sender, MouseEventArgs e)
        {
            this.ShowInTaskbar = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Normal;
            this.Show();
        }
    }
}