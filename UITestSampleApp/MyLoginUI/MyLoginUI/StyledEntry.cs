using System;
using Xamarin.Forms;

namespace MyLoginUI.Views
{
	public class StyledEntry : Entry
	{
		public StyledEntry(double opacity = 0)
		{
			BackgroundColor = Color.Transparent;
			HeightRequest = 40;
			TextColor = Color.White;
			Opacity = opacity;
			PlaceholderColor = Color.White;
		}

		public new event EventHandler Completed;

		public static readonly BindableProperty ReturnTypeProperty =
			BindableProperty.Create<StyledEntry, ReturnType>(s => s.ReturnType, ReturnType.Done);

		public ReturnType ReturnType
		{
			get { return (ReturnType)GetValue(ReturnTypeProperty); }
			set { SetValue(ReturnTypeProperty, value); }
		}

		public void InvokeCompleted()
		{
			Completed?.Invoke(this, null);
		}
	}

	public enum ReturnType
	{
		Go,
		Next,
		Done,
		Send,
		Search
	}
}
