using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace WandaWHTW
{
	public class AgendaCell : ViewCell
	{
		public enum ModelStatus
		{
			Start,
			Middle,
			End,
			Only
		}

		public static readonly BindableProperty FromTimeProperty = BindableProperty.Create(nameof(FromTime), typeof(DateTime), typeof(AgendaCell), TimeSpan.MinValue);
		public static readonly BindableProperty ToTimeProperty = BindableProperty.Create(nameof(ToTime), typeof(DateTime), typeof(AgendaCell), TimeSpan.MinValue);
		public static readonly BindableProperty StatusProperty = BindableProperty.Create(nameof(Status), typeof(ModelStatus), typeof(AgendaCell), ModelStatus.Middle);

		public TimeSpan FromTime
		{
			get
			{
				return (TimeSpan)this.GetValue(FromTimeProperty);
			}
			set
			{ this.SetValue(FromTimeProperty, value); }
		}

		public TimeSpan ToTime
		{
			get
			{ return (TimeSpan)this.GetValue(ToTimeProperty); }
			set
			{ this.SetValue(ToTimeProperty, value); }
		}

		public ModelStatus Status
		{
			get
			{ return (ModelStatus)GetValue(StatusProperty); }
			set
			{ SetValue(StatusProperty, value); }
		}



		private Label _lbFromHour, _lbToHour, _lbUntill;
		private BoxView _lowerLine, _upperLine, _square;
		private const int squareSize = 10;

		public AgendaCell()
		{
			//_lbFromHour = new Label()
			//{
			//	Text = "12:00",
			//	Margin = new Thickness(0),
			//};
			//_lbToHour = new Label()
			//{
			//	Text = "14:00",
			//	Margin = new Thickness(0),
			//};
			//_lbUntill = new Label()
			//{
			//	Text = "até",
			//	Margin = new Thickness(0)
			//};

			//_lbUntill.FontSize = Device.GetNamedSize(NamedSize.Small, _lbUntill);
			//_lbFromHour.FontSize = Device.GetNamedSize(NamedSize.Large, _lbFromHour);
			//_lbToHour.FontSize = Device.GetNamedSize(NamedSize.Large, _lbToHour);

			//var layout = new RelativeLayout()
			//{
			//	Padding = new Thickness(10),
			//	StyleClass = new[] { "Clear" },
			//};

			//_lowerLine = new BoxView() { BackgroundColor = AppStyle.SecondaryColor };
			//_upperLine = new BoxView() { BackgroundColor = AppStyle.SecondaryColor };
			//_square = new BoxView() { BackgroundColor = AppStyle.SecondaryColor };



			//layout.Children.Add(_square,
			//	Constraint.RelativeToParent(p => p.Width / 6 - squareSize / 2),
			//	Constraint.RelativeToView(_lbFromHour, (p, v) => v.Y + (v.Height / 2) - squareSize / 2),
			//	Constraint.Constant(squareSize),
			//	Constraint.Constant(squareSize));

			//layout.Children.Add(_upperLine,
			//	Constraint.RelativeToView(_square, (p, v) => v.X + v.Width / 2 - _upperLine.Width / 2),
			//	Constraint.Constant(0),
			//	Constraint.Constant(2),
			//	Constraint.RelativeToView(_square, (p, v) => v.Y + v.Height / 2));

			//layout.Children.Add(_lowerLine,
			//	Constraint.RelativeToView(_upperLine, (p, v) => v.X),
			//	Constraint.RelativeToView(_square, (p, v) => v.Y + v.Height / 2),
			//	Constraint.Constant(2),
			//	Constraint.RelativeToView(_upperLine, (p, v) => v.Height));


			//layout.Children.Add(_lbFromHour,
			//	Constraint.RelativeToView(_square, (p, v) => v.Bounds.Right + 5),
			//	Constraint.RelativeToParent(p => p.Height - _lbFromHour.Height / 2));

			//layout.Children.Add(_lbUntill, 
			//	Constraint.RelativeToView(_lbFromHour, (p,v) => v.Bounds.Right +5),
			//	Constraint.RelativeToParent(p => p.Height - _lbUntill.Height / 2));

			//layout.Children.Add(_lbToHour,
			//	Constraint.RelativeToView(_lbUntill, (p, v) => v.Bounds.Right + 5),
			//	Constraint.RelativeToParent(p => p.Height - _lbToHour.Height / 2));

			//var size = layout.Measure(0, 0, MeasureFlags.IncludeMargins);
			//layout.HeightRequest = size.Minimum.Height;
			//this.View = layout;
			this.View = new StackLayout();

		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			await this.View.LayoutTo(this.View.Bounds);
		}


		protected override void OnBindingContextChanged()
		{
			base.OnBindingContextChanged();
			if (BindingContext != null)
			{
				//_lbFromHour.Text = FromTime.ToString("hh:mm");
				//_lbToHour.Text = ToTime.ToString("hh:mm");
				switch (Status)
				{
					case ModelStatus.Start:
						_upperLine.IsVisible = false;
						_lowerLine.IsVisible = true;
						break;
					case ModelStatus.Middle:
						_upperLine.IsVisible = true;
						_lowerLine.IsVisible = false;
						break;
					case ModelStatus.Only:
						_upperLine.IsVisible = false;
						_lowerLine.IsVisible = false;
						break;
					default:
						_upperLine.IsVisible = true;
						_lowerLine.IsVisible = true;
						break;
				}
			}
			if (Device.OS == TargetPlatform.iOS)
			{
				this.View.Layout(this.View.Bounds);
			}
		}
	}
}
