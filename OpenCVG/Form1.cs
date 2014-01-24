using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Emgu.CV.Structure;
using Emgu.CV;
using System.Reflection;

namespace OpenCVG
{
    public partial class Form1 : Form
    {
        FuncForm ff = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            Type t = typeof(Image<Bgr, Byte>);


            MethodInfo[] ms = GetMethodsByName(t, "Draw");

            foreach (var m in ms) {
                try
                {
                    Type[] ta = m.GetParameters().Select(p => p.ParameterType).ToArray();
                    ff = new FuncForm(this, ta);
                    Type bla = ta[1];
                    ff.BuildGui();
                    this.label1.Text += "\nSuccess!!!";
                    break;
                }
                catch (TypeGUINotFoundError ex)
                {
                    this.label1.Text += "\nNo type:" + ex.t.FullName;
                }
                catch (Exception yute)
                {
                    this.label1.Text += "\nFUCK:" + yute.Message;
               
                }
            }

        }

        private MethodInfo[] GetMethodsByName(Type t, string name)
        {
            return t.GetMethods().Where(m => m.Name == name).ToArray();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            


           /* using (Image<Bgr, Byte> img = new Image<Bgr, byte>(400, 200, new Bgr(255, 0, 0)))
            {
                //Create the font
                MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);

                //Draw "Hello, world." on the image using the specific font
                img.Draw("Hello, world", ref f, new Point(10, 80), new Bgr(0, 255, 0));
                   
                this.BackgroundImage = img.ToBitmap();
            }*/
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Text = ", ".Join(ff.GetValues());



           using (Image<Bgr, Byte> img = new Image<Bgr, byte>(400, 200, new Bgr(255, 0, 0)))
           {
               //Create the font
               MCvFont f = new MCvFont(Emgu.CV.CvEnum.FONT.CV_FONT_HERSHEY_COMPLEX, 1.0, 1.0);

               MethodInfo mi = img.GetType().GetMethod("Draw", ff.types);
               mi.Invoke(img, ff.GetValues());

               this.BackgroundImage = img.ToBitmap();
           }

        }
    }


    static class StringJoinExtendor
    {
        public static String Join(this string self, params object[] objs)
        {
            return String.Join(self, objs);
        }
    }

}
