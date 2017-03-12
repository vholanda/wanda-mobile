using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WandaWHTW
{
	public class ChatsPage : ContentPage
	{
		ListView lista;
		public ChatsPage()
		{
			this.Title = "Minhas Consultas";
	

			lista = new ListView(ListViewCachingStrategy.RetainElement)
			{
				IsPullToRefreshEnabled = true,
			};
			lista.Refreshing += Lista_Refreshing;
			lista.ItemSelected += Lista_ItemSelected;
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

		private async void Lista_ItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await this.Navigation.PushAsync(new HistoricChatPage((e.SelectedItem as Model).Id));
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();
			await this.RefreshList();
		}

		public async Task RefreshList()
		{
			this.lista.ItemsSource = await Repository.ListarChats();
			this.lista.IsRefreshing = false;
		}


		public class Model
		{
			public Guid Id { get; set; }

			public DateTime StartedDate { get; set; }

			public DateTime? FinishDate { get; set; }

			public override string ToString()
			{
				return StartedDate.ToShortDateString() + " - " + FinishDate?.ToShortDateString();
			}
		}
	}
}
