using System;
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

        public static Button AddButton(this RelativeLayout parent, string text)
        {
            var button = parent.Add<Button>();
            button.Text = text;
            return button;
        }

        public static Label AddLabel(this RelativeLayout parent, string text, Color background, Color textColor)
        {
            var textView = parent.Add<Label>();
            textView.Text = text;
            textView.BackgroundColor = background;
            textView.TextColor = textColor;
            textView.FontSize = 15f;
            textView.HorizontalTextAlignment = TextAlignment.Center;
            return textView;
        }
    }
}