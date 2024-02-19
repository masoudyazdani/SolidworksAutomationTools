using EPDM.Interop.epdm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SolidworksAutomationTools
{
    public partial class Printer : Form
    {
        private bool useExternalTreeFile = false;
        private bool treeIsGenerated = false;
        public Printer()
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
            SolidworksFactory swAppFactory = SolidworksFactory.getFactory();
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
            SolidworksFactory swAppFactory = SolidworksFactory.getFactory();

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
            SolidworksFactory swAppFactory = SolidworksFactory.getFactory();

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
                SolidworksFactory swAppFactory = SolidworksFactory.getFactory();

                if (!useExternalTreeFile)
                {
                    if (!checkForOutputFolder() || !checkForBOMTables())
                    {
                        return;
                    }
                    // Extract References
                    Console.WriteLine("Extracting References...");
                    if (!useExternalTreeFile && !treeIsGenerated)
                        swAppFactory.extractReferences(mainAssembly.Text);
                    Console.WriteLine("References Extracted Succesfully");
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
                    //
                    swAppFactory.generateDWGNames();

                    string file = outputFolder.Text + "//tree.txt";
                    System.IO.StreamWriter writer = new System.IO.StreamWriter(file);
                    foreach (string d in swAppFactory.dwgName)
                    {
                        if (d != null || d != "")
                            writer.WriteLine(d);
                    }
                    writer.Close();
                }
                var directories = Directory.GetDirectories(outputFolder.Text).Append(outputFolder.Text);
                foreach (var dir in directories)
                {
                    string file = dir + "//tree.txt";
                    string line;
                    if (File.Exists(file))
                    {
                        swAppFactory.dwgName = new ArrayList();
                        System.IO.StreamReader reader = new System.IO.StreamReader(file);
                        while ((line = reader.ReadLine()) != null)
                        {
                            swAppFactory.dwgName.Add(line);
                        }
                        reader.Close();
                        // Print All Drawing Files Based on selected configuration
                        ArrayList printedFiles = null;
                        if (printChkbox.Checked)
                        {
                            printedFiles = swAppFactory.Print(vault, dir, ignorePrinterFileChkbox.Checked);
                        }

                        if (bundlePDFsChkbox.Checked)
                        {
                            Console.WriteLine("Generating Bundle...");
                            if (printedFiles == null)
                            {
                                printedFiles = SolidworksFactory.ExtractPDFs(dir, swAppFactory.dwgName);
                            }
                            swAppFactory.MergePDFs(printedFiles, dir + "\\Bundle.pdf");
                            Console.WriteLine("Bundle Generated Successfully.");
                        }

                        // Generate BOMs
                        if (generateBOMChkbox.Checked)
                        {
                            swAppFactory.saveExcel(dir + "\\bom.xlsx", includePictureChk.Checked);
                        }
                    }
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

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            EdmViewInfo[] views;

            IEdmVault8 vault = (IEdmVault8)new EdmVault5();
            vault.GetVaultViews(out views, false);
            foreach (EdmViewInfo v in views)
            {
                vaultList.Items.Add(v.mbsVaultName);
            }
            if (views.Length > 0)
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

        private void outputFolder_TextChanged(object sender, EventArgs e)
        {
            SolidworksFactory swAppFactory = SolidworksFactory.getFactory();
            useExternalTreeFile = false;
            try
            {
                var directories = Directory.GetDirectories(outputFolder.Text).Append(outputFolder.Text);
                foreach (var dir in directories)
                {
                    string file = dir + "//tree.txt";
                    string line;
                    if (File.Exists(file))
                    {
                        useExternalTreeFile = true;
                    }
                }
                treeUsagelbl.Visible = useExternalTreeFile;
                // Populate Items in Solidworks Factory
            }
            catch (DirectoryNotFoundException)
            {
                return;
            }

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
