using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WandaWHTW
{
	public class HistoricChatPage : ContentPage
	{
		ListView lista;
		Guid conversationId;
		public HistoricChatPage(Guid conversationId)
		{
			this.Title = "Minhas Consultas";
			this.conversationId = conversationId;

			lista = new ListView(ListViewCachingStrategy.RetainElement)
			{
				IsPullToRefreshEnabled = true,
			};
			lista.Refreshing += Lista_Refreshing;

			var relLayout = new RelativeLayout();
			relLayout.Children.Add(lista,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent(p => p.Width),
				Constraint.RelativeToParent(p => p.Height));

			Content = relLayout;
		}

		private async void Lista_Refreshing(object sender, EventArgs e)
		{
			await RefreshList();
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await this.RefreshList();
		}

		public async Task RefreshList()
		{
			this.lista.ItemsSource = (await Repository.GetChat(conversationId)).Message;
			this.lista.IsRefreshing = false;
		}

		public class Model
		{
			public Guid Id { get; set; }
			public DateTime StartedDate { get; set; }
			public DateTime? FinshDate { get; set; }
			public Guid UserId { get; set; }
			public List<MessageList> Message { get; set; }
		}

		public class MessageList
		{

			public Guid Id { get; set; }
			public String Message1 { get; set; }
			public bool FromUser { get; set; }

			public override string ToString()
			{
				return (FromUser ? "Wanda: " : "Você: ") + Message1;
			}
		}
	}
}
