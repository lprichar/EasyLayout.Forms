using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
		}

	    private void Button_OnClicked(object sender, EventArgs e)
	    {
	        MainTitle.PrintStats();
	        TraditionalHeader.PrintStats();
        }
	}
}