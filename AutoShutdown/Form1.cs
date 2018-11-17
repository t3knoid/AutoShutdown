using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace AutoShutdown
{
    /// <summary>
    /// http://www.blackwasp.co.uk/DetectPowerEvents.aspx
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private MenuStrip menuStrip1;
        private PictureBox pictureBox1;

        public Form1()
        {
            InitializeComponent();
            SystemEvents.PowerModeChanged += new PowerModeChangedEventHandler(SystemEvents_PowerModeChanged); //https://docs.microsoft.com/en-us/dotnet/api/microsoft.win32.systemevents.powermodechanged
            this.ResumeLayout(false);
            ShowPowerStatus();
        }

        private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
        {
            ShowPowerStatus();
        }
        /// <summary>
        /// Determines and displays the value of the PowerLineStatus property.
        /// https://docs.microsoft.com/en-us/dotnet/api/system.windows.forms.powerstatus?view=netframework-4.7.2
        /// </summary>
        private void ShowPowerStatus()
        {
            // Display the value of the selected property of the PowerStatus type.
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
            if (propval.ToString() == "Offline")
                pictureBox1.Image = global::AutoShutdown.Properties.Resources.Power___Shut_Down_128x128;
            else
                pictureBox1.Image = global::AutoShutdown.Properties.Resources.power_128;
        }
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void InitializeComponent()
        {
            this.Icon = global::AutoShutdown.Properties.Resources.Icon1;
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(167, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // pictureBox1
            // 
            //this.pictureBox1.Image = global::AutoShutdown.Properties.Resources.Power___Shut_Down_128x128;
            this.pictureBox1.Location = new System.Drawing.Point(21, 44);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 129);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(167, 186);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }
}