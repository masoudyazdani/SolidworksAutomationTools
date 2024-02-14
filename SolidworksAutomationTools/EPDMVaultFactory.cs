using EPDM.Interop.epdm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidworksAutomationTools
{
    class EPDMVaultFactory
    {
        private static EPDMVaultFactory factory;
        private EPDMVaultFactory()
        {

        }

        public static EPDMVaultFactory getFactory()
        {
            if (factory == null)
            {
                factory = new EPDMVaultFactory();
            }
            return factory;
        }

        public IEdmVault7 getVault(string vaultName)
        {
            IEdmVault7 vault = new EdmVault5();
            if (!vault.IsLoggedIn)
            {
                try
                {
                    vault.LoginAuto(vaultName, 0);
                }
                catch (System.Runtime.InteropServices.COMException)
                {
                    System.Windows.Forms.MessageBox.Show("Vault login error");
                    return null;
                }
            }
            return vault;
        }

    }
}
