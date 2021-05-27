﻿using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections.Generic;
using System.IO;

namespace SWPrintAndMerge
{
    public partial class SolidworksFactory
    {
        private static string DRAWING_TEMPLATE = "temp.slddrw";
        private static string BOM_TEMPLATE = "bom.sldbomtbt";

        private SldWorks swApp = null;        

        public IDictionary<string, string> itemOrderMap = new Dictionary<string, string>();
        public IDictionary<string, Item> pathItemMap = new Dictionary<string, Item>();
        private DrawingDoc doc1;
        private BomTableAnnotation bomAssBOMTable;
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
            if (bomPath == mainPath) { 
                bomAssBOMTable = generateBOMTable(bomPath, swBomType_e.swBomType_Indented);
                mainAssBOMTable = bomAssBOMTable;
            }
            else
            {
                bomAssBOMTable = generateBOMTable(bomPath, swBomType_e.swBomType_Indented);
                mainAssBOMTable = generateBOMTable(mainPath, swBomType_e.swBomType_Indented);
            }
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
            TableAnnotation ann = (TableAnnotation)bomAssBOMTable;
            itemOrderMap.Add("", modelPath);
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

                    if (!pathItemMap.ContainsKey(filePath))
                    {
                        item = new Item();
                        item.referenceQty.Add(itemNo, qty);
                        item.partNo = partNo ;
                        item.path = filePath;
                        item.totalQty = qty;
                        pathItemMap.Add(filePath, item);
                    }
                    else
                    {
                        item = pathItemMap[filePath];
                        item.referenceQty.Add(itemNo,qty);
                        item.totalQty += qty;
                    }

                    itemOrderMap.Add(itemNo, item.path);

                    int lastDotLoc = itemNo.LastIndexOf(".");
                    string parent = "";
                    if (lastDotLoc > 0)
                        parent = itemNo.Substring(0, lastDotLoc);
                }
            }


        }

        public void addWhereUsedTable(Item item)
        {
            string dir = AppContext.BaseDirectory;

            doc1.SetCurrentLayer("Table");
            TableAnnotation tbl = doc1.InsertTableAnnotation2(false, 0.179432255418992, 0.0106792013380164, (int)swBOMConfigurationAnchorType_e.swBOMConfigurationAnchor_BottomLeft, dir + "\\usetable.sldtbt", 2, 1);
            tbl.InsertRow(0, 0);
            tbl.Text[1, 0] = "WTF?!";
            doc1.SetCurrentLayer("");
        }

        public bool isBOMGenerated()
        {
            return bomAssBOMTable != null && mainAssBOMTable != null;
        }

        public void saveExcel(string bomPath, bool includePictures)
        {
            bomAssBOMTable.SaveAsExcel(bomPath, true, includePictures);
        }

    }
}