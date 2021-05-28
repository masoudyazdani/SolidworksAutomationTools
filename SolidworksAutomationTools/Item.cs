using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWPrintAndMerge
{
    public class Item
    {
        public int totalQty;
        public string partNo;
        public string path;

        public IDictionary<string, int> referenceQty; // item no -> qty

        public Item()
        {
            referenceQty = new Dictionary<string, int>();
        }

        public bool isAssembly()
        {
            return path.EndsWith(".sldasm");
        }

        public override int GetHashCode()
        {
            return path.GetHashCode();
        }
    }

}
