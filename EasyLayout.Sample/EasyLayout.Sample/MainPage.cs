using EasyLayout.Forms;
using Xamarin.Forms;

namespace EasyLayout.Sample
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            var relativeLayout = new RelativeLayout();

            var label1 = new Label
            {
                Text = "Label 1",
                BackgroundColor = Color.OrangeRed
            };

            var label2 = new Label
            {
                Text = "Label 2",
                BackgroundColor = Color.GreenYellow
            };

            Size GetSize(VisualElement ve, RelativeLayout rl) => ve.Measure(rl.Width, rl.Height).Request;

            //relativeLayout.Children.Add(label1,
            //    Constraint.RelativeToParent(parent => (parent.X) + 10),
            //    Constraint.RelativeToParent(parent => (parent.Y) + 10)
            //    );

            //relativeLayout.Children.Add(label2,
            //    Constraint.RelativeToView(label1, (rl, l1) => l1.X + 10),
            //    Constraint.RelativeToView(label1, (rl, l1) => l1.Y + l1.Height + 10));

            relativeLayout.ConstrainLayout(() =>
                label1.Bounds.Left == relativeLayout.Bounds.Left + 100 &&
                label1.Bounds.Top == relativeLayout.Bounds.Top + 100 &&
                label1.Bounds.Width == 100 &&
                label1.Bounds.Height == 100 &&

                label2.Bounds.Bottom == label1.Bounds.Bottom + 10 &&
                label2.Bounds.Right == label1.Bounds.Right + 10
            );

            Content = relativeLayout;
        }
    }
}
