// ReSharper disable CompareOfFloatsByEqualityOperator

using EasyLayout.Droid.Sample.Models;
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
        private ListView _productsListView;

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
            _productsListView = GetProductsListView();
        }

        private static ListView GetProductsListView()
        {
            var listView = new ListView(ListViewCachingStrategy.RetainElement)
            {
                ItemsSource = Data.GetProducts(),
                ItemTemplate = new DataTemplate(typeof(ProductsListCell)),
            };
            return listView;
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

                && _productsListView.Bounds.Top == _topFrame.Bounds.Bottom
                && _productsListView.Bounds.Left == _relativeLayout.Bounds.Left
                && _productsListView.Bounds.Right == _relativeLayout.Bounds.Right
                && _productsListView.Bounds.Height == _relativeLayout.Bounds.Height - 200
            );
        }
    }

    public class ProductsListCell : ViewCell
    {
        private RelativeLayout _relativeLayout;
        private Label _titleLabel;

        public ProductsListCell()
        {
            AddViews();
            ConstrainLayout();
            View = _relativeLayout;
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _titleLabel = AddLabel("Title");
        }

        private static Label AddLabel(string textBinding)
        {
            var label = new Label();
            label.SetBinding(Label.TextProperty, textBinding);
            return label;
        }

        private void ConstrainLayout()
        {
            _relativeLayout.Children.Add(_titleLabel,
                () => _relativeLayout.X,
                () => _relativeLayout.Y);

            //_relativeLayout.ConstrainLayout(() =>
            //    _titleLabel.Bounds.Left == _relativeLayout.Bounds.Left
            //    && _titleLabel.Bounds.Top == _relativeLayout.Bounds.Top
            //    && _titleLabel.Bounds.Width == _relativeLayout.Bounds.Width
            //    && _titleLabel.Bounds.Height == _relativeLayout.Bounds.Height
            //    );
        }
    }
}
