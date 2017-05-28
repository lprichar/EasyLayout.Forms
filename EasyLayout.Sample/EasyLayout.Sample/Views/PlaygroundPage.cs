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
                _helloLabel.Bounds.GetCenterX() == _relativeLayout.Bounds.GetCenterX()
                && _helloLabel.Bounds.Top == _relativeLayout.Bounds.Top + 10
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
