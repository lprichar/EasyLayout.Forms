using EasyLayout.Forms;
using Xamarin.Forms;

namespace EasyLayout.Sample
{
    public class MainPage : ContentPage
    {
        public MainPage()
        {
            var relativeLayout = new RelativeLayout();

            var label = new Label
            {
                Text = "Hello World",
                BackgroundColor = Color.OrangeRed
            };

            //Size GetSize(VisualElement ve, RelativeLayout rl) => ve.Measure(rl.Width, rl.Height).Request;

            //label.Margin = new Thickness(0, 50, 0, 0);

            //relativeLayout.Children.Add(label,
            //    Constraint.RelativeToParent(parent => (parent.Width) - GetSize(label, parent).Width),
            //    Constraint.RelativeToParent(parent => (parent.Y))
            //    );

            relativeLayout.ConstrainLayout(() =>
                label.Bounds.Right == relativeLayout.Bounds.Right - 50 &&
                label.Bounds.Top == relativeLayout.Bounds.Top + 50 &&
                label.Bounds.Width == 300 &&
                label.Bounds.Height == 100
            );


            Content = relativeLayout;
        }
    }
}
