using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesMngmt.Invoice
{
    public partial class RichTextBox : Form
    {
        public RichTextBox()
        {
            InitializeComponent();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void RichTextBox_Load(object sender, EventArgs e)
        {
            richTextBox1.AllowDrop = true;
            richTextBox1.DragDrop += richTextBox1_dropdown;
        }

        private void richTextBox1_dropdown(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);

            if (data != null) {

                var filename = data as string[];
                if (filename.Length > 0) {

                    richTextBox1.LoadFile(filename[1]);
                }

            }
        }
    }
}
