// ReSharper disable CompareOfFloatsByEqualityOperator

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Button _printStatsButton;
        private Button _aggregateButton;
        private ElfxProductView _productView;

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
            //NavigationPage.BarBackgroundColor
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _topFrame = _relativeLayout.AddBoxView(Color.Transparent);
            _perfLabel = _relativeLayout.AddPerfLabel("", Color.Gray);
            _productsListView = GetProductsListView();
            _printStatsButton = AddButton(_relativeLayout, "", "Default", "calculator.png");
            _aggregateButton = AddButton(_relativeLayout, "", "Default", "text_sum.png");
            _productView = AddProductView();
        }

        private ElfxProductView AddProductView()
        {
            var elfxProductView = new ElfxProductView();
            return elfxProductView;
        }

        private Button AddButton(RelativeLayout relativeLayout, string title, string style, string image)
        {
            var button = relativeLayout.AddButton(title, style);
            var fileImageSource = new FileImageSource
            {
                File = image
            };
            button.Image = fileImageSource;
            return button;
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
            const int topHeight = 140;

            _relativeLayout.ConstrainLayout(() =>
                _topFrame.Top() == _relativeLayout.Top()
                && _topFrame.Left() == _relativeLayout.Left()
                && _topFrame.Right() == _relativeLayout.Right()
                && _topFrame.Height == topHeight

                && _productView.Top() == _relativeLayout.Top() + 10
                && _productView.Left() == _relativeLayout.Left()
                && _productView.Right() == _relativeLayout.Right()
                && _productView.Height == 68

                && _perfLabel.Bottom() == _topFrame.Bottom() - 10
                && _perfLabel.Left() == _relativeLayout.Left() + 10

                && _productsListView.Top() == _topFrame.Bottom()
                && _productsListView.Left() == _relativeLayout.Left()
                && _productsListView.Right() == _relativeLayout.Right()
                && _productsListView.Height == _relativeLayout.Height - topHeight

                && _printStatsButton.Bottom() == _topFrame.Bottom() - 10
                && _printStatsButton.Right() == _topFrame.Right() - 10

                && _aggregateButton.Right() == _printStatsButton.Left() - 10
                && _aggregateButton.Bottom() == _printStatsButton.Bottom()
            );
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _printStatsButton.Clicked += PrintStatsButtonOnClicked;
            _aggregateButton.Clicked += AggregateButtonOnClicked;
            _productsListView.ItemSelected += ProductsListViewOnItemSelected;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            _printStatsButton.Clicked -= PrintStatsButtonOnClicked;
            _aggregateButton.Clicked -= AggregateButtonOnClicked;
            _productsListView.ItemSelected -= ProductsListViewOnItemSelected;
        }

        private void ProductsListViewOnItemSelected(object sender, SelectedItemChangedEventArgs selectedItemChangedEventArgs)
        {
            var product = (Product)selectedItemChangedEventArgs.SelectedItem;
            _productView.SetProduct(product);
        }

        private void AggregateButtonOnClicked(object sender, EventArgs eventArgs)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append($"Header[0]: {_productView.TitleMeasures}, {_productView.TitleLayouts}; ");

            var it = _productsListView as ITemplatedItemsView<Cell>;
            var items = it.TemplatedItems;

            var firstRowTitle = items.FirstOrDefault();
            if (firstRowTitle != null)
            {
                var productsListCell = firstRowTitle as ProductsListCell;
                sb.Append($"List[0].Title: {productsListCell?.Measures}, {productsListCell?.Layouts}; ");
            }
            _perfLabel.Text = sb.ToString();
            ForceLayout();
        }

        /// <summary>
        /// This is a terrible hack to force a right-edge constrained label, whose text has changed, 
        /// to redraw itself.  This is a problem with RelativeLayout's since they don't know to take 
        /// the width of their controls into account when determining whether to redraw themselves
        /// </summary>
        private new void ForceLayout()
        {
            _relativeLayout.ForceLayout();
            _relativeLayout.ForceLayout();
        }

        private void PrintStatsButtonOnClicked(object sender, EventArgs eventArgs)
        {
            _productView.PrintStats();
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
        private PerfLabel _categoryLabel;
        private Image _image;
        private PerfLabel _amountLabel;

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
            _categoryLabel.PrintStats();
            _amountLabel.PrintStats();
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _titleLabel = AddLabel("Title", true, "Header", null, Color.White);
            _categoryLabel = AddLabel("Category", false, "Subheader");
            _amountLabel = AddAmountLabel();
            _image = AddImage("Image");
        }

        private static PerfLabel AddAmountLabel()
        {
            var amountLabel = AddLabel("Amount", false, "Inverse", "{0:C}");
            amountLabel.VerticalTextAlignment = TextAlignment.Center;
            return amountLabel;
        }

        private Image AddImage(string sourceBinding)
        {
            var image = new Image();
            image.SetBinding(Image.SourceProperty, sourceBinding);
            return image;
        }

        private static PerfLabel AddLabel(string textBinding, bool bold, string style = null, string stringFormat = null, Color? textColor = null)
        {
            var label = new PerfLabel();
            label.SetBinding(Label.TextProperty, textBinding, stringFormat: stringFormat);
            if (bold)
            {
                label.FontAttributes = FontAttributes.Bold;
            }
            if (style != null)
            {
                label.StyleClass = new List<string> { style };
            }
            if (textColor.HasValue)
            {
                label.TextColor = textColor.Value;
            }
            return label;
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _image.Left() == _relativeLayout.Left() + 5
                && _image.CenterY() == _relativeLayout.CenterY()
                && _image.Height == 36
                && _image.Width == 36

                && _titleLabel.Left() == _image.Right() + 10
                && _titleLabel.Top() == _relativeLayout.Top() + 2
                && _titleLabel.Width == _relativeLayout.Width
                && _titleLabel.Height == 15

                && _categoryLabel.Bottom() == _relativeLayout.Bottom() - 2
                && _categoryLabel.Left() == _image.Right() + 10
                && _categoryLabel.Width == _relativeLayout.Width
                && _categoryLabel.Height == 15

                && _amountLabel.Right() == _relativeLayout.Right() - 5
                && _amountLabel.Top() == _relativeLayout.Top()
                && _amountLabel.Bottom() == _relativeLayout.Bottom()
                );
        }
    }
}
