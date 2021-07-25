using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
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
    public partial class Form3 : Form
    {
        private SldWorks swApp;
        private HashSet<string> collapseItems;
        public Form3()
        {
            InitializeComponent();
        }

        private void configAsmSelectBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Solidworks Drawing files (*.SLDDRW)|*.SLDDRW";
            fileDialog.Title = "Select Main Drawing";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                configAsmTxt.Text = fileDialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Console.WriteLine("Initializing Solidworks...");
            swApp = new SldWorks();
            swApp.Visible = true;
            string fileExt = Path.GetExtension(configAsmTxt.Text).ToLower();
            ModelDoc2 swDrawing = null;
            if (fileExt != ".slddrw")
                return;

            swDrawing = swApp.OpenDoc(configAsmTxt.Text, (int)swDocumentTypes_e.swDocDRAWING);
            Feature swFeat = (Feature)swDrawing.FirstFeature();
            while((swFeat != null))
            {
                if (swFeat.GetTypeName() == "BomFeat")
                {
                    BomFeature feat = swFeat.GetSpecificFeature2();
                    object[] tables  = (object[])feat.GetTableAnnotations(); 
                    foreach (BomTableAnnotation a  in tables)
                    {
                        TableAnnotation ann = (TableAnnotation)a;
                        for (int i = 0; i < ann.RowCount; i++)
                        {
                            string partNo = ann.Text[i, 1].Trim();
                            if (collapseItems.Contains(partNo))
                            {
                                Console.WriteLine(ann.Text[i, 1]);
                                a.Collapse(1, i);
                            }
                        }
                            return;
                    }
                }
                swFeat = swFeat.GetNextFeature();
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {

            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "List files (*.txt)|*.txt";
            fileDialog.Title = "Select Collapse List";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                collapeTxt.Text = fileDialog.FileName;
                collapseItems = new HashSet<string>();
                System.IO.StreamReader reader = new System.IO.StreamReader(collapeTxt.Text);
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    collapseItems.Add(line);
                }
                reader.Close();

            }
        }
    }
}
