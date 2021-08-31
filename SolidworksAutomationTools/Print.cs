using EPDM.Interop.epdm;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iTextSharp.text;
using iTextSharp.text.pdf;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SolidworksAutomationTools
{

    public partial class SolidworksFactory
    {
        public ArrayList dwgName;
        public bool[] isAvailable; 
        public String[] dwgPath;
//        public String[] dwgTreeName;
        public void generateDWGNames() // It is Behpoo Tailored
        {
            // Generate DWG Names
            dwgName = new ArrayList();
            foreach (KeyValuePair<string, Item> kv in pathItemMap)
            {
                string d  = Path.GetFileNameWithoutExtension(kv.Key).ToUpper();
                //Console.WriteLine(d + "is detected!");
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
                dwgName.Add(d);
                //Console.WriteLine(d + " is ADDED.");
            }
            isAvailable = new bool[dwgName.Count];
            for (int i = 0; i < isAvailable.Length; i++)
                isAvailable[i] = false;
        }

        private void TraverseFolder(IEdmFolder5 CurFolder)
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
                    if (!fileName.EndsWith(".SLDDRW"))
                    {

                        continue;
                    }

                    String fileName2 = fileName.Remove(fileName.Length - 7, 7);

                    if (dwgName.Contains(fileName2))
                    {
                        if (isAvailable != null)
                            isAvailable[dwgName.IndexOf(fileName2)] = true;
                        dwgPath[dwgName.IndexOf(fileName2)] = CurFolder.LocalPath + "\\" + fileName;
                    }
                }

                //Enumerate the sub-folders in the folder
                IEdmPos5 FolderPos = default(IEdmPos5);
                FolderPos = CurFolder.GetFirstSubFolderPosition();
                while (!FolderPos.IsNull)
                {
                    IEdmFolder5 SubFolder = default(IEdmFolder5);
                    SubFolder = CurFolder.GetNextSubFolder(FolderPos);
                    TraverseFolder(SubFolder);
                }

            }
            catch (System.Runtime.InteropServices.COMException ex)
            {
                MessageBox.Show("HRESULT = 0x" + ex.ErrorCode.ToString("X") + ex.Message);
            }
        }

        public bool MergePDFs(IEnumerable fileNames, string targetPdf)
        {
            bool merged = true;
            FileStream stream = new FileStream(targetPdf, FileMode.Create);
            Document document = new Document();
            PdfCopy pdf = new PdfCopy(document, stream);
            document.Open();
            iTextSharp.text.pdf.PdfReader reader = null;
            foreach (string file in fileNames)
            {
                try
                {
                    reader = new iTextSharp.text.pdf.PdfReader(file);
                    pdf.AddDocument(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    merged = false;
                    Console.WriteLine("failed to bundle " + file);
                    Console.WriteLine(ex.StackTrace);
                }
            }                
            if (document != null)
            {
                try
                {
                    document.Close();
                }
                catch (IOException)
                {
                    Console.WriteLine("There is no file to bundle.");
                }

            }
            return merged;
        }

        public ArrayList Print(string vaultName, string outputPath, bool ignore)
        {
            int errors = 0, warnings = 0;
            // Extract DWG files 
            Console.WriteLine("Finding All Available Drawings...");
            IEdmVault7 vault = new EdmVault5();
            if (!vault.IsLoggedIn)
            {
                try
                {
                    vault.LoginAuto(vaultName, 0);
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    MessageBox.Show("Vault login error");
                    return null;
                }
            }
            ArrayList pdfs = new ArrayList();
            IEdmFolder5 root = vault.RootFolder;
            dwgPath = new string[dwgName.Count];
            TraverseFolder(root);
            if (isAvailable != null)
                for (int i = 0; i < dwgName.Count; i++)
                {
                    if (!isAvailable[i])
                    {
                        Console.WriteLine("Drawing for " + dwgName[i] + " is not avialable.");
                    }
                }
            Console.WriteLine("All available drawing has been extracted.");
            // Save To PDF
            for (int i = 0; i < dwgName.Count; i++)
            {
                String d = dwgPath[i];
                if (d == null)
                    continue;
                string pdfPath = outputPath + "\\" + dwgName[i] + ".pdf";
                pdfs.Add(pdfPath);
                if (File.Exists(pdfPath) && ignore)
                {
                    continue;
                }
                // Open Drawing File
                DrawingDoc swModel = (DrawingDoc)swApp.OpenDoc6(d, (int)swDocumentTypes_e.swDocDRAWING, (int)swOpenDocOptions_e.swOpenDocOptions_Silent, "", ref errors, ref warnings);
                ModelDocExtension swModelDocExt = (ModelDocExtension)((ModelDoc2)swModel).Extension;
                String[] sheets = swModel.GetSheetNames();
                foreach (String sheet in sheets)
                {
                    swModel.ActivateSheet(sheet);
                    swModelDocExt.Rebuild((int)swRebuildOptions_e.swRebuildAll);
                }
                PageSetup pageSetup = ((ModelDoc2)swModel).PageSetup;
                pageSetup.Orientation = 2;
                // Printing Drawing
                Console.WriteLine("Printing " + dwgName[i] + "...");
                PrintSpecification printSpec = (PrintSpecification)swModelDocExt.GetPrintSpecification();
                printSpec.ScaleMethod = (int)swPrintSelectionScaleFactor_e.swPrintCurrentSheet;
                printSpec.AddPrintRange(1, sheets.Length);
                swModelDocExt.PrintOut4("Microsoft Print to PDF", pdfPath, printSpec);
                swApp.CloseDoc(d);
            }
            return pdfs;
        }

        public static ArrayList ExtractPDFs(string path, ArrayList dwgName)
        {
            ArrayList pdfs = new ArrayList();
            for (int i = 0; i < dwgName.Count; i++)
            {
                if (dwgName[i] == null)
                    continue;
                string file = path + "\\" + dwgName[i] + ".pdf";
                if (File.Exists(file))
                    pdfs.Add(file);
                else
                    Console.WriteLine(file + " does not exist.");
            }
            return pdfs;
        }

    }
}
