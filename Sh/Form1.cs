using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Sh
{
    public partial class Form1 : Form
    {
        private const int SW_SHOWMAXIMIZED = 3; // Pinvoke declaration for ShowWindow

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        [DllImport("user32.dll")]
        static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private int childFormNumber = 0;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // #1
            foreach (Control control in this.Controls)
            {
                // #2
                MdiClient client = control as MdiClient;
                if (!(client == null))
                {
                    // #3
                    client.BackColor = Color.LightBlue;
                    // #4
                    break;
                }
            }
            ShKrnl.Api.OpenNewProgram(Environment.CurrentDirectory + "\\progman.exe");
        }

        private void chkNewProgs_Tick(object sender, EventArgs e)
        {
            foreach (string prog in ShKrnl.Api.GetOpenPrograms())
            {
                List<string> corrects = new();
                foreach (Progman prog2 in MdiChildren)
                {
                    if (prog2.pad == prog)
                    {
                        corrects.Add(prog);
                    }
                }
                if (!corrects.Contains(prog))
                {
                    if (!prog.EndsWith("\\Sh.exe") && prog.EndsWith(".exe", StringComparison.CurrentCultureIgnoreCase))
                    {
                        Progman progman = new Progman(prog);
                        progman.pad = prog;
                        progman.MdiParent = this;
                        progman.Show();
                        Process p = Process.Start(prog);
                        Thread.Sleep(400); // Allow the process to open it's window
                        SetParent(p.MainWindowHandle, progman.GetHandle());
                        ShowWindow(p.MainWindowHandle, SW_SHOWMAXIMIZED);
                    }
                }
            }
        }
    }
}
