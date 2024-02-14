using System;
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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Printer printer = new Printer();
            printer.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            UpdateDescription form = new UpdateDescription();
            form.Show();
            this.Hide();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var form = new ClearLocalFile();
            form.Show();
            this.Hide();

        }

        private void button6_Click(object sender, EventArgs e)
        {
            var form = new PartDrawingCompare();
            form.Show();
            this.Hide();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            var form = new AutoCollapse();
            form.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var form = new CustomProperties();
            form.Show();
            this.Hide();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            var form = new ConfigurationBuilder();
            form.Show();
            this.Hide();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            var form = new CreatePartFromList();
            form.Show();
            this.Hide();
        }
    }
}
