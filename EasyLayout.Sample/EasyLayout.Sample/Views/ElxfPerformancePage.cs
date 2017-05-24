// ReSharper disable CompareOfFloatsByEqualityOperator
using EasyLayout.Forms;
using EasyLayout.Forms.Sample;
using Xamarin.Forms;

namespace EasyLayout.Sample.Views
{
    public class ElxfPerformancePage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private BoxView _topFrame;
        private Label _perfLabel;

        public ElxfPerformancePage()
        {
            AddViews();
            ConstrainLayout();
            SetPageProperties();
        }

        private void SetPageProperties()
        {
            Title = "EasyLayout.Forms Performance";
            Content = _relativeLayout;
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _topFrame = _relativeLayout.AddBoxView(Colors.Background);
            _perfLabel = _relativeLayout.AddLabel("Click The Buttons To View Perf Stats", Color.White);
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _topFrame.Bounds.Top == _relativeLayout.Bounds.Top
                && _topFrame.Bounds.Left == _relativeLayout.Bounds.Left
                && _topFrame.Bounds.Right == _relativeLayout.Bounds.Right
                && _topFrame.Bounds.Height == 200

                && _perfLabel.Bounds.Top == _relativeLayout.Bounds.Top + 100
                && _perfLabel.Bounds.Right == _relativeLayout.Bounds.Right - 10
            );
        }
    }
}
