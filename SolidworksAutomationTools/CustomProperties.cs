using EPDM.Interop.epdm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidworksAutomationTools
{
    public partial class CustomProperties : Form
    {
        public CustomProperties()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string folder;
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                folder = fbd.SelectedPath;
            }else
            {
                return;
            }
            string customPrpFile;
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text File (*.txt)|*.txt";
            fileDialog.Title = "Select Custom Properties List File";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                customPrpFile  = fileDialog.FileName;
            }
            else
            {
                return;
            }


            string line;
            List<string> customProperties = new List<string>();
            System.IO.StreamReader reader = new System.IO.StreamReader(customPrpFile);
            while ((line = reader.ReadLine()) != null)
            {
                customProperties.Add(line);
            }
            reader.Close();
            SolidworksFactory swAppFactory = SolidworksFactory.getFactory();
            string[] files = swAppFactory.getAllFile(folder, comboBox1.Text);
            foreach (string f in files)
            {
                Console.WriteLine("Updating Properties of " + f);
                swAppFactory.updateProperies(f, customProperties.ToArray());
            }
            Console.WriteLine("Done.");
        }
    }
}
