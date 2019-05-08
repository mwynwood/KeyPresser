using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace KeyPresser
{
    public partial class Form1 : Form
    {
        // Lets you do key presses
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        // Hides it from the ALT-TAB menu
        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr window, int index, int value); 
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr window, int index);

        const int GWL_EXSTYLE = -20;
        const int WS_EX_TOOLWINDOW = 0x00000080;
        const int WS_EX_APPWINDOW = 0x00040000;

        public Form1()
        {
            InitializeComponent();
            notifyIcon1.Text = Application.ProductName;
            this.Text = Application.ProductName;
            timer1.Interval = 59000; // 59 Seconds
            timer1.Start();

            // Hides it from the ALT-TAB menu
            int windowStyle = GetWindowLong(Handle, GWL_EXSTYLE);
            SetWindowLong(Handle, GWL_EXSTYLE, windowStyle | WS_EX_TOOLWINDOW);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Press 'Reserved' key
            keybd_event(0xFC, 0x45, 0, 0);
            //Console.Beep();

            // Press Scroll Lock again
            //keybd_event(0x91, 0x45, 0, 0);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false; // Fixes a bug where the icon stays after it's closed
            Application.Exit();
        }

        private void aboutKeyPresserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutBox1 about = new AboutBox1();
            about.StartPosition = FormStartPosition.CenterScreen;
            about.ShowDialog();
        }
    }
}
