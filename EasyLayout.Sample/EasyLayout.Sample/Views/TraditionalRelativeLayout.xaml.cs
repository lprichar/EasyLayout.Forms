using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLayout.Forms.Sample;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLayout.Sample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TraditionalRelativeLayout : ContentPage
	{
		public TraditionalRelativeLayout ()
		{
			InitializeComponent ();
		    Title = "Traditional XAML RelativeLayout";
		    AddViews();
		}

	    private void AddViews()
	    {
	        var relativeLayout = InnerRelativeLayout;

            var mainLabel = relativeLayout.AddLabel("MainLabel", Colors.BluePurple, Colors.DarkGrey);
	        var relativeLabel = relativeLayout.AddLabel("RelativeLabel", Colors.Red, Colors.DarkGrey);
	        var centerLabel = relativeLayout.AddLabel("CenterLabel", Colors.Green, Colors.White);

            relativeLayout.Children.Add(mainLabel, 
                Constraint.RelativeToParent(rl => rl.X + 10),
                Constraint.RelativeToParent(rl => rl.Y + 10),
                Constraint.Constant(150),
                Constraint.Constant(40)
                );

            relativeLayout.Children.Add(relativeLabel, 
                Constraint.RelativeToView(mainLabel, (rl, v) => v.X + v.Width + 10),
                Constraint.RelativeToView(mainLabel, (rl, v) => v.Y + v.Height + 10),
                Constraint.RelativeToView(mainLabel, (rl, v) => v.Width),
                Constraint.RelativeToView(mainLabel, (rl, v) => v.Height)
                );

            Size GetSize(VisualElement ve, RelativeLayout rl) => ve.Measure(rl.Width, rl.Height).Request;

            relativeLayout.Children.Add(centerLabel, 
                Constraint.RelativeToView(relativeLabel, (rl, v) => v.X + (v.Width * .5f) - (GetSize(centerLabel, rl).Width / 2)),
                Constraint.RelativeToView(relativeLabel, (rl, v) => v.Y + v.Height + 10)
                //,Constraint.RelativeToView(mainLabel, (rl, v) => v.Width)
                // ^ this breaks RelativeLayout
                );


        }
	}
}
