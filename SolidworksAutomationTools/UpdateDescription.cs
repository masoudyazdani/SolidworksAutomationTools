using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EPDM.Interop.epdm;

namespace SolidworksAutomationTools
{
    public partial class UpdateDescription : Form
    {
        public class PropertiesFile
        {
            public int[] name;
            public ArrayList[] value;
            public ArrayList files;
        }


        public UpdateDescription()
        {
            InitializeComponent();
        }

        private PropertiesFile getPropertiesFile(IEdmVault7 vault)
        {
            // Get File
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Tab Delimited File (*.txt)|*.TXT";
            fileDialog.Title = "Select Properties File";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                if (File.Exists(fileDialog.FileName))
                {
                    var desiredProperties = new PropertiesFile();
                    desiredProperties.files = new ArrayList();
                    //
                    System.IO.StreamReader reader = new System.IO.StreamReader(fileDialog.FileName);
                    string line = reader.ReadLine();
                    if (line != null) {
                        string[] headers = line.Split('\t');
                        desiredProperties.name = new int[headers.Length - 1];
                        desiredProperties.value = new ArrayList[headers.Length - 1];

                        IEdmVariableMgr5 VariableMgr = default(IEdmVariableMgr5);

                        //
                        for (int i=0;i<desiredProperties.name.Length;i++) {
                            VariableMgr = (IEdmVariableMgr5)vault;
                            desiredProperties.name[i] = VariableMgr.GetVariable(headers[i+1]).ID;
                            desiredProperties.value[i] = new ArrayList();
                        }
                    }
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] data = line.Split('\t');
                        if (data.Length <= desiredProperties.name.Length)
                        {
                            continue;
                        }
                        desiredProperties.files.Add(data[0]);
                        for (int i = 0; i < desiredProperties.name.Length; i++)
                        {
                            desiredProperties.value[i].Add(data[i+1].Replace("\"", ""));
                        }
                        }
                        reader.Close();
                    return desiredProperties; 
                }
            }
            return null;
        }

        private void addBtn_Click(object sender, EventArgs e)
        {
            var pdmFactory = EPDMVaultFactory.getFactory();
            var vault = pdmFactory.getVault(comboBox1.Text);
            IEdmFolder5 root = vault.GetFolderFromPath(vault.RootFolderPath);
            var desiredProperties = getPropertiesFile(vault);
            if (desiredProperties == null)
            {
                return;
            }

            //
            IEdmBatchUpdate2 Update = default(IEdmBatchUpdate2);
            Update = (IEdmBatchUpdate2)vault.CreateUtility(EdmUtility.EdmUtil_BatchUpdate);

            //
            IEdmSearch5 Search = default(IEdmSearch5);
            Search = (IEdmSearch5)vault.CreateUtility(EdmUtility.EdmUtil_Search);
            var files = desiredProperties.files;
            for (var i = 0; i < files.Count; i++)
            {
                Search.Clear();
                Search.FileName = (string)files[i] + ".%";
                Search.FindFiles = true;
                Search.FindFolders = false;
                Search.Recursive = true;
                Search.FindLockedFiles = true;
                Search.FindUnlockedFiles = false;

                IEdmSearchResult5 Result = default(IEdmSearchResult5);
                Result = Search.GetFirstResult();
                if ((Result != null))
                {
                    IEdmFolder5 aFolder;
                    FileInfo fi = new FileInfo(Result.Path);
                    if (!fi.IsReadOnly)
                    {
                        IEdmFile7 aFile = (IEdmFile7)vault.GetFileFromPath(Result.Path, out aFolder);
                        IEdmEnumeratorVariable7 enumVar = (IEdmEnumeratorVariable7)aFile.GetEnumeratorVariable();
                        for (var j = 0; j < desiredProperties.name.Length; j++)
                        {
                            var nameId = desiredProperties.name[j];
                            var value = desiredProperties.value[j][i];
                            Console.WriteLine(Result.Path + " > " + nameId + " = " + value);
                            Update.SetVar(Result.ID, nameId, value, "", (int)(EdmBatchFlags.EdmBatch_AllConfigs | EdmBatchFlags.EdmBatch_RefreshPreview | EdmBatchFlags.EdmBatch_UpdateVarIfNotPartOfCard));
                        }
                        Console.WriteLine(Result.Path + " is updated.");
                    }
                }
            }
            EdmBatchError2[] Errors = null;
            int errorSize = 0;
            errorSize = Update.CommitUpdate(out Errors, null);
            //Display any errors 
            string Msg = "Card variables updated.";

            string ErrName = null;
            string ErrDesc = null;
            string FileName = null;

            int Lo = 0;
            Lo = Errors.GetLowerBound(0);

            int Hi = 0;
            Hi = Errors.GetUpperBound(0);

            IEdmVault9 vault9;
            vault9 = (IEdmVault9)vault;
            EdmObjectInfo[] ppoObjects = null;

            while (Lo < Hi - 1)
            {
                ppoObjects[Lo].meType = EdmObjectType.EdmObject_File;
                Lo = Lo + 1;
            }

            vault9.GetObjects(ref ppoObjects);

            while (Lo < Hi - 1)
            {
                if ((Errors[Lo].mlFileID > 0))
                {
                    IEdmFile6 File = default(IEdmFile6);
                    int ID;
                    ID = (int)ppoObjects[Lo].moObjectID;
                    if (ppoObjects[Lo].meType == EdmObjectType.EdmObject_File)
                    {
                        if (ID == Errors[Lo].mlFileID)
                        {
                            File = (IEdmFile6)ppoObjects[Lo].mpoObject;
                            FileName = File.Name;
                        }
                    }
                }

                vault.GetErrorString(Errors[Lo].mlErrorCode, out ErrName, out ErrDesc);

                Msg = Msg + "\n" + ErrName + " occure for " + FileName;

                Lo = Lo + 1;

            }
            Console.WriteLine(Msg);
        }

        private void UpdateDescription_Load(object sender, EventArgs e)
        {
            EdmViewInfo[] views;

            IEdmVault8 vault = (IEdmVault8)new EdmVault5();
            vault.GetVaultViews(out views, false);
            foreach (EdmViewInfo v in views)
            {
                comboBox1.Items.Add(v.mbsVaultName);
            }
            if (views.Length > 0)
                comboBox1.SelectedIndex = 0;
        }
    }
}
