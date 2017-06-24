// ReSharper disable CompareOfFloatsByEqualityOperator
using EasyLayout.Forms;
using EasyLayout.Forms.Sample;
using Xamarin.Forms;

namespace EasyLayout.Sample.Views
{
    public class PlaygroundPage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private Label _topLabel;
        private Label _bottomLabel;

        public PlaygroundPage()
        {
            AddViews();
            SetPageProperties();
            ConstrainLayout();
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _topLabel.Top() == _relativeLayout.Top()
                && _topLabel.Left() == _relativeLayout.Left()
                && _topLabel.Right() == _relativeLayout.Right()
                
                && _bottomLabel.Top() == _topLabel.Bottom()
                && _bottomLabel.Left() == _relativeLayout.Left()
                && _bottomLabel.Right() == _relativeLayout.Right()
                && _bottomLabel.Bottom() == _relativeLayout.Bottom()
            );
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _relativeLayout.BackgroundColor = Color.Red;
            _topLabel = _relativeLayout.AddLabel("Top Label", Colors.White, Colors.DarkerBlue);
            _bottomLabel = _relativeLayout.AddLabel("Bottom Label", Colors.White, Colors.BluePurple);
            _bottomLabel.VerticalTextAlignment = TextAlignment.End;
        }

        private void SetPageProperties()
        {
            Content = _relativeLayout;
        }
    }
}
