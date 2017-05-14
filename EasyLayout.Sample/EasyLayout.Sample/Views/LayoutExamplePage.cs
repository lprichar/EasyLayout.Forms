using EasyLayout.Forms.Sample;
using EasyLayout.Forms;
using EasyLayout.Forms.Sample.Models;
using Xamarin.Forms;
using Colors = EasyLayout.Forms.Sample.Colors;

namespace EasyLayout.Sample
{
    public class LayoutExamplePage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private Label _center;
        private Label _top;
        private Label _right;
        private Label _left;
        private Label _bottom;
        private Label _upperRight;
        private Label _upperLeft;
        private Label _lowerLeft;
        private Label _lowerRight;
        private Label _topTop;
        private Label _bottomBottom;

        private MainViewModel ViewModel { get; } = new MainViewModel();

        public LayoutExamplePage()
        {
            AddViews();
            ConstrainLayout();
            SetPageProperties();
        }

        private void SetPageProperties()
        {
            Title = "EasyLayout.Forms Layout Sample";
            Content = _relativeLayout;
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _center.Bounds.GetCenterX() == _relativeLayout.Bounds.GetCenterX() &&
                _center.Bounds.GetCenterY() == _relativeLayout.Bounds.GetCenterY() &&
                _center.Bounds.Height == 50 &&

                _topTop.Bounds.GetCenterX() == _relativeLayout.Bounds.GetCenterX() &&
                _topTop.Bounds.Top == _relativeLayout.Bounds.Top &&
                _topTop.Bounds.Height == 40 &&
                _topTop.Bounds.Width == _relativeLayout.Bounds.Width - 10 &&

                _top.Bounds.Left == _center.Bounds.Left &&
                _top.Bounds.Right == _center.Bounds.Right &&
                _top.Bounds.Bottom == _center.Bounds.Top - 20 &&

                _right.Bounds.Left == _center.Bounds.Right + 20 &&
                _right.Bounds.Top == _center.Bounds.Top &&
                _right.Bounds.Bottom == _center.Bounds.Bottom &&

                _left.Bounds.Right == _center.Bounds.Left - 20 &&
                _left.Bounds.Top == _center.Bounds.Top &&
                _left.Bounds.Bottom == _center.Bounds.Bottom &&

                _bottom.Bounds.Left == _center.Bounds.Left &&
                _bottom.Bounds.Right == _center.Bounds.Right &&
                _bottom.Bounds.Top == _center.Bounds.Bottom + 20 &&

                _upperRight.Bounds.Left == _center.Bounds.Right + 20 &&
                _upperRight.Bounds.Bottom == _top.Bounds.Bottom &&
                _upperRight.Bounds.Height == 45 &&
                _upperRight.Bounds.Width == _upperLeft.Bounds.Width &&

                _upperLeft.Bounds.Right == _center.Bounds.Left - 20 &&
                _upperLeft.Bounds.Bottom == _center.Bounds.Top - 20 &&
                _upperLeft.Bounds.Height == 40 &&
                _upperLeft.Bounds.Width == 200 &&

                _lowerLeft.Bounds.Right == _left.Bounds.Right &&
                _lowerLeft.Bounds.Bottom == _bottom.Bounds.Bottom &&

                _lowerRight.Bounds.Top == _bottom.Bounds.Top &&
                _lowerRight.Bounds.Left == _right.Bounds.Left &&

                _bottomBottom.Bounds.Bottom == _relativeLayout.Bounds.Bottom &&
                _bottomBottom.Bounds.Left == _topTop.Bounds.Left &&
                _bottomBottom.Bounds.Height == ViewModel.Height.ToConst() &&
                _bottomBottom.Bounds.Width == _topTop.Bounds.Width
            );
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _topTop = _relativeLayout.AddLabel("this.GetCenterX() == _layoutView.GetCenterX() \n&& this.Top == relativeLayout.Top", Colors.Gold, Colors.DarkGrey);
            _center = _relativeLayout.AddLabel("this.GetCenterY() == \nrelativeLayout.GetCenterY()", Colors.DarkGrey, Colors.White);
            _top = _relativeLayout.AddLabel("this.Bottom == \n_center.Top - 20", Colors.Yellow, Colors.DarkGrey);
            _upperLeft = _relativeLayout.AddLabel("this.Height == 40 &&\nthis.Width == 140", Colors.YellowGreen, Colors.White);
            _left = _relativeLayout.AddLabel("this.Right == \n_center.Left - 20", Colors.Green, Colors.White);
            _lowerLeft = _relativeLayout.AddLabel("this.Right == _left.Right &&\nthis.Bottom == _bottom.Bottom", Colors.LightBlue, Colors.White);
            _bottom = _relativeLayout.AddLabel("this.Top == \n_center.Bottom + 20", Colors.DarkBlue, Colors.White);
            _lowerRight = _relativeLayout.AddLabel("this.Top == _bottom.Top &&\nthis.Left == _right.Left", Colors.BluePurple, Colors.White);
            _right = _relativeLayout.AddLabel("this.Left == \n_center.Right + 20", Colors.Red, Colors.White);
            _upperRight = _relativeLayout.AddLabel("this.Bounds.Bottom == _top.Bounds.Bottom", Colors.Orange, Colors.White);
            _bottomBottom = _relativeLayout.AddLabel("this.Height == ViewModel.Height.ToConst() && \nthis.Width == _topTop.Width", Colors.Purple, Colors.White);
        }
    }
}
