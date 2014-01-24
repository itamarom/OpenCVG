using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace OpenCVG
{
    public partial class Blocker : Form
    {
        public Blocker()
        {
            InitializeComponent();
        }

        private void Blocker_Load(object sender, EventArgs e)
        {

        }

        private void filterTxt_TextChanged(object sender, EventArgs e)
        {
            string filter = filterTxt.Text.ToLower();

            funcsList.Items.Clear();
            if (funcsList.Tag != null)
                funcsList.Items.AddRange(((object[])funcsList.Tag).Where(f => f.ToString().ToLower().Contains(filter)).ToArray());
        }

        private void funcsList_DoubleClick(object sender, EventArgs e)
        {

        }

        private void blocksPanel_Paint(object sender, PaintEventArgs e)
        {

        }
    }

    class Block
    {
        public MethodInfo Method { get; set; }
        public object[] Values { get; set; }

        public object Invoke(object img)
        {
            return Method.Invoke(img, Values);
        }

        public Size AddBlock(Panel container, Point location)
        {

            return Size.Empty;
        }
    }
}
