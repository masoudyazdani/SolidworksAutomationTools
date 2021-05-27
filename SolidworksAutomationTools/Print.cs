using EPDM.Interop.epdm;
using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using iText.Layout;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;
using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace SWPrintAndMerge
{

    public partial class SolidworksFactory
    {
        private String[] dwgName;
        public String[] dwgPath;
        public String[] dwgTreeName;
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
                        dwgPath[Array.IndexOf(dwgName, fileName2)] = CurFolder.LocalPath + "\\" + fileName;
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

        private bool MergePDFs(IEnumerable fileNames, string targetPdf)
        {
            bool merged = true;
            PdfDocument document = new PdfDocument(new PdfWriter(targetPdf));
            PdfMerger merger = new PdfMerger(document);
            try
            {
                foreach (string file in fileNames)
                {
                    PdfDocument doc = new PdfDocument(new PdfReader(file));
                    merger.Merge(doc, 1, doc.GetNumberOfPages());
                    doc.Close();
                }
            }
            catch (Exception)
            {
                merged = false;
            }
            finally
            {
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
            }
            return merged;
        }

        private ArrayList Print(string vaultName, string outputPath, bool ignore)
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
                    Console.WriteLine("Vault login error");
                    Console.Read();
                    return null;
                }
            }
            ArrayList pdfs = new ArrayList();
            IEdmFolder5 root = vault.RootFolder;
            //p.dwgName = p.dwgTreeName.Select(q => Path.GetFileNameWithoutExtension(q)).ToArray();
            //p.dwgPath = new string[p.dwgName.Length];
            TraverseFolder(root);
            Console.WriteLine("All available drawing has been extracted.");
            // Save To PDF
            for (int i = 0; i < dwgName.Length; i++)
            {
                String d = dwgPath[i];
                if (d == null)
                    continue;
                // Handle Directory and File Existance
                if (!Directory.Exists(outputPath + "\\" + Path.GetDirectoryName(dwgTreeName[i])))
                {
                    Directory.CreateDirectory(outputPath + "\\" + Path.GetDirectoryName(dwgTreeName[i]));
                }
                string pdfPath = outputPath + "\\" + dwgTreeName[i] + ".pdf";
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

        static ArrayList ExtractPDFs(string path, string[] dwgName)
        {
            ArrayList pdfs = new ArrayList();
            for (int i = 0; i < dwgName.Length; i++)
            {
                if (dwgName[i] == null)
                    continue;
                string file = path + "\\" + dwgName[i] + ".pdf";
                if (File.Exists(file))
                    pdfs.Add(file);
            }
            return pdfs;
        }





        //        public int k = 0;

        //        private String[] getDWGNames()
        //        {
        //            k = 0;
        //            OpenFileDialog fileDialog = new OpenFileDialog();
        //            fileDialog.Filter = "Solidworks Assembly files (*.SLDASM)|*.SLDASM";
        //            fileDialog.Title = "Select Assembly";
        //            if (fileDialog.ShowDialog() == DialogResult.OK)
        //            {
        //                String filePath = fileDialog.FileName;
        //                //TreeExtractor treeExtractor = new TreeExtractor(filePath, swApp);
        //                //return treeExtractor.getFeaturesName();
        //                Console.WriteLine("Opening Assembly...");
        //                ModelDoc2 doc1 = Class1.swApp.OpenDoc(filePath, (int)SolidWorks.Interop.swconst.swDocumentTypes_e.swDocASSEMBLY);

        ////                DrawingDoc d2 = swApp.OpenDoc("")
        //            }
        //            return null;
        //        }

        //        [STAThread]
        //        public static void Main2(string[] args)
        //        {
        //            Program p = new Program();
        //            Console.Write("Please State Vault Name > ");
        //            String vaultName = Console.ReadLine();


        //            // Select Folder
        //            Console.WriteLine("Please select output folder.");
        //            FolderBrowserDialog fbd = new FolderBrowserDialog();
        //            DialogResult result = fbd.ShowDialog();
        //            String c;
        //            ArrayList dwgNames = new ArrayList();
        //            if (result != DialogResult.OK || string.IsNullOrWhiteSpace(fbd.SelectedPath))
        //            {
        //                Console.WriteLine("An error occured in selecting folder.");
        //                c = Console.ReadLine();
        //                return;
        //            }
        //            Console.WriteLine("Output folder is selected successfully.");
        //            string line;
        //            string file = fbd.SelectedPath + "//tree.txt";
        //            if (File.Exists(file))
        //            {
        //                System.IO.StreamReader reader = new System.IO.StreamReader(file);
        //                while ((line = reader.ReadLine()) != null)
        //                {
        //                    dwgNames.Add(line);
        //                }
        //                reader.Close();
        //                p.dwgTreeName = (string[])dwgNames.ToArray(typeof(string));
        //            }
        //            else
        //            {
        //                p.dwgTreeName = p.getDWGNames();
        //                StreamWriter writer = new System.IO.StreamWriter(file);
        //                foreach (string dwg in p.dwgTreeName)
        //                {
        //                    if (dwg!= null)
        //                        writer.WriteLine(dwg);
        //                }
        //                writer.Close();
        //            }
        //            // Ask for Print
        //            string c2 = "";
        //            bool print = false;
        //            while (true)
        //            {
        //                Console.Write("Do you want to print all document (Y: print all; N: just bundle)? (Y/N)");
        //                c2 = Console.ReadLine();
        //                if (c2 == "Y")
        //                {
        //                    print = true;
        //                    break;
        //                }
        //                else if (c2 == "N")
        //                {
        //                    print = false;
        //                    break;
        //                }
        //                else
        //                {
        //                    Console.WriteLine("Invlid Inputs.");
        //                }
        //            }

        //            // Bundle PDFs
        //            MergePDFs((print) ? Print(vaultName, p, fbd.SelectedPath) : ExtractPDFs(fbd.SelectedPath, p.dwgTreeName) , fbd.SelectedPath + "\\Bundle.pdf");

        //            Console.Write("Do you want to Close solidworks? (Y to close)");
        //            c = Console.ReadLine();
        //            if (c.Equals("Y"))
        //            {
        //                Class1.swApp.ExitApp();
        //            }
        //        }
    }
}
