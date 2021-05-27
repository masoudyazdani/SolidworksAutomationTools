using EPDM.Interop.epdm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SWPrintAndMerge
{
    public partial class Form1 : Form
    {
        private SolidworksFactory swAppFactory;
        private bool useExternalTreeFile = false ;
        public Form1()
        {
            InitializeComponent();
        }

        private bool checkForOutputFolder()
        {
            if (outputFolder.Text == "" || !Directory.Exists(outputFolder.Text))
            {
                MessageBox.Show("Output Directory is not valid.", "Path Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private bool checkForBOMTables()
        {
            if (bomAssemblyTxt.Text == "" && mainAssembly.Text == "")
            {
                MessageBox.Show("BOM Assembly Path and Main Assembly Path is not valid.", "Path Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            else if (!swAppFactory.isBOMGenerated())
            {
                swAppFactory.generateBOMTables(bomAssemblyTxt.Text, mainAssembly.Text);
            }
            return true;
        }


        private void selectMainAssembly_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Solidworks Assembly files (*.SLDASM)|*.SLDASM";
            fileDialog.Title = "Select Assembly";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                mainAssembly.Text = fileDialog.FileName;
            }
            if (swAppFactory.isBOMGenerated())
            {
                swAppFactory.reinitialize();
            }
        }

        private void toPrintSelect_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Solidworks Assembly files (*.SLDASM)|*.SLDASM";
            fileDialog.Title = "Select Assembly";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                bomAssemblyTxt.Text = fileDialog.FileName;
            }

            if (swAppFactory.isBOMGenerated())
            {
                swAppFactory.reinitialize();
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MethodInvoker myProcessStarter = new MethodInvoker(() =>
            {
                if (!checkForOutputFolder() || !checkForBOMTables())
                {
                    return;
                }
                // Extract References
                if (!useExternalTreeFile)
                    swAppFactory.extractReferences(mainAssembly.Text);

                dataGridView1.Invoke((MethodInvoker)delegate
                {
                    foreach (KeyValuePair<string, string> kv in swAppFactory.itemOrderMap)
                    {
                        if (swAppFactory.pathItemMap.ContainsKey(kv.Value))
                        {
                            Item item = swAppFactory.pathItemMap[kv.Value];
                            dataGridView1.Rows.Add(kv.Key, item.partNo, item.totalQty);
                        }
                    }
                });
                // Find All Drawing Files


            });
            myProcessStarter.BeginInvoke(null, null);
        }

        private void outputFolderSelectBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                outputFolder.Text = fbd.SelectedPath;
            }
            string file = outputFolder.Text + "//tree.txt";
            string line;
            if (File.Exists(file))
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(file);
                while ((line = reader.ReadLine()) != null)
                {
                    //                    dwgNames.Add(line);
                }
                reader.Close();
                //              p.dwgTreeName = (string[])dwgNames.ToArray(typeof(string));
                useExternalTreeFile = true;
                
            }
            else
            {
                useExternalTreeFile = false;
            }
            treeUsagelbl.Visible = useExternalTreeFile; 
            // Populate Items in Solidworks Factory

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            swAppFactory = new SolidworksFactory();
            EdmViewInfo[] views;

            IEdmVault8 vault = (IEdmVault8)new EdmVault5();
            vault.GetVaultViews(out views, false);
            foreach (EdmViewInfo v in views)
            {
                vaultList.Items.Add(v.mbsVaultName);
            }
            vaultList.SelectedIndex = 0;

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (!checkForBOMTables())
                return;
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Filter = "Excel File (*.xls)|*.xls";
            fileDialog.Title = "Select Output File";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                swAppFactory.saveExcel(fileDialog.FileName, includePictureChk.Checked);
            }

        }

        private void print_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                ignorePrinterFileChkbox.Enabled = false;
                ignorePrinterFileChkbox.Checked = false;
            }
            else
            {
                ignorePrinterFileChkbox.Enabled = true;

            }
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Checked)
            {
                includePictureChk.Enabled = false;
                includePictureChk.Checked = false;
            }
            else
            {
                includePictureChk.Enabled = true;

            }
        }
    }
}
