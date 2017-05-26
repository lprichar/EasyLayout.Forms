using System;
using System.Linq;
using System.Text;
using EasyLayout.Droid.Sample.Models;
using EasyLayout.Sample.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLayout.Sample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TraditionalPerformancePage
	{
		public TraditionalPerformancePage ()
		{
			InitializeComponent ();
		    ProductsList.ItemsSource = Data.GetProducts();
		}

	    private void PrintStatsClicked(object sender, EventArgs e)
	    {
	        ProductView.PrintStats();
	        var it = ProductsList as ITemplatedItemsView<Cell>;
	        var items = it.TemplatedItems;
	        foreach (var item in items)
	        {
	            var title = item.FindByName<PerfLabel>("Title");
	            title.PrintStats();
	            var category = item.FindByName<PerfLabel>("Category");
	            category.PrintStats();
	            var amount = item.FindByName<PerfLabel>("Amount");
	            amount.PrintStats();
            }
        }

	    private void AggregateStatsClicked(object sender, EventArgs e)
	    {
            StringBuilder sb = new StringBuilder();

	        sb.Append($"Header[0]: {ProductView.TitleMeasures}, {ProductView.TitleLayouts}; ");

	        var it = ProductsList as ITemplatedItemsView<Cell>;
	        var items = it.TemplatedItems;

	        var firstRowTitle = items.FirstOrDefault();
	        if (firstRowTitle != null)
	        {
	            var findByName = firstRowTitle.FindByName<PerfLabel>("Title");
	            sb.Append($"List[0].Title: {findByName.Measures}, {findByName.Layouts}; ");
	        }
	        StatsLabel.Text = sb.ToString();
	    }

	    private void ProductsList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
	    {
	        var eSelectedItem = (Product)e.SelectedItem;
	        ProductView.SetProduct(eSelectedItem);
	    }
	}
}