using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WandaWHTW
{
	public class ConsultaPage : ContentPage
	{
		ListView chatList;
		Entry messageEntry;
		Button sendMessageButton;
		Button finalizarButton;
		public ConsultaPage()
		{
			this.Title = "Consulta";
			chatList = new ListView(ListViewCachingStrategy.RecycleElement)
			{
				VerticalOptions = LayoutOptions.Fill,
				IsPullToRefreshEnabled = true,
			};
			chatList.Refreshing += ChatList_Refreshing;
			messageEntry = new Entry()
			{
				Placeholder = "Enviar uma mensagem",
				VerticalOptions = LayoutOptions.Center,
				HorizontalOptions = LayoutOptions.StartAndExpand,
			};
			sendMessageButton = new Button()
			{
				BackgroundColor = AppStyle.SecondaryColor,
				TextColor = AppStyle.ClearColor,
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				Text = "Enviar"
			};
			sendMessageButton.Clicked += SendMessageButton_Clicked;

			finalizarButton = new Button()
			{
				BackgroundColor = AppStyle.SecondaryColor,
				TextColor = AppStyle.ClearColor,
				HorizontalOptions = LayoutOptions.End,
				VerticalOptions = LayoutOptions.Center,
				Text = "Finalizar"
			};
			finalizarButton.Clicked += FinalizarButton_Clicked;
			var relLayout = new RelativeLayout();

			relLayout.Children.Add(chatList,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent(p => p.Width),
				Constraint.RelativeToParent(p => p.Height * 0.9));

			relLayout.Children.Add(sendMessageButton,
				xConstraint: Constraint.RelativeToParent(p => p.Width * 0.7),
				yConstraint: Constraint.RelativeToParent(p => p.Height * 0.9),
				widthConstraint: Constraint.RelativeToParent(p => p.Width * 0.3),
				heightConstraint: Constraint.RelativeToParent(p => p.Height * 0.1));

			relLayout.Children.Add(finalizarButton,
				Constraint.RelativeToParent(p => p.Width * 0.5),
				Constraint.RelativeToParent(p => p.Height * 0.9),
				Constraint.RelativeToParent(p => p.Width * 0.3),
				Constraint.RelativeToParent(p => p.Height * 0.1));

			relLayout.Children.Add(messageEntry,
				Constraint.Constant(0),
				Constraint.RelativeToParent(p => p.Height * 0.9),
				Constraint.RelativeToParent(p => p.Width * 0.5),
				Constraint.RelativeToParent(p => p.Height * 0.1));

			Content = relLayout;
		}

		private async void FinalizarButton_Clicked(object sender, EventArgs e)
		{
			//if (await Repository.FinalizeConsulta(conversationId))
			//{
			//	CrossSettings.Current.AddOrUpdateValue("current_conversation", Guid.Empty);
			//	conversationId = Guid.Empty;
			//	await DisplayAlert("Sucesso", "Sua consulta foi marcada", "ok");
			//	((Application.Current.MainPage as MasterDetailPage)?.Master as MasterPage).GotoAgenda();
			//}
			//else
			//{
			//	await DisplayAlert("Erro", "Não foi possivel finalizar sua consulta", "ok");
			//}
		}

		private async void ChatList_Refreshing(object sender, EventArgs e)
		{
			await RefreshList();
		}

		Guid conversationId;

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			conversationId = CrossSettings.Current.GetValueOrDefault<Guid>("current_conversation");
			if (conversationId == Guid.Empty)
			{
				//var x = await Repository.CreateConsulta();
				//CrossSettings.Current.AddOrUpdateValue("current_conversation", x.Id);
			}
			await RefreshList();
		}

		private async void SendMessageButton_Clicked(object sender, EventArgs e)
		{
			finalizarButton.IsEnabled = false;
			sendMessageButton.IsEnabled = false;

			var result = await Repository.SendMessage(conversationId, messageEntry.Text);
			await RefreshList();
			messageEntry.Text = "";

			finalizarButton.IsEnabled = true;
			sendMessageButton.IsEnabled = true;

		}

		private async Task RefreshList()
		{
			var data = await Repository.GetChat(conversationId);
			this.chatList.ItemsSource = data.Message;
			this.chatList.IsRefreshing = false;
		}
	}
}
