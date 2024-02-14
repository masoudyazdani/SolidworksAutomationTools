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

namespace SolidworksAutomationTools
{
    public partial class ClearLocalFile : Form
    {
        HashSet<string> files;
        public ClearLocalFile()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "List files (*.txt)|*.txt";
            fileDialog.Title = "Select Collapse List";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                localFileTxt.Text = fileDialog.FileName;
                files = new HashSet<string>();
                System.IO.StreamReader reader = new System.IO.StreamReader(localFileTxt.Text);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(',');
                    string d = data[4] + "\\" + data[1];
                    files.Add(d);
                    Console.WriteLine(d);
                }
                reader.Close();

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            foreach (string f in files)
                try
                {
                    File.Delete(f);
                }catch(Exception ex)
                {
                    Console.WriteLine(f + " is not deleted");
                    Console.WriteLine(ex.Message);
                }
        }
    }
}
