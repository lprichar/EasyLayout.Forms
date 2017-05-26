// ReSharper disable CompareOfFloatsByEqualityOperator

using System.Collections.Generic;
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

        public ElfxProductView()
        {
            AddViews();
            ConstrainLayout();
            SetPageProperties();
        }

        private void SetPageProperties()
        {
            Content = _relativeLayout;
            BindingContext = this;
        }

        private void AddViews()
        {
            _relativeLayout = ViewUtils.AddRelativeLayout();
            _image = AddImage("Product.Image");
            _titleLabel = AddLabel("Product.Title", isBold: true);
            _categoryLabel = AddLabel("Product.Category");
        }

        private Image AddImage(string binding)
        {
            var image = new Image();
            image.SetBinding(Image.SourceProperty, binding);
            return image;
        }

        private PerfLabel AddLabel(string binding, bool isBold = false)
        {
            var perfLabel = new PerfLabel();
            perfLabel.SetBinding(Label.TextProperty, binding);
            if (isBold)
            {
                perfLabel.FontAttributes = FontAttributes.Bold;
            }
            perfLabel.TextColor = Color.White;
            return perfLabel;
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _image.Bounds.Left == _relativeLayout.Bounds.Left
                && _image.Bounds.Top == _relativeLayout.Bounds.Top
                && _image.Bounds.Width == 48
                && _image.Bounds.Height == 48

                && _titleLabel.Bounds.Left == _image.Bounds.Right + 10
                && _titleLabel.Bounds.Top == _relativeLayout.Bounds.Top
                
                && _categoryLabel.Bounds.Left == _titleLabel.Bounds.Left
                && _categoryLabel.Bounds.Top == _titleLabel.Bounds.Bottom + 5
            );
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

        public void SetProduct(Product product)
        {
            //Image.Source = product.Image;
            Product = product;
            //Category.Text = product.Category;
        }
    }
}
