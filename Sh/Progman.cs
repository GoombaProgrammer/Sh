using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sh
{
    public partial class Progman : Form
    {
        private bool dragging = false;
        public string pad = "";
        private Point dragCursorPoint;
        private Point dragFormPoint;
        public Progman(string appName)
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true); // This is to avoid visual artifacts
            label1.Text = appName;
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShKrnl.Api.CloseProgram(pad);
            Close();
        }

        private void minimizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void maximizeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WindowState &= ~FormWindowState.Maximized;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            contextMenuStrip1.Show(Cursor.Position);
        }

        public IntPtr GetHandle()   // Get screen for window
        {
            return panel2.Handle;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            dragging = true;
            dragCursorPoint = Cursor.Position;
            dragFormPoint = this.Location;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging)
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint));
                this.Location = Point.Add(dragFormPoint, new Size(dif));
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            dragging = false;
        }
    }
}
