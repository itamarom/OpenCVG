using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace OpenCVG
{
    delegate Size AddControlsFunc(Control container, object tag, Point location, object initialValue);
    delegate object GetValueFunc(Control container, object tag);

    class TypeGUINotFoundError : Exception
    {
        Type t { get; set; }

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
                delegate(Control container, object tag, Point location,object initialValue)
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
        }

        static Control ControlByTag(Control container, object tag)
        {
            for (int i = 0; i < container.Controls.Count; i++)
            {
                if (container.Controls[i].Tag == tag)
                {
                    return container.Controls[i];
                }
            }

            return null;
        }

        Control container;

        public FuncForm(Control container)
        {
            this.container = container;
        }

        public void BuildGui(Type[] types)
        {
            Point location = new Point();
            const int MARGIN = 20;

            for (int i = 0; i < types.Length; i++)
            {
                if (guis.ContainsKey(types[i]))
                {
                    Size s = guis[types[i]].AddControls(this.container, i, location, null);
                    location.Y += s.Height;
                    location.Y += MARGIN;
                }
                else
                {
                    throw new TypeGUINotFoundError(types[i]);
                }
            }
        }
    }
}
