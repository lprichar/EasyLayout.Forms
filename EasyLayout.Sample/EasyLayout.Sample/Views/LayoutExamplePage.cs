// ReSharper disable CompareOfFloatsByEqualityOperator
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
                _center.CenterX() == _relativeLayout.CenterX() &&
                _center.CenterY() == _relativeLayout.CenterY() &&
                _center.Height == 50 &&

                _topTop.CenterX() == _relativeLayout.CenterX() &&
                _topTop.Top() == _relativeLayout.Top() &&
                _topTop.Height == 40 &&
                _topTop.Width == _relativeLayout.Width - 10 &&

                _top.Left() == _center.Left() &&
                _top.Right() == _center.Right() &&
                _top.Bottom() == _center.Top() - 20 &&

                _right.Left() == _center.Right() + 20 &&
                _right.Top() == _center.Top() &&
                _right.Bottom() == _center.Bottom() &&

                _left.Right() == _center.Left() - 20 &&
                _left.Top() == _center.Top() &&
                _left.Bottom() == _center.Bottom() &&

                _bottom.Left() == _center.Left() &&
                _bottom.Right() == _center.Right() &&
                _bottom.Top() == _center.Bottom() + 20 &&

                _upperRight.Left() == _center.Right() + 20 &&
                _upperRight.Bottom() == _top.Bottom() &&
                _upperRight.Height == 45 &&
                _upperRight.Width == _upperLeft.Width &&

                _upperLeft.Right() == _center.Left() - 20 &&
                _upperLeft.Bottom() == _center.Top() - 20 &&
                _upperLeft.Height == 40 &&
                _upperLeft.Width == 200 &&

                _lowerLeft.Right() == _left.Right() &&
                _lowerLeft.Bottom() == _bottom.Bottom() &&

                _lowerRight.Top() == _bottom.Top() &&
                _lowerRight.Left() == _right.Left() &&

                _bottomBottom.Bottom() == _relativeLayout.Bottom() &&
                _bottomBottom.Left() == _topTop.Left() &&
                _bottomBottom.Height == ViewModel.Height.ToConst() &&
                _bottomBottom.Width == _topTop.Width
            );
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _topTop = _relativeLayout.AddLabel("this.CenterX() == _layoutView.CenterX() \n&& this.Top() == relativeLayout.Top()", Colors.DarkGrey, Colors.Gold);
            _center = _relativeLayout.AddLabel("this.CenterY() == \nrelativeLayout.CenterY()", Colors.White, Colors.DarkGrey);
            _top = _relativeLayout.AddLabel("this.Bottom() == \n_center.Top() - 20", Colors.DarkGrey, Colors.Yellow);
            _upperLeft = _relativeLayout.AddLabel("this.Height == 40 &&\nthis.Width == 140", Colors.White, Colors.YellowGreen);
            _left = _relativeLayout.AddLabel("this.Right() == \n_center.Left() - 20", Colors.White, Colors.Green);
            _lowerLeft = _relativeLayout.AddLabel("this.Right() == _left.Right() &&\nthis.Bottom() == _bottom.Bottom()", Colors.White, Colors.LightBlue);
            _bottom = _relativeLayout.AddLabel("this.Top() == \n_center.Bottom() + 20", Colors.White, Colors.DarkBlue);
            _lowerRight = _relativeLayout.AddLabel("this.Top() == _bottom.Top() &&\nthis.Left() == _right.Left()", Colors.White, Colors.BluePurple);
            _right = _relativeLayout.AddLabel("this.Left() == \n_center.Right() + 20", Colors.White, Colors.Red);
            _upperRight = _relativeLayout.AddLabel("this.Bottom() == _top.Bottom()", Colors.White, Colors.Orange);
            _bottomBottom = _relativeLayout.AddLabel("this.Height == ViewModel.Height.ToConst() && \nthis.Width == _topTop.Width", Colors.White, Colors.Purple);
        }
    }
}
