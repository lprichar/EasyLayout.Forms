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
        private Label _titleLabel;
        private string _title;

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
            _titleLabel = _relativeLayout.AddLabel("Select A Product");
            _titleLabel.SetBinding(Label.TextProperty, "Title");
        }

        private void ConstrainLayout()
        {
            _relativeLayout.ConstrainLayout(() =>
                _titleLabel.Bounds.Left == _relativeLayout.Bounds.Left
                && _titleLabel.Bounds.Top == _relativeLayout.Bounds.Top
            );
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged();
            }
        }

        public void SetProduct(Product product)
        {
            //Image.Source = product.Image;
            Title = product.Title;
            //Category.Text = product.Category;
        }
    }
}
