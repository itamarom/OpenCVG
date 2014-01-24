using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace OpenCVG
{
    static class StringJoinExtendor
    {
        public static String Join(this string self, params object[] objs)
        {
            return String.Join(self, objs);
        }
    }

    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            FuncForm ff = new FuncForm(this);
            ff.BuildGui(new Type[] { typeof(int), typeof(int) });
        }
    }
}
