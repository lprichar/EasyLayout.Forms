using EasyLayout.Droid.Sample.Models;

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
            ProductId.PrintStats();
        }

	    public void SetProduct(Product product)
	    {
	        ProductId.Text = product.Id.ToString();
            Title.Text = product.Title;
	        Category.Text = product.Category;
	    }
	}
}