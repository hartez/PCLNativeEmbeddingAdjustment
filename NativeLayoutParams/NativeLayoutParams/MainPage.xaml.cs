using Xamarin.Forms;

namespace NativeLayoutParams
{
	public partial class MainPage : ContentPage
	{
		public const string AdjustNativeControlSignal = "AdjustNativeControls";
		public const string FillInNativeControlSignal = "FillIn";

		public MainPage()
		{
			InitializeComponent();

			// Let any interested native projects know that we're ready for them to update
			// (or create) native controls
			MessagingCenter.Send(this, FillInNativeControlSignal, Placeholder);
			MessagingCenter.Send(this, AdjustNativeControlSignal, Layout.Children[1]);
		}
	}
}
