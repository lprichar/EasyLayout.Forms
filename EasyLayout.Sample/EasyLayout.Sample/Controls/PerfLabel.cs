using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace EasyLayout.Sample.Controls
{
    public class PerfLabel : Label
    {
        private int _measured = 0;
        private int _sizeAllocated = 0;
        private string _text;

        protected override SizeRequest OnMeasure(double widthConstraint, double heightConstraint)
        {
            _measured++;
            return base.OnMeasure(widthConstraint, heightConstraint);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            _sizeAllocated++;
            base.OnSizeAllocated(width, height);
        }

        public void PrintStats()
        {
            if (_text == null)
            {
                _text = Text;
            }
            Text = _text + " (" + _measured + "," + _sizeAllocated + ")";
        }
    }
}
