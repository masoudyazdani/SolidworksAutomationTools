using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections;
using System.IO;
using System.Linq;

namespace SWPrintAndMerge
{
    class TreeExtractor
    {
        public SolidWorks.Interop.sldworks.SldWorks swApp;
        ArrayList compPaths = new ArrayList();
        ArrayList treePath = new ArrayList();
        public TreeExtractor(String filePath, SldWorks _swApp)
        {
            swApp = _swApp;
            Console.WriteLine("Opening Assembly...");
            ModelDoc2 doc = swApp.OpenDoc(filePath, (int)SolidWorks.Interop.swconst.swDocumentTypes_e.swDocASSEMBLY);
            TreeControlItem root = doc.FeatureManager.GetFeatureTreeRootItem();
            compPaths.Add(doc.GetPathName());
            treePath.Add("");
            Console.WriteLine("Extracting Assembly Tree...");
            TraverseNode(root.GetFirstChild(), "");
            Console.WriteLine("Assembly Tree Extracted Successfully.");
            swApp.CloseDoc(filePath);
        }


        public void TraverseNode(TreeControlItem root, string path)
        {
            TreeControlItem feature = root;
            while (feature != null)
            {
                if (feature.ObjectType == (int)(swTreeControlItemType_e.swFeatureManagerItem_Component))
                {

                    Component cmp = feature.Object;
                    if (cmp.GetSuppression() == (int)swComponentSuppressionState_e.swComponentSuppressed)
                    {
                        feature = feature.GetNext();
                        continue;
                    }
                    Console.WriteLine(cmp.GetPathName());
                    if (!compPaths.Contains(cmp.GetPathName()))
                    {
                        compPaths.Add(cmp.GetPathName());
                        if (cmp.GetPathName().ToLower().EndsWith(".sldasm"))
                        {
                            treePath.Add(path + Path.GetFileNameWithoutExtension(cmp.GetPathName()) + "\\");
                            TraverseNode(feature.GetFirstChild(), path + Path.GetFileNameWithoutExtension(cmp.GetPathName()) + "\\");
                        }
                        else
                        {
                            treePath.Add(path);
                        }
                    }                  
                }else if (feature.ObjectType == (int)swTreeControlItemType_e.swFeatureManagerItem_Feature)
                {
                    Feature f = feature.Object;
                    if (f.GetTypeName2() == "FtrFolder")
                    {
                        TraverseNode(feature.GetFirstChild(), path);
                    }
                }
                
                feature = feature.GetNext();
            }
        }

        public string[] getFeaturesName()
        {
            String[] dwgs = new string[compPaths.Count];
            int j = 0;
            for(int i=0;i<dwgs.Length;i++)
            {
                String d = Path.GetFileNameWithoutExtension((string)compPaths[i]);
                if (d.StartsWith("P"))
                {
                    d = "D" + d.Substring(1);
                }
                else if (d.StartsWith("W") || d.StartsWith("A") || d.StartsWith("F"))
                {
                    d = "D" + d;
                }
                else
                {
                    d = "";
                }
                if (!dwgs.Contains(d) && d != "")
                {
                    dwgs[j] = ((string)treePath[i] + d).ToUpper();
                    j++;
                }
            }
            return dwgs;
        }
    }
}
