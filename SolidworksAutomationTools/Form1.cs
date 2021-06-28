using EPDM.Interop.epdm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace SolidworksAutomationTools
{
    public partial class Form1 : Form
    {
        private SolidworksFactory swAppFactory;
        private bool useExternalTreeFile = false;
        private bool treeIsGenerated = false;
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
                treeIsGenerated = false;
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
                treeIsGenerated = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string vault = vaultList.Text;
            MethodInvoker myProcessStarter = new MethodInvoker(() =>
            {
                if (!checkForOutputFolder() || !checkForBOMTables())
                {
                    return;
                }
                // Extract References
                if (!useExternalTreeFile && !treeIsGenerated)
                    swAppFactory.extractReferences(mainAssembly.Text);

                treeIsGenerated = true;
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
                // Print All Drawing Files Based on selected configuration
                ArrayList printedFiles = null;
                if (printChkbox.Checked)
                {
                    printedFiles = swAppFactory.Print(vault, outputFolder.Text, ignorePrinterFileChkbox.Checked);
                }

                if (bundlePDFsChkbox.Checked)
                {
                    if (printedFiles == null)
                    {
                        printedFiles = SolidworksFactory.ExtractPDFs(outputFolder.Text, swAppFactory.dwgTreeName);
                    }
                    swAppFactory.MergePDFs(printedFiles, outputFolder.Text + "\\Bundle.pdf");
                }

                // Generate BOMs
                if (generateBOMChkbox.Checked)
                {
                    swAppFactory.saveExcel(outputFolder.Text + "\\bom.xlsx", includePictureChk.Checked);
                }
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void mainAssembly_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
