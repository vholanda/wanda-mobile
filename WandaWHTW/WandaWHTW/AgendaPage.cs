using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace WandaWHTW
{
	public class AgendaPage : ContentPage
	{
		ListView lista;
		DatePicker picker;
		public AgendaPage()
		{
			this.Title = "Agenda";
			picker = new DatePicker()
			{
				TextColor = AppStyle.ClearColor,
				Date = DateTime.Now
			};

			var stackL = new StackLayout()
			{
				BackgroundColor = AppStyle.MainColor,
				Children =
				{
					new Label() { Text = "Data", TextColor = AppStyle.ClearColor },
					picker
				}
			};

			lista = new ListView(ListViewCachingStrategy.RetainElement)
			{
				IsPullToRefreshEnabled = true,
			};
			lista.Refreshing += Lista_Refreshing;

			var relLayout = new RelativeLayout();
			relLayout.Children.Add(stackL,
				Constraint.Constant(0),
				Constraint.Constant(0),
				Constraint.RelativeToParent(p => p.Width),
				Constraint.RelativeToParent(p => p.Height * 0.15));

			relLayout.Children.Add(lista,
				Constraint.Constant(0),
				Constraint.RelativeToParent(p => p.Height * 0.15),
				Constraint.RelativeToParent(p => p.Width),
				Constraint.RelativeToParent(p => p.Height * 0.85));

			Content = relLayout;
		}

		private async void Lista_Refreshing(object sender, EventArgs e)
		{
			await RefreshList();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await this.RefreshList();
		}

		public async Task RefreshList()
		{
			//var cell = new DataTemplate(typeof(AgendaCell));
			//cell.SetBinding(AgendaCell.FromTimeProperty, "FromTime");
			//cell.SetBinding(AgendaCell.ToTimeProperty, "ToTime");
			//cell.SetBinding(AgendaCell.StatusProperty, "Status");

			var dates = Model.GetDay().ToArray();

			var dt = await Repository.GetAgenda(picker.Date);
			foreach (var item in dt)
			{
				item.SetToTime();
			}
			var x = dt.Union(dates).OrderBy(d => d.FromTime);
			this.lista.ItemsSource = x;
			this.lista.IsRefreshing = false;
		}

		public class Model
		{
			public TimeSpan FromTime { get; set; }

			public TimeSpan ToTime { get; set; }

			public String Hospital { get; set; }

			public AgendaCell.ModelStatus Status { get; set; }

			public Model(TimeSpan fromTime)
			{
				FromTime = fromTime;
				SetToTime();
				Status = AgendaCell.ModelStatus.Middle;
			}

			public Model()
			{

			}

			public void SetToTime()
			{
				ToTime = FromTime.Add(new TimeSpan(0, 0, 45, 0));
			}

			public override string ToString()
			{
				if (!String.IsNullOrEmpty(Hospital))
					return FromTime.ToString("hh\\:mm") + " até " + ToTime.ToString("hh\\:mm") + " no " + Hospital;
				return FromTime.ToString("hh\\:mm") + " até " + ToTime.ToString("hh\\:mm");
			}

			public static IEnumerable<Model> GetDay()
			{
				TimeSpan current = TimeSpan.Zero;
				TimeSpan day = new TimeSpan(24, 0, 0);
				TimeSpan space = new TimeSpan(0, 0, 45, 0);
				while (current < day)
				{
					yield return new Model(current);
					current = current.Add(space);
				}
			}
		}
	}
}
