// ReSharper disable CompareOfFloatsByEqualityOperator

using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLayout.Droid.Sample.Models;
using EasyLayout.Forms;
using EasyLayout.Forms.Sample;
using EasyLayout.Sample.Controls;
using Xamarin.Forms;

namespace EasyLayout.Sample.Views
{
    public class ElxfPerformancePage : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private BoxView _topFrame;
        private PerfLabel _perfLabel;
        private ListView _productsListView;
        private Button _showStatsButton;
        private Button _aggregateButton;

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
            _perfLabel = _relativeLayout.AddPerfLabel("Click The Buttons To View Perf Stats", Color.White);
            _productsListView = GetProductsListView();
            _showStatsButton = _relativeLayout.AddButton("Show Stats In Views");
            _aggregateButton = _relativeLayout.AddButton("Aggregate Stats");
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

                && _showStatsButton.Bounds.Bottom == _topFrame.Bounds.Bottom - 10
                && _showStatsButton.Bounds.Right == _topFrame.Bounds.Right - 10

                && _aggregateButton.Bounds.Right == _showStatsButton.Bounds.Left - 10
                && _aggregateButton.Bounds.Bottom == _showStatsButton.Bounds.Bottom
            );
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _showStatsButton.Clicked += ShowStatsButtonOnClicked;
            _aggregateButton.Clicked += AggregateButtonOnClicked;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _showStatsButton.Clicked -= ShowStatsButtonOnClicked;
            _aggregateButton.Clicked -= AggregateButtonOnClicked;
        }

        private void AggregateButtonOnClicked(object sender, EventArgs eventArgs)
        {
            StringBuilder sb = new StringBuilder();

            //sb.Append($"Header[0]: {ProductView.TitleMeasures}, {ProductView.TitleLayouts}; ");

            var it = _productsListView as ITemplatedItemsView<Cell>;
            var items = it.TemplatedItems;

            var firstRowTitle = items.FirstOrDefault();
            if (firstRowTitle != null)
            {
                var productsListCell = firstRowTitle as ProductsListCell;
                sb.Append($"List[0].Title: {productsListCell?.Measures}, {productsListCell?.Layouts}; ");
            }
            _perfLabel.Text = sb.ToString();
        }

        private void ShowStatsButtonOnClicked(object sender, EventArgs eventArgs)
        {
            _perfLabel.PrintStats();
            var it = _productsListView as ITemplatedItemsView<Cell>;
            var items = it.TemplatedItems;
            foreach (var item in items)
            {
                var productsListCell = item as ProductsListCell;
                productsListCell?.PrintStats();
            }
        }
    }

    public class ProductsListCell : ViewCell
    {
        private RelativeLayout _relativeLayout;
        private PerfLabel _titleLabel;
        private Image _image;

        public ProductsListCell()
        {
            AddViews();
            ConstrainLayout();
            View = _relativeLayout;
        }

        public int Measures => _titleLabel.Measures;
        public int Layouts => _titleLabel.Layouts;

        public void PrintStats()
        {
            _titleLabel.PrintStats();
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _titleLabel = AddLabel("Title");
            _image = AddImage("Image");
        }

        private Image AddImage(string sourceBinding)
        {
            var image = new Image
            {
                BackgroundColor = Color.FromRgb(245, 95, 95)
            };
            image.SetBinding(Image.SourceProperty, sourceBinding);
            return image;
        }

        private static PerfLabel AddLabel(string textBinding)
        {
            var label = new PerfLabel();
            label.SetBinding(Label.TextProperty, textBinding);
            return label;
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _image.Bounds.Left == _relativeLayout.Bounds.Left
                && _image.Bounds.Top == _relativeLayout.Bounds.Top
                && _image.Bounds.Height == 36
                && _image.Bounds.Width == 36

                && _titleLabel.Bounds.Left == _image.Bounds.Right + 10
                && _titleLabel.Bounds.Top == _relativeLayout.Bounds.Top
                && _titleLabel.Bounds.Height == _relativeLayout.Bounds.Height
                );
        }
    }
}
