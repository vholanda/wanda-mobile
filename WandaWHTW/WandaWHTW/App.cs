using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WandaWHTW
{
	public class App : Application
	{
		public static float ScreenWidth;
		public static float ScreenHeight;
		public App()
		{
			//AgendaCell cell = new AgendaCell();
			CheckMainPage();
		}

		public void CheckMainPage()
		{
			var token = CrossSettings.Current.GetValueOrDefault<String>("token");

			if (String.IsNullOrEmpty(token))// não logado, vamos logar
			{
				MainPage = new LoginPage();
			}
			else //logado vamos direto pra tela principal
			{

				MasterDetailPage mdPage = new MasterDetailPage()
				{
					Master = new MasterPage(),
					Detail = new Page(),
				};

				(mdPage.Master as MasterPage).MasterDetail = mdPage;
				(mdPage.Master as MasterPage).GoToMainPage();
				// The root page of your application
				MainPage = mdPage;
			}
		}


		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
