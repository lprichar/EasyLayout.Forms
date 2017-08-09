// ReSharper disable CompareOfFloatsByEqualityOperator

using EasyLayout.Forms;
using EasyLayout.Forms.Sample;
using Xamarin.Forms;

namespace EasyLayout.Sample.Views
{
    public class PlaygroundPage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private Label _topLabel, _middleLabel, _bottomLabel;

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
                && _topLabel.Height() == 40
                
                && _bottomLabel.Width() == _relativeLayout.Width()
                && _bottomLabel.Bottom() == _relativeLayout.Bottom()
                && _bottomLabel.Height() == 40
                
                // Right now this needs to be manually positioned after the views it's dependent upon
                && _middleLabel.Top() == _topLabel.Bottom()
                && _middleLabel.Left() == _relativeLayout.Left()
                && _middleLabel.Right() == _relativeLayout.Right()
                && _middleLabel.Bottom() == _bottomLabel.Top()
            );
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _relativeLayout.BackgroundColor = Color.Red;
            _topLabel = _relativeLayout.AddLabel("Top Label", Colors.White, Colors.DarkerBlue);
            _topLabel.VerticalTextAlignment = TextAlignment.Center;
            _middleLabel = _relativeLayout.AddLabel("Middle Label", Colors.White, Colors.BluePurple);
            _middleLabel.VerticalTextAlignment = TextAlignment.End;
            _bottomLabel = _relativeLayout.AddLabel("Bottom Label", Colors.White, Colors.Green);
            _bottomLabel.VerticalTextAlignment = TextAlignment.Center;
        }

        private void SetPageProperties()
        {
            Content = _relativeLayout;
        }
    }
}