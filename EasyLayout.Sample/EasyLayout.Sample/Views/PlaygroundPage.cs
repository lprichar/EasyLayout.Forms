// ReSharper disable CompareOfFloatsByEqualityOperator
using EasyLayout.Forms;
using EasyLayout.Forms.Sample;
using Xamarin.Forms;

namespace EasyLayout.Sample.Views
{
    public class PlaygroundPage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private Label _helloLabel;

        public PlaygroundPage()
        {
            AddViews();
            SetPageProperties();
            ConstrainLayout();
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _helloLabel.CenterX() == _relativeLayout.CenterX()
                && _helloLabel.Top() == _relativeLayout.Top() + 10
            );
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _helloLabel = _relativeLayout.AddLabel("Hello World");
        }

        private void SetPageProperties()
        {
            Content = _relativeLayout;
        }
    }
}
