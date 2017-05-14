using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLayout.Forms;
using EasyLayout.Forms.Sample;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLayout.Sample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TraditionalRelativeLayout : ContentPage
	{
	    private Label _mainLabel;
	    private Label _relativeLabel;
	    private Label _centerLabel;

	    public TraditionalRelativeLayout ()
		{
			InitializeComponent ();
		    Title = "Traditional XAML RelativeLayout";
		    AddViews();
		    ConstrainLayout();
		}

	    private void ConstrainLayout()
	    {
	        var relativeLayout = EasyLayoutRelativeLayout;

            relativeLayout.ConstrainLayout(() =>
                _mainLabel.Bounds.Top == relativeLayout.Bounds.Top + 10 &&
                _mainLabel.Bounds.Left == relativeLayout.Bounds.Left + 10 &&
                _mainLabel.Bounds.Width == 150 &&
                _mainLabel.Bounds.Height == 40 &&

                _relativeLabel.Bounds.Top == _mainLabel.Bounds.Bottom + 10 &&
                _relativeLabel.Bounds.Left == _mainLabel.Bounds.Right + 10 &&
                _relativeLabel.Bounds.Width == _mainLabel.Bounds.Width &&
                _relativeLabel.Bounds.Height == _mainLabel.Bounds.Height &&

                _centerLabel.Bounds.GetCenterX() == _relativeLabel.Bounds.GetCenterX() &&
                _centerLabel.Bounds.Top == _relativeLabel.Bounds.Bottom + 10
                );
	    }

	    private void AddViews()
	    {
	        AddTraditionalProgramaticRelativeLayout();
	        AddEasyLayoutControls();
	    }

	    private void AddEasyLayoutControls()
	    {
	        var relativeLayout = EasyLayoutRelativeLayout;
            _mainLabel = relativeLayout.AddLabel("MainLabel", Colors.BluePurple, Colors.DarkGrey);
	        _relativeLabel = relativeLayout.AddLabel("RelativeLabel", Colors.Red, Colors.DarkGrey);
	        _centerLabel = relativeLayout.AddLabel("CenterLabel", Colors.Green, Colors.White);
	    }

        private void AddTraditionalProgramaticRelativeLayout()
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
	            Constraint.RelativeToView(relativeLabel,
	                (rl, v) => v.X + (v.Width * .5f) - (GetSize(centerLabel, rl).Width / 2)),
	            Constraint.RelativeToView(relativeLabel, (rl, v) => v.Y + v.Height + 10)
	            //,Constraint.RelativeToView(mainLabel, (rl, v) => v.Width)
	            // ^ this breaks RelativeLayout
	        );
	    }
	}
}
