using EPDM.Interop.epdm;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace SolidworksAutomationTools
{
    public partial class SolidworksFactory
    {
        private static string DRAWING_TEMPLATE = "temp.slddrw";
        private static string BOM_TEMPLATE = "bom.sldbomtbt";
        private static string USE_TABLE_TEMPLATE = "usetable.sldtbt";


        private SldWorks swApp = null;

        public IDictionary<string, string> itemOrderMap = new Dictionary<string, string>();
        public IDictionary<string, Item> pathItemMap = new Dictionary<string, Item>();
        private DrawingDoc doc1;
        private BomTableAnnotation bomAssDetailedBOMTable;
        private BomTableAnnotation mainAssBOMTable;
        private static SolidworksFactory factory;

        public static SolidworksFactory getFactory()
        {
            if (factory == null)
            {
                factory = new SolidworksFactory();
            }
            return factory;
        }

        private SolidworksFactory()
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
            Console.WriteLine("Generating BOM Table...");
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
            Console.WriteLine("BOM Table Generated Successfully.");
            return bomTable;
        }

        private View createViewForModel(string modelPath)
        {
            return doc1.CreateDrawViewFromModelView3(modelPath, "*Front", 0, 0, 0);
        }

        public void extractReferences(string modelPath)
        {
            itemOrderMap.Clear();
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
                        item.partNo = partNo;
                        item.path = filePath;
                        pathItemMap.Add(filePath, item);
                    }
                    else
                    {
                        item = pathItemMap[filePath];
                        //item.referenceQty.Add(parent,qty);
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
            foreach (string p in i.referenceQty.Keys)
            {
                if (p != "")
                {
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

        //public void updateDescriptionFromConfig(string vaultName)
        //{   IEdmBomMgr bomMgr;
        //    IEdmBomView bomView = null;
        //    int arrSize = 0;
        //    EdmBomVersion[] ppoVersions = null;
        //    int i = 0;
        //    int j = 0;
        //    int id = 0;
        //    string str = "";
        //    string verstr = "";
        //    int verArrSize = 0;

        //    foreach (string path in array){
        //        string fileExt = Path.GetExtension(path).ToLower();
        //        string fileName = Path.GetFileNameWithoutExtension(path).ToLower();
        //        IEdmFolder5 folder;
        //        var file = (IEdmFile7)vault.GetFileFromPath(path, out folder);
        //        // Get named BOMs and their versions for the selected file
        //        bomMgr = (IEdmBomMgr)vault.CreateUtility(EdmUtility.EdmUtil_BomMgr);
        //        EdmBomLayout[] ppoRetLayouts = null;
        //        EdmBomLayout ppoRetLayout = default(EdmBomLayout);
        //        bomMgr.GetBomLayouts(out ppoRetLayouts);
        //        i = 0;
        //        arrSize = ppoRetLayouts.Length;
        //        while (i < arrSize)
        //        {
        //            ppoRetLayout = ppoRetLayouts[i];
        //            if (ppoRetLayout.mbsLayoutName == "BOM")
        //            {
        //                bomView = file.GetComputedBOM(ppoRetLayout.mbsLayoutName, file.CurrentVersion, "", (int)EdmBomFlag.EdmBf_ShowSelected);
        //                break;
        //            }
        //            i = i + 1;
        //        }
        //        if (bomView != null)
        //        {
        //            string bomFile = "c:\\users\\masoud\\desktop\\SavedBOM.csv";
        //            ((IEdmBomView3)bomView).SaveToCSV(, false);

        //        }
        //        return;
        //        //                var variables = file.GetEnumeratorVariable();
        //        //              object par;
        //        //              variables.GetVar("Descriptions", "@", out par);
        //        //                Console.WriteLine(par);
        //    }
        //}

            public void updateProperies(string path, string[] customProperties)
        {
            string fileExt = Path.GetExtension(path).ToLower();
            ModelDoc2 swModel = null;
            if (fileExt == ".sldprt")
            {
                swModel = swApp.OpenDoc(path, (int)swDocumentTypes_e.swDocPART);
            }
            else if (fileExt == ".sldasm")
            {
                swModel = swApp.OpenDoc(path, (int)swDocumentTypes_e.swDocASSEMBLY);
            }
            
            if (swModel != null)
            {
                CustomPropertyManager swCustPrpMgr = swModel.Extension.CustomPropertyManager[""];
                foreach (string s in customProperties)
                {
                    if (s.Trim().StartsWith("#")){
                        continue;
                    }
                    string[] v = s.Split(',');
                    int res = swCustPrpMgr.Add3(v[0],
                        int.Parse(v[1]), v[2], (int)swCustomPropertyAddOption_e.swCustomPropertyDeleteAndAdd);
                    Console.WriteLine("property " + v[0] + " was added with result code = " + res);
                }
            }
            swModel.Save();
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

        private void TraverseFolder(ref List<string> arr, IEdmFolder5 CurFolder, string ext)
        {
            try
            {
                //Enumerate the files in the folder
                IEdmPos5 FilePos = default(IEdmPos5);
                FilePos = CurFolder.GetFirstFilePosition();
                IEdmFile5 file = default(IEdmFile5);
                while (!FilePos.IsNull)
                {
                    file = CurFolder.GetNextFile(FilePos);
                    String fileName = file.Name.ToUpper();
                    if (!fileName.EndsWith(ext.ToUpper()))
                    {
                        continue;
                    }
                    arr.Add(CurFolder.LocalPath + "\\" + fileName);
                }

                //Enumerate the sub-folders in the folder
                IEdmPos5 FolderPos = default(IEdmPos5);
                FolderPos = CurFolder.GetFirstSubFolderPosition();
                while (!FolderPos.IsNull)
                {
                    IEdmFolder5 SubFolder = default(IEdmFolder5);
                    SubFolder = CurFolder.GetNextSubFolder(FolderPos);
                    TraverseFolder(ref arr, SubFolder, ext);
                }

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                System.Windows.Forms.MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + ex.Message);
            }

        }


        public string[] getAllFile(string path, string extension)
        {
            IEdmVault7 vault = new EdmVault5();
            EdmViewInfo[] views;
            ((IEdmVault8)vault).GetVaultViews(out views, false);
            foreach (EdmViewInfo v in views)
            {
                if (path.ToLower().StartsWith(v.mbsPath.ToLower()))
                {
                    try
                    {
                        vault.LoginAuto(v.mbsVaultName, 0);
                    }
                    catch (System.Runtime.InteropServices.COMException)
                    {
                        System.Windows.Forms.MessageBox.Show("Vault login error");
                        return null;
                    }

                }
            }

            IEdmFolder5 root = vault.GetFolderFromPath(path);
            List<string> output = new List<string>();
            TraverseFolder(ref output, root, extension);
            return output.ToArray();

        }
    }
}
