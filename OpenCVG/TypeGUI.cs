using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;

namespace OpenCVG
{
    delegate Size AddControlsFunc(Control container, object tag, Point location, object initialValue);
    delegate object GetValueFunc(Control container, object tag);

    class TypeGUINotFoundError : Exception
    {
        public Type t { get; set; }

        public TypeGUINotFoundError(Type t)
        {
            this.t = t;
        }
    }

    class TypeGUI
    {
        public AddControlsFunc AddControls { get; set; }
        public GetValueFunc GetValue { get; set; }

        public TypeGUI(AddControlsFunc a, GetValueFunc b)
        {
            this.AddControls = a;
            this.GetValue = b;
        }
    }

    class FuncForm
    {
        public static Dictionary<Type, TypeGUI> guis = new Dictionary<Type, TypeGUI>();

        static FuncForm()
        {
            guis.Add(typeof(int), new TypeGUI(
                delegate(Control container, object tag, Point location, object initialValue)
                {
                    NumericUpDown input = new NumericUpDown();


                    if (initialValue != null)
                        input.Value = (int)initialValue;

                    input.Location = location;
                    input.Tag = tag;
                    input.Minimum = int.MinValue;
                    input.Maximum = int.MaxValue;
                    container.Controls.Add(input);

                    return input.Size;


                }, delegate(Control container, object tag)
                {
                    NumericUpDown input = (NumericUpDown)ControlByTag(container, tag);
                    return input.Value;
                }));

            guis.Add(typeof(MCvFont), new TypeGUI(
                delegate(Control container, object tag, Point location, object initialValue)
                {
                    TextBox hScaleTb = new TextBox();
                    hScaleTb.Location = location;
                    //hScaleTb.Text = initialValue.ToString();
                    hScaleTb.Tag = SubTag(tag, 0);

                    location.Y += hScaleTb.Height + 20;

                    TextBox vScaleTb = new TextBox();
                    vScaleTb.Location = location;
                    //vScaleTb.Text = initialValue.ToString();
                    vScaleTb.Tag = SubTag(tag, 1);

                    container.Controls.Add(hScaleTb);
                    container.Controls.Add(vScaleTb);

                    return hScaleTb.Size + vScaleTb.Size + new Size(0, 20);
                },
                delegate(Control container, object tag)
                {
                    double h = 0, v = 0;

                    try
                    {
                        h = double.Parse(((TextBox)ControlByTag(container, tag, 0)).Text);
                        v = double.Parse(((TextBox)ControlByTag(container, tag, 1)).Text);

                    }
                    catch (Exception e)
                    {
                        //TODO: throw excpetion.
                    }

                    return new MCvFont(FONT.CV_FONT_HERSHEY_COMPLEX, h, v);
                }

                ));

            guis.Add(typeof(double), new TypeGUI(
           delegate(Control container, object tag, Point location, object initialValue)
           {
               if (initialValue == null)
                   initialValue = 0;

               TextBox input = new TextBox();
               input.Location = location;
               input.Text = initialValue.ToString();
               input.Tag = tag;

               return input.Size;
           },
           delegate(Control container, object tag)
           {
               double x = 0;

               try
               {
                   x = double.Parse(((TextBox)ControlByTag(container, tag)).Text);

               }
               catch (Exception e)
               {
                   //TODO: throw excpetion.
               }

               return x;
           }));
            guis.Add(typeof(string), new TypeGUI(
delegate(Control container, object tag, Point location, object initialValue)
{
    if (initialValue == null)
        initialValue = "";

    TextBox input = new TextBox();
    input.Location = location;
    input.Text = initialValue.ToString();
    input.Tag = tag;

    container.Controls.Add(input);

    return input.Size;
},
delegate(Control container, object tag)
{
    return ((TextBox)ControlByTag(container, tag)).Text;
}));

            guis.Add(typeof(Point), new TypeGUI(
               delegate(Control container, object tag, Point location, object initialValue)
               {
                   TextBox x = new TextBox();
                   x.Location = location;
                   //hScaleTb.Text = initialValue.ToString();
                   x.Tag = SubTag(tag, 0);

                   location.Y += x.Height + 20;

                   TextBox y = new TextBox();
                   y.Location = location;
                   //vScaleTb.Text = initialValue.ToString();
                   y.Tag = SubTag(tag, 1);

                   container.Controls.Add(x);
                   container.Controls.Add(y);

                   return x.Size + y.Size + new Size(0, 20);
               },
               delegate(Control container, object tag)
               {

                   int x = 0, y = 0;

                   try
                   {
                       x = int.Parse(((TextBox)ControlByTag(container, tag, 0)).Text);
                       y = int.Parse(((TextBox)ControlByTag(container, tag, 1)).Text);

                   }
                   catch (Exception e)
                   {
                       //TODO: throw excpetion.
                   }

                   return new Point(x, y);
               }));

            guis.Add(typeof(Bgr), new TypeGUI(
   delegate(Control container, object tag, Point location, object initialValue)
   {
       Button btn = new Button();
       btn.Text = "Choose color";
       btn.Location = location;
       btn.Tag = tag;

       btn.Click += new EventHandler(delegate(object sender, EventArgs e)
       {
           ColorDialog cdlg = new ColorDialog();
           cdlg.ShowDialog();

           if (btn.Tag is object[])
           {
               ((object[])btn.Tag)[1] = new Bgr(cdlg.Color);
           }
           else
           {
               btn.Tag = new object[] { btn.Tag, new Bgr(cdlg.Color) };
           }

           btn.BackColor = cdlg.Color;
       });

       container.Controls.Add(btn);

       return btn.Size;
   },
   delegate(Control container, object tag)
   {
       object t = ControlByTag(container, tag).Tag;
       if (t is object[])
       {
           return ((object[])t)[1];
       }
       else
       {
           return t;
       }

   }));

        }

        static String SubTag(object tag, object subtag)
        {
            return tag.ToString() + "_sub_" + subtag.ToString();
        }

        static Control ControlByTag(Control container, object tag, object subtag)
        {
            return ControlByTag(container, SubTag(tag, subtag));
        }

        static Control ControlByTag(Control container, object tag)
        {
            for (int i = 0; i < container.Controls.Count; i++)
            {
                object ct = container.Controls[i].Tag;
                if (ct == null)
                    continue;
                if (ct.ToString() == tag.ToString()
                    || (ct is object[] && ((object[])ct)[0].ToString() == tag.ToString()))
                {
                    return container.Controls[i];
                }
            }

            return null;
        }

        private Type RemoveRefIfExist(Type t)
        {
            if (t.IsByRef)
                return t.Module.GetType(t.FullName.Replace("&", ""));

            return t;
        }


        public Control container;
        public Type[] types;

        public FuncForm(Control container, Type[] types)
        {
            this.container = container;
            this.types = types;
        }
        public void BuildGui()
        {
            Point location = new Point();
            const int MARGIN = 20;

            for (int i = 0; i < types.Length; i++)
            {
                Type t = RemoveRefIfExist(types[i]);

                if (guis.ContainsKey(t))
                {

                    Size s = guis[t].AddControls(this.container, i, location, null);
                    location.Y += s.Height;
                    location.Y += MARGIN;
                }
                else
                {
                    throw new TypeGUINotFoundError(t);
                }
            }
        }

        public object[] GetValues()
        {
            object[] result = new object[types.Length];

            for (int i = 0; i < types.Length; i++)
            {
                Type t = RemoveRefIfExist(types[i]);

                result[i] = guis[t].GetValue(this.container, i);
            }

            return result;
        }

    }
}
