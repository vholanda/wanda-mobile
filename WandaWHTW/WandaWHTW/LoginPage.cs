using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace WandaWHTW
{
	public class LoginPage : ContentPage
	{
		Entry rgInput, passwordInput;
		Button logarButton;
		public LoginPage()
		{
			Title = "Login";
			rgInput = new Entry()
			{
				Placeholder = "seu RG aqui",
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Keyboard = Keyboard.Numeric
			};
			rgInput.Completed += (s, e) => passwordInput.Focus();

			passwordInput = new Entry()
			{
				IsPassword = true,
				Placeholder = "sua senha aqui",
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
			};

			passwordInput.Completed += (s, e) => Logar(s, e);

			logarButton = new Button()
			{
				HorizontalOptions = LayoutOptions.Fill,
				VerticalOptions = LayoutOptions.Center,
				Text = "Acessar",
				BackgroundColor = AppStyle.MainColor,
				TextColor = AppStyle.ClearColor,
			};

			logarButton.Clicked += Logar;

			Content = new StackLayout
			{
				VerticalOptions = LayoutOptions.Center,
				Padding = new Thickness(30, 0),
				Children = {
					new Label { Text = "Bem vinda(o) a Wanda", FontSize= Device.GetNamedSize(NamedSize.Large, typeof(Label)), HorizontalOptions = LayoutOptions.Center, },
					new Label { Text = "Faça seu acesso", FontSize = Device.GetNamedSize(NamedSize.Small, typeof(Label)), HorizontalOptions = LayoutOptions.Center },
					rgInput,
					passwordInput,
					logarButton
				}
			};
		}

		private async void Logar(object sender, EventArgs e)
		{
			logarButton.IsEnabled = false;
			if(await Repository.Login(rgInput.Text, passwordInput.Text))
			{
				(App.Current as App).CheckMainPage();
			}
			else
			{
				await DisplayAlert("Erro ao acessar", "Não foi possivel acessar, verifique seu RG e senha", "ok");
				rgInput.Focus();
			}
			logarButton.IsEnabled = true;
		}
	}
}
