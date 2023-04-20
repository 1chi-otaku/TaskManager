using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace TaskManager
{
    public partial class Form1 : Form
    {
        Process[] processes = Process.GetProcesses();
        Thread thread;
        public Form1()
        {
            InitializeComponent();
            button2.Enabled = false;
            Refresh(this, EventArgs.Empty);
            thread = new Thread(new ThreadStart(Timer));
            thread.Start();
        }

        private void Refresh(object sender, EventArgs e)
        {
            processes = Process.GetProcesses();
            listBox1.Items.Clear();
            
            foreach (var item in processes)
            {
                listBox1.Items.Add(item.ProcessName);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex != -1)
            {
                processes[listBox1.SelectedIndex].Close();
                processes[listBox1.SelectedIndex].Kill();
            }
            Refresh(this, EventArgs.Empty);
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(listBox1.SelectedIndex != -1)
            {
                button2.Enabled = true;
            }
            else
                button2.Enabled=false;
        }

        private void Timer()
        {
            while (true)
            {
                System.Threading.Thread.Sleep(10000);
                Refresh(this, EventArgs.Empty);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            thread.Abort();
            Application.Exit();
        }
    }
}
