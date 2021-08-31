using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SolidworksAutomationTools
{
    public partial class ConfigurationBuilder : Form
    {
        private SldWorks swApp;
        public ConfigurationBuilder()
        {
            InitializeComponent();
        }

        private void configAsmSelectBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Solidworks Assembly files (*.SLDASM)|*.SLDASM";
            fileDialog.Title = "Select Config Assembly";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                configAsmTxt.Text = fileDialog.FileName;
            }
        }

        private void createConfig_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Initializing Solidworks...");
            swApp = new SldWorks();
            swApp.Visible = true;
            string fileExt = Path.GetExtension(configAsmTxt.Text).ToLower();
            ModelDoc2 swConfigAsm = null;
            if (fileExt != ".sldasm")
                return;

            swConfigAsm = swApp.OpenDoc(configAsmTxt.Text, (int)swDocumentTypes_e.swDocASSEMBLY);
            swConfigAsm.ConfigurationManager.AddConfiguration2(configTxt.Text, "", "", (int)swConfigurationOptions2_e.swConfigOption_MinFeatureManager | (int)swConfigurationOptions2_e.swConfigOption_SuppressByDefault, "", "", true);
            swConfigAsm.ShowConfiguration(configTxt.Text);
            object[] comps = (object[])((AssemblyDoc)swConfigAsm).GetComponents(false);
            for (int i = 0; i < comps.Length; i++)
            {
                Component2 cmp = (Component2)comps[i];
                if (cmp.IsHidden(false))
                {
                    Component2 mdl = cmp;
                    ArrayList hierarcy = new ArrayList();
                    while (mdl != null)
                    {
                        hierarcy.Add(mdl);
                        mdl = mdl.GetParent();
                    }
                    for (int j = hierarcy.Count - 1; j >= 0; j--)
                    {
                        mdl = (Component2)hierarcy[j];
                        if (mdl.IsHidden(true))
                        {
                            break;
                        }
                        ModelDoc2 doc = mdl.GetModelDoc2();
                        doc.ConfigurationManager.AddConfiguration2(configTxt.Text, "", "", (int)swConfigurationOptions2_e.swConfigOption_MinFeatureManager | (int)swConfigurationOptions2_e.swConfigOption_SuppressByDefault, "", "", true);
                        mdl.ReferencedConfiguration = configTxt.Text;
                    }

                    cmp.SetSuppression2((int)swComponentSuppressionState_e.swComponentSuppressed);
                }
            }
            MessageBox.Show("Configuration Built Successfully", "Configuration Builder", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form2_Load(object sender, EventArgs e)
        {
//            AllocConsole();
        }

        private void configAsmTxt_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //[DllImport("kernel32.dll", SetLastError = true)]
        //[return: MarshalAs(UnmanagedType.Bool)]
        //static extern bool AllocConsole();
    }
}
