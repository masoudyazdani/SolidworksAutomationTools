using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;

namespace SolidworksAutomationTools
{
    public partial class SolidworksFactory
    {
        private static string DRAWING_TEMPLATE = "temp.slddrw";
        private static string BOM_TEMPLATE = "bom.sldbomtbt";
        private static string USE_TABLE_TEMPLATE = "usetable.sldtbt";
        private static string QTY_PROPERTY_NAME = "Qty";
        private static string WEIGHT_PROPERTY_NAME = "Weight";
        private static string MATERIAL_PROPERTY_NAME = "Material";


        private SldWorks swApp = null;        

        public IDictionary<string, string> itemOrderMap = new Dictionary<string, string>();
        public IDictionary<string, Item> pathItemMap = new Dictionary<string, Item>();
        private DrawingDoc doc1;
        private BomTableAnnotation bomAssDetailedBOMTable;
        private BomTableAnnotation mainAssBOMTable;

        public SolidworksFactory()
        {
            Console.WriteLine("Initializing Solidworks...");
            swApp = new SldWorks();
            swApp.Visible = true;
            swApp.QuitDoc("temp - Sheet1");
            this.reinitialize();
        }

        public void reinitialize()
        {
            if (doc1 != null)
                swApp.QuitDoc("temp - Sheet1");
            {

            }
            string dir = AppContext.BaseDirectory;
            doc1 = swApp.OpenDoc(dir + "\\" + DRAWING_TEMPLATE, (int)swDocumentTypes_e.swDocDRAWING);
        }

        public void generateBOMTables(string bomPath, string mainPath)
        {
            if ((bomPath == "" && mainPath != "") || (bomPath != "" && mainPath == ""))
            {
                string path = mainPath + bomPath;
                mainPath = path;
                bomPath = path;
            }
            bomAssDetailedBOMTable = generateBOMTable(bomPath, swBomType_e.swBomType_Indented);

            if (mainPath != bomPath)
                mainAssBOMTable = generateBOMTable(mainPath, swBomType_e.swBomType_Indented);
            else
                mainAssBOMTable = bomAssDetailedBOMTable;
        }

        private BomTableAnnotation generateBOMTable(string modelPath, swBomType_e bomType)
        {
            View view = createViewForModel(modelPath);
            string dir = AppContext.BaseDirectory;
            BomTableAnnotation bomTable = view.InsertBomTable4(false,
            1,
            1,
            (int)swBOMConfigurationAnchorType_e.swBOMConfigurationAnchor_BottomRight,
            (int)bomType,
            view.ReferencedConfiguration,
            dir + "\\" + BOM_TEMPLATE,
            false,
            (int)swNumberingType_e.swNumberingType_Detailed,
            false);
            return bomTable;
        }

        private View createViewForModel(string modelPath)
        {
            return doc1.CreateDrawViewFromModelView3(modelPath, "*Front", 0, 0, 0);
        }

        public void extractReferences(string modelPath)
        {            
            TableAnnotation ann = (TableAnnotation)bomAssDetailedBOMTable;
            itemOrderMap.Add("", modelPath.ToLower());
            for (int i = 2; i < ann.RowCount; i++)
            {
                if (!ann.Text[i, 5].Equals("-"))
                {
                    string itemNo = ann.Text[i, 0];
                    string filePath = (ann.Text[i, 6].Trim() + ann.Text[i, 7].Trim()).ToLower();
                    string partNo = ann.Text[i, 1].Trim();
                    int qty = int.Parse(ann.Text[i, 5]);

                    bool isAssembly = File.Exists(filePath + ".sldasm");
                    bool isPart = File.Exists(filePath + ".sldprt");

                    if (isAssembly && !isPart)
                    {
                        filePath += ".sldasm";
                    }
                    else if (isPart && !isAssembly)
                    {
                        filePath += ".sldprt";
                    }
                    else
                    {
                        isAssembly = false;
                        isPart = false;
                        // TODO Should be opened to decide which one is referenced in here
                    }


                    Item item;

                    int lastDotLoc = itemNo.LastIndexOf(".");
                    string parent = "";
                    if (lastDotLoc > 0)
                        parent = itemNo.Substring(0, lastDotLoc);

                    if (!pathItemMap.ContainsKey(filePath))
                    {
                        item = new Item();
                        item.referenceQty.Add(parent, qty);
                        item.partNo = partNo ;
                        item.path = filePath;
                        pathItemMap.Add(filePath, item);
                    }
                    else
                    {
                        item = pathItemMap[filePath];
                        item.referenceQty.Add(parent,qty);
                    }

                    itemOrderMap.Add(itemNo, item.path);

                }
            }
            // Now, total qty will be calculated base on the extracted tree
            foreach (Item i in pathItemMap.Values)
            {
                int qty = calculateTotalQty(i);
                i.totalQty = qty;
            }

        }

        public int calculateTotalQty(Item i)
        {
            int qty = 0;
            foreach(string p in i.referenceQty.Keys)
            {
                if (p != "") {
                    string parentPath = itemOrderMap[p];
                    Item parent = pathItemMap[parentPath];
                    qty += calculateTotalQty(parent) * i.referenceQty[p];
                }
                else
                {
                    return 1;
                }
            }
            return qty;
        }

        public void addWhereUsedTable(Item item)
        {
            string dir = AppContext.BaseDirectory;

            doc1.SetCurrentLayer("Table");
            TableAnnotation tbl = doc1.InsertTableAnnotation2(false, 
                0.179432255418992, 
                0.0106792013380164, 
                (int)swBOMConfigurationAnchorType_e.swBOMConfigurationAnchor_BottomLeft, 
                dir + "\\" + USE_TABLE_TEMPLATE, 2, 1);
            foreach (string k in item.referenceQty.Keys)
            {
                tbl.InsertRow(0, 0);
                tbl.Text[1, 0] = pathItemMap[itemOrderMap[k]].partNo;
            }
            doc1.SetCurrentLayer("");
        }

        public void updateProperies(string path)
        {
            string fileExt = Path.GetExtension(path);
            ModelDoc2 swModel = null;
            if (fileExt == ".sldprt")
                swModel = swApp.OpenDoc(path, (int)swDocumentTypes_e.swDocPART);
            else if  (fileExt == ".sldasm")
                swModel = swApp.OpenDoc(path, (int)swDocumentTypes_e.swDocASSEMBLY);

            CustomPropertyManager swCustPrpMgr = swModel.Extension.CustomPropertyManager[""];
            swCustPrpMgr.Add3(QTY_PROPERTY_NAME, 
                (int)swCustomInfoType_e.swCustomInfoNumber, 
                pathItemMap[path].totalQty.ToString(), 
                (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            swCustPrpMgr.Add3(MATERIAL_PROPERTY_NAME, 
                (int)swCustomInfoType_e.swCustomInfoText, 
                "\"SW-Material\"", 
                (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            swCustPrpMgr.Add3(WEIGHT_PROPERTY_NAME, 
                (int)swCustomInfoType_e.swCustomInfoText, 
                "\"SW-Mass\"", 
                (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
            swApp.CloseDoc(path);
        }

        public bool isBOMGenerated()
        {
            return bomAssDetailedBOMTable != null && mainAssBOMTable != null;
        }

        public void saveExcel(string bomPath, bool includePictures)
        {
            bomAssDetailedBOMTable.SaveAsExcel(bomPath, true, includePictures);
        }

    }
}
