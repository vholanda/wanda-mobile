using Plugin.Settings;
using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WandaWHTW
{
	public class MasterPage : ContentPage
	{
		PageButton btnAgenda, btnChat, btnAllChats;
		Button btnLogoff;
		public MasterDetailPage MasterDetail { get; set; }

		public MasterPage()
		{
			this.Title = "Wanda";
			btnAgenda = new PageButton(new AgendaPage())
			{
				Text = "Agenda",
				TextColor = AppStyle.MainColor,
				HorizontalOptions = LayoutOptions.Fill,
			};
			btnAgenda.Clicked += PageButton_Clicked;

			btnChat = new PageButton(new ConsultaPage())
			{
				Text = "Consulta atual",
				TextColor = AppStyle.MainColor,
				HorizontalOptions = LayoutOptions.Fill
			};
			btnAllChats = new PageButton(new ChatsPage())
			{
				Text = "Minhas Consultas",
				TextColor = AppStyle.MainColor,
				HorizontalOptions = LayoutOptions.Fill
			};
			btnAllChats.Clicked += PageButton_Clicked;

			btnLogoff = new Button()
			{
				Text = "Entrar com outro usuário",
				TextColor = AppStyle.MainColor,
				HorizontalOptions = LayoutOptions.Fill
			};

			btnLogoff.Clicked += (s, e) => { CrossSettings.Current.AddOrUpdateValue<String>("token", null); (App.Current as App).CheckMainPage(); };

			btnChat.Clicked += PageButton_Clicked;

			Content = new StackLayout
			{
				Children =
				{
					new StackLayout()
					{
						HorizontalOptions = LayoutOptions.Fill,
						VerticalOptions = LayoutOptions.Start,
						BackgroundColor = AppStyle.MainColor,
						HeightRequest = 200,
						Padding = new Thickness(20),
						Children =
						{
							new Label() {
								Text ="Wanda - WHTW",
								FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
								VerticalOptions = LayoutOptions.Center,
								HorizontalOptions=  LayoutOptions.Center
							}
						},
					},
					new StackLayout()
					{
						HorizontalOptions = LayoutOptions.Fill,
						VerticalOptions = LayoutOptions.Fill,
						Children =
						{
							btnChat,
							btnAllChats,
							btnAgenda,
							btnLogoff
						}
					}
				}
			};
		}

		private async void PageButton_Clicked(object sender, EventArgs e)
		{
			PageButton s = sender as PageButton;
			if (s == null) return;
			await this.ChangePage(s.Page);
		}

		public async Task GoToMainPage()
		{
			Guid id = CrossSettings.Current.GetValueOrDefault<Guid>("current_conversation");
			if (id == Guid.Empty)
			{
				await ChangePage(btnAllChats.Page);
			}
			else
			{
				await ChangePage(btnChat.Page);
			}
		}

		public async Task ChangePage(Page page)
		{
			this.MasterDetail.Detail = new NavigationPage(page)
			{
				BarBackgroundColor = AppStyle.MainColor,
				BarTextColor = AppStyle.ClearColor
			};
		}

		internal void GotoAgenda()
		{
			this.ChangePage(this.btnAgenda.Page);
		}
	}
}
