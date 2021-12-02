using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanForms
{
    public partial class Form1 : Form
    {
        List<string> searchPattern = new List<string>();
        public Form1()
        {
            InitializeComponent();
        }

        private void pathBtn_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFile = new OpenFileDialog())
            {
                if (openFile.ShowDialog() == DialogResult.OK)
                {
                    this.pathTB.Text = Path.GetDirectoryName(openFile.FileName);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Search();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            searchPattern.Clear();
            for (int i = 0; i < this.checkedListBox1.CheckedItems.Count; i++)
            {
                searchPattern.Add(this.checkedListBox1.CheckedItems[i].ToString());
            }
        }
        private void Search()
        {
            string name = string.Empty;
            if (this.pathTB.Text != string.Empty)
            {
                this.listBox1.Items.Clear();
                foreach (var item in Directory.GetFiles(this.pathTB.Text))
                {
                    if (searchPattern.Any(x => x.Contains("name")))
                    {
                        name = item.Split('.')[0];
                        if (Path.GetFileName(item.Split('.')[0]).ToLower().Contains(this.nameTB.Text.ToLower()))
                        {
                            this.listBox1.Items.Add(Path.GetFileName(item));
                        }
                    }
                    if (searchPattern.Any(x => x.Contains("creation time")))
                    {
                        if (File.GetCreationTime(item).ToString().Contains(this.nameTB.Text))
                        {
                            this.listBox1.Items.Add(Path.GetFileName(item));
                        }
                    }
                    if (searchPattern.Any(x => x.Contains("ext")))
                    {
                        if (Path.GetFileName(item.Split('.')[1]).ToLower().Contains(this.nameTB.Text.ToLower()))
                        {
                            this.listBox1.Items.Add(Path.GetFileName(item));
                        }
                    }
                    if (searchPattern.Any(x => x.Contains("last write time")))
                    {
                        if (File.GetLastWriteTime(item).ToString().Contains(this.nameTB.Text))
                        {
                            this.listBox1.Items.Add(Path.GetFileName(item));
                        }
                    }
                    if (searchPattern.Any(x => x.Contains("last open time")))
                    {
                        if (File.GetLastAccessTime(item).ToString().Contains(this.nameTB.Text))
                        {
                            this.listBox1.Items.Add(Path.GetFileName(item));
                        }
                    }
                }
                if (this.listBox1.Items.Count == 0)
                {
                    MessageBox.Show("Not found!");
                }
                GC.Collect();
            }
        }

        private void checkedListBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            searchPattern.Clear();
            for (int i = 0; i < this.checkedListBox1.CheckedItems.Count; i++)
            {
                searchPattern.Add(this.checkedListBox1.CheckedItems[i].ToString());
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Hide();
                this.notifyIcon.Visible = true;
            }
        }

        private void MenuShow_Click(object sender, EventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon.Visible = false;
        }

        private void MenuExit_Click(object sender, EventArgs e)
        {
            this.notifyIcon.Visible = false;
            Environment.Exit(0);
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            this.notifyIcon.Visible = false;
        }
    }
}
