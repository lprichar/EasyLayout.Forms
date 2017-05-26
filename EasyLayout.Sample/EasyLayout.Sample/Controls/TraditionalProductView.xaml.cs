using EasyLayout.Droid.Sample.Models;
using EasyLayout.Forms.Sample;
using Xamarin.Forms;

namespace EasyLayout.Sample.Controls
{
	public partial class TraditionalProductView
	{
	    public int TitleMeasures => Title.Measures;
	    public int TitleLayouts => Title.Layouts;

		public TraditionalProductView ()
		{
			InitializeComponent ();
		}

	    public void PrintStats()
	    {
	        Title.PrintStats();
	        Category.PrintStats();
        }

	    public void SetProduct(Product product)
	    {
	        Image.Source = product.Image;
            Title.Text = product.Title;
	        Category.Text = product.Category;
	        Confirm.IsVisible = true;
	        BackgroundColor = Colors.DarkBlue;
	    }
	}
}