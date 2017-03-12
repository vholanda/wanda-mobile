using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WandaWHTW
{
    public class PageButton : Xamarin.Forms.Button
	{
		public Xamarin.Forms.Page Page { get; private set; }

		public PageButton(Page page)
		{
			this.Page = page;
		}
	}
}
