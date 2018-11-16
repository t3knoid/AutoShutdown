using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Autoshutdown
{
    /// <summary>
    /// http://www.blackwasp.co.uk/DetectPowerEvents.aspx
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        private TextBox textBox1;

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
            textBox1.Text = propval.ToString();
        }
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Location = new System.Drawing.Point(12, 7);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 13);
            this.textBox1.TabIndex = 0;
            // 
            // Form1
            // 
            this.ClientSize = new System.Drawing.Size(124, 32);
            this.Controls.Add(this.textBox1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}