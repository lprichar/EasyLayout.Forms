// ReSharper disable CompareOfFloatsByEqualityOperator

using EasyLayout.Droid.Sample.Models;
using EasyLayout.Forms;
using EasyLayout.Forms.Sample;
using Xamarin.Forms;

namespace EasyLayout.Sample.Controls
{
    public class ElfxProductView : ContentView
    {
        private RelativeLayout _relativeLayout;
        private PerfLabel _titleLabel;
        private Product _product;
        private PerfLabel _categoryLabel;
        private Image _image;
        private Button _confirmButton;

        public ElfxProductView()
        {
            AddViews();
            ConstrainLayout();
            SetPageProperties();
        }

        public int TitleMeasures => _titleLabel.Measures;
        public int TitleLayouts => _titleLabel.Layouts;

        public void SetProduct(Product product)
        {
            BackgroundColor = Colors.DarkBlue;
            Product = product;
            _confirmButton.IsVisible = true;
        }

        public void PrintStats()
        {
            _titleLabel.PrintStats();
            _categoryLabel.PrintStats();
        }

        private void SetPageProperties()
        {
            Content = _relativeLayout;
            BindingContext = this;
        }

        public Product Product
        {
            get { return _product; }
            set
            {
                _product = value;
                OnPropertyChanged();
            }
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _image = AddImage("Product.Image");
            _titleLabel = AddLabel("Product.Title", NamedSize.Medium, isBold: true);
            _categoryLabel = AddLabel("Product.Category", NamedSize.Small);
            _confirmButton = AddConfirmButton(_relativeLayout);
        }

        private static Button AddConfirmButton(RelativeLayout parent)
        {
            var confirmButton = parent.AddButton("Confirm");
            confirmButton.IsVisible = false;
            confirmButton.BackgroundColor = Colors.DarkerBlue;
            return confirmButton;
        }

        private Image AddImage(string binding)
        {
            var image = new Image();
            image.SetBinding(Image.SourceProperty, binding);
            return image;
        }

        private PerfLabel AddLabel(string binding, NamedSize namedSize, bool isBold = false)
        {
            var perfLabel = new PerfLabel();
            perfLabel.SetBinding(Label.TextProperty, binding);
            if (isBold)
            {
                perfLabel.FontAttributes = FontAttributes.Bold;
            }
            perfLabel.FontSize = Device.GetNamedSize(namedSize, typeof(Label));
            perfLabel.TextColor = Color.White;
            return perfLabel;
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ForceLayout();
            _relativeLayout.ConstrainLayout(() =>
                _image.Bounds.Left == _relativeLayout.Bounds.Left + 10
                && _image.Bounds.Top == _relativeLayout.Bounds.Top + 10
                && _image.Bounds.Width == 48
                && _image.Bounds.Height == 48

                && _titleLabel.Bounds.Left == _image.Bounds.Right + 10
                && _titleLabel.Bounds.Top == _relativeLayout.Bounds.Top + 10
                && _titleLabel.Bounds.Width == _relativeLayout.Bounds.Width - 168
                && _titleLabel.Bounds.Height == 20

                && _categoryLabel.Bounds.Left == _titleLabel.Bounds.Left
                && _categoryLabel.Bounds.Top == _titleLabel.Bounds.Bottom + 5
                && _categoryLabel.Bounds.Width == _relativeLayout.Bounds.Width - 58
                && _categoryLabel.Bounds.Height == 20

                && _confirmButton.Bounds.Right == _relativeLayout.Bounds.Right - 10
                && _confirmButton.Bounds.Top == _relativeLayout.Bounds.Top + 10
                && _confirmButton.Bounds.Bottom == _relativeLayout.Bounds.Bottom - 20
                && _confirmButton.Bounds.Width == 90
            );
        }
    }
}
