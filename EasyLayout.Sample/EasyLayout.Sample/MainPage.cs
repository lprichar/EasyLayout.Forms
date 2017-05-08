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

            //relativeLayout.Children.Add(label,
            //    Constraint.RelativeToParent(parent => (parent.Width) - GetSize(label, parent).Width),
            //    Constraint.RelativeToParent(parent => (parent.Height * .5) - GetSize(label, parent).Height * .5)
            //    );

            relativeLayout.ConstrainLayout(() =>
                label.Bounds.GetCenterX() == relativeLayout.Bounds.GetCenterX() &&
                label.Bounds.GetCenterY() == relativeLayout.Bounds.GetCenterY() &&
                label.Bounds.Width == 300 &&
                label.Bounds.Height == 100
            );


            Content = relativeLayout;
        }
    }
}
