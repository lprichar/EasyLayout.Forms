using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace EasyLayout.Sample.Controls
{
	public partial class TraditionalHeader : ContentView
	{
		public TraditionalHeader ()
		{
			InitializeComponent ();
		}

	    public void PrintStats()
	    {
	        SpeakerDept.PrintStats();
	        SpeakerName.PrintStats();
            Moar.PrintStats();
        }
    }
}