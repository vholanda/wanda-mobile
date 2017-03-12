using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;

namespace WandaWHTW.Droid
{
	[Activity(Label = "WandaWHTW", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, Theme = "@style/myTheme")]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{

			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			global::Xamarin.Forms.Forms.Init(this, bundle);

			App.ScreenWidth = (int)Resources.DisplayMetrics.WidthPixels;
			App.ScreenHeight = (int)Resources.DisplayMetrics.HeightPixels;

			LoadApplication(new WandaWHTW.App());
		}
	}
}

