using CsvHelper;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SolidworksAutomationTools
{

    public partial class CreatePartFromList : Form
    {
        public CreatePartFromList()
        {
            InitializeComponent();
        }

        public static List<PartProperties> ReadPropertiesFromCsv(string path)
        {
            using var reader = new StreamReader(path);
            using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);

            var records = new List<PartProperties>();
            var rows = csv.GetRecords<dynamic>().ToList();

            foreach (var row in rows)
            {
                var partProps = new PartProperties
                {
                    FileName = row.PartNo,
                    Material = row.Material // Assumes "material" is the exact column name in CSV
                };

                foreach (var property in (IDictionary<string, object>)row)
                {
                    if (property.Key != "PartNo" && property.Key != "Material")
                    {
                        partProps.Properties[property.Key] = property.Value.ToString();
                    }
                }

                records.Add(partProps);
            }

            return records;
        }

        public static void UpdateSolidWorksProperties(string basePath, PartProperties part, string template)
        {
            string swFilePath = Path.Combine(basePath, part.FileName + ".SLDPRT");
            try
            {
                File.Copy(template, swFilePath);
            }catch (IOException)
            {

            }
            int _ = 0;
            SldWorks swApp = new SldWorks();
            ModelDoc2 swModel = swApp.OpenDoc6(swFilePath, (int)swDocumentTypes_e.swDocPART, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref _, ref _);

            if (swModel != null)
            {
                PartDoc swPart = swModel as PartDoc;

                // Set material if present
                if (!string.IsNullOrEmpty(part.Material))
                {
                    string[] materialDatabasePath = {@"C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\lang\english\sldmaterials\SolidWorks DIN Materials.sldmat",
                         @"C:\Program Files\SOLIDWORKS Corp\SOLIDWORKS\lang\english\sldmaterials\SolidWorks Materials.sldmat" };  // Modify this
                    foreach (var p in materialDatabasePath) {
                        var mat = "";
                        swPart.SetMaterialPropertyName2("", p, part.Material);
                        var name = swPart.GetMaterialPropertyName2("", out mat);
                        if (name != "")
                        {
                            break;
                        }
                    }
                }

                // Set other properties
                foreach (var property in part.Properties)
                {
                    swModel.Extension.CustomPropertyManager[""].Add3(property.Key, (int)swCustomInfoType_e.swCustomInfoText, property.Value, (int)swCustomPropertyAddOption_e.swCustomPropertyReplaceValue);
                }

                swModel.SaveSilent();
                swApp.CloseDoc(swModel.GetTitle());
            }
            else
            {
                Console.WriteLine($"Unable to open {part.FileName}");
            }
        }

        private void outputFolderSelectBtn_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            if (result == DialogResult.OK)
            {
                outputFolder.Text = fbd.SelectedPath;
            }
            string file = Path.Combine(outputFolder.Text, "list.csv");
            string template = Path.Combine(outputFolder.Text, "Template.SLDPRT");
            if (File.Exists(file))
            {
                List<PartProperties> parts = ReadPropertiesFromCsv(file);

                foreach (var part in parts)
                {
                    UpdateSolidWorksProperties(outputFolder.Text, part, template);
                }
            }
            // Populate Items in Solidworks Factory
        }
    }

    public class PartProperties
    {
        public string FileName { get; set; }
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
        public string Material { get; set; }
    }

}
