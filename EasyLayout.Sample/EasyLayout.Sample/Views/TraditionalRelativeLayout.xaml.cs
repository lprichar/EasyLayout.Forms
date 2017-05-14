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
	public partial class TraditionalRelativeLayout : ContentPage
	{
		public TraditionalRelativeLayout ()
		{
			InitializeComponent ();
		    Title = "Traditional XAML RelativeLayout";
		}
	}
}
