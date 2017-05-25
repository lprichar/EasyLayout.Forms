using System;
using EasyLayout.Sample.Controls;
using Xamarin.Forms;

namespace EasyLayout.Forms.Sample
{
    public static class ViewUtils
    {
        public static RelativeLayout AddRelativeLayout()
        {
            return new RelativeLayout();
        }

        public static T Add<T>(this RelativeLayout parent) where T : View
        {
            var child = (T)Activator.CreateInstance(typeof(T));
            return child;
        }

        public static BoxView AddBoxView(this RelativeLayout parent, Color backgroundColor)
        {
            var frame = parent.Add<BoxView>();
            frame.BackgroundColor = backgroundColor;
            return frame;
        }

        public static Button AddButton(this RelativeLayout parent, string text)
        {
            var button = parent.Add<Button>();
            button.Text = text;
            return button;
        }

        public static Label AddLabel(this RelativeLayout parent, string text, Color textColor, Color? background = null)
        {
            var textView = parent.Add<Label>();
            textView.Text = text;
            if (background != null)
            {
                textView.BackgroundColor = background.Value;
            }
            textView.TextColor = textColor;
            textView.FontSize = 15f;
            textView.HorizontalTextAlignment = TextAlignment.Center;
            return textView;
        }

        public static PerfLabel AddPerfLabel(this RelativeLayout parent, string text, Color color)
        {
            var perfLabel = new PerfLabel
            {
                Text = text,
                TextColor = color
            };
            return perfLabel;
        }
    }
}