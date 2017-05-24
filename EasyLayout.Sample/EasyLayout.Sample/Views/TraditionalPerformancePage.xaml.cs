using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyLayout.Droid.Sample.Models;
using EasyLayout.Sample.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace EasyLayout.Sample.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TraditionalPerformancePage : ContentPage
	{
		public TraditionalPerformancePage ()
		{
			InitializeComponent ();
		    ProductsList.ItemsSource = Data.GetProducts();
		}

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        MainTitle.PrintStats();
	        TraditionalHeader.PrintStats();
	        var it = ProductsList as ITemplatedItemsView<Cell>;
	        var items = it.TemplatedItems;
	        foreach (var item in items)
	        {
	            var findByName = item.FindByName<PerfLabel>("Title");
	            findByName.PrintStats();
            }
        }

	    private void AggregateStats_OnClicked(object sender, EventArgs e)
	    {
            StringBuilder sb = new StringBuilder();

	        sb.Append($"Header[0]: {TraditionalHeader.SpeakerDeptMeasures}, {TraditionalHeader.SpeakerDeptLayouts}; ");

	        var it = ProductsList as ITemplatedItemsView<Cell>;
	        var items = it.TemplatedItems;

	        var firstRowTitle = items.FirstOrDefault();
	        if (firstRowTitle != null)
	        {
	            var findByName = firstRowTitle.FindByName<PerfLabel>("Title");
	            sb.Append($"List[0].Title: {findByName.Measures}, {findByName.Layouts}; ");
	        }
	        MainTitle.Text = sb.ToString();
	    }
    }
}