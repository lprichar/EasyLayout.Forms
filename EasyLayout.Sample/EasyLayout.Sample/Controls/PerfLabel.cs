using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EasyLayout.Sample.Controls
{
    public class PerfLabel : Label
    {
        public int Measures { get; private set; } = 0;
        public int Layouts { get; private set; } = 0;
        private string _text;

        public PerfLabel()
        {
            this.MeasureInvalidated += OnMeasureInvalidated;
        }

        private void OnMeasureInvalidated(object sender, EventArgs eventArgs)
        {
            Measures++;
        }

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            Measures++;
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            Layouts++;
            base.OnSizeAllocated(width, height);
        }

        public void PrintStats()
        {
            if (_text == null)
            {
                _text = Text;
            }
            Text = _text + " (" + Measures + "," + Layouts + ")";
        }
    }
}
