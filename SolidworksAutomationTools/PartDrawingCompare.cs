using EPDM.Interop.epdm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SolidworksAutomationTools
{
    public partial class PartDrawingCompare : Form
    {

        public PartDrawingCompare()
        {
            InitializeComponent();
        }

        private void printBtn_Click(object sender, EventArgs e)
        {
            IEdmVault21 vault = new EdmVault5() as IEdmVault21;
            if (!vault.IsLoggedIn)
            {
                try
                {
                    vault.LoginAuto(vaultList.SelectedItem.ToString(), 0);
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    MessageBox.Show("Vault login error");
                    return ;
                }
            }
            IEdmSearchResult5 _searchResult;

            IEdmSearch7 _search = vault.CreateUtility(EdmUtility.EdmUtil_Search);
            _search.FindFiles = true;
            _search.SetToken(EdmSearchToken.Edmstok_FindFiles, true);
            _search.SetToken(EdmSearchToken.Edmstok_Recursive, true);
            _search.SetToken(EdmSearchToken.Edmstok_Name, "P[0123456789][0123456789]%.sldprt");
            _search.StartFolderID = vault.RootFolderID;
            _searchResult = _search.GetFirstResult();
            ArrayList results = new ArrayList();
            while (_searchResult != null) {
                string part_drawing = "D" + _searchResult.Name.Substring(1).Split('.')[0] + ".SLDDRW";
                part_drawing = part_drawing.ToUpper();
                results.Add(part_drawing);
                //Console.WriteLine(part_drawing);
                _searchResult = _search.GetNextResult();
            }
            _search.SetToken(EdmSearchToken.Edmstok_Name, "D%.slddrw");
            _search.StartFolderID = vault.RootFolderID;
            _searchResult = _search.GetFirstResult();
            HashSet<string> drawing_set = new HashSet<string>();
            while (_searchResult != null)
            {
                drawing_set.Add(_searchResult.Name.ToUpper());
                //Console.WriteLine(_searchResult.Name);
                _searchResult = _search.GetNextResult();
            }
            foreach (string dwg in results)
                if (!drawing_set.Contains(dwg))
                    dataGridView1.Rows.Add(dwg);
            Console.WriteLine("Done");

        }

        private void PartDrawingCompare_Load(object sender, EventArgs e)
        {
            EdmViewInfo[] views;

            IEdmVault8 vault = (IEdmVault8)new EdmVault5();
            vault.GetVaultViews(out views, false);
            foreach (EdmViewInfo v in views)
            {
                vaultList.Items.Add(v.mbsVaultName);
            }
            vaultList.SelectedIndex = 0;
        }
    }
}
