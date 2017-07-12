using Android.App;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace NativeLayoutParams.Droid
{
	[Activity(Label = "NativeLayoutParams", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			// Listen for requests from the PCL project to do native Android things with native Android controls
			MessagingCenter.Subscribe<NativeLayoutParams.MainPage, Xamarin.Forms.View>(this, MainPage.AdjustNativeControlSignal, AdjustNativeControl);
			MessagingCenter.Subscribe<NativeLayoutParams.MainPage, Xamarin.Forms.ContentView>(this, MainPage.FillInNativeControlSignal, FillInNativeControl);

			global::Xamarin.Forms.Forms.Init(this, bundle);
			LoadApplication(new App());
		}

		// Method 1: The PCL project hands us a placeholder which we can fill in with a freshly constructed native control
		private void FillInNativeControl(MainPage mainPage, ContentView view)
		{
			// Create the native control
			var tv = new TextView(this) {Text = "I'm a native text view"};
			tv.SetBackgroundColor(Color.Coral.ToAndroid());

			// Wrap the native control and set it as the Content of the ContentView
			view.Content = new NativeViewWrapper(tv, getDesiredSizeDelegate: SizeRequestDelegate);
		}

		private SizeRequest? SizeRequestDelegate(NativeViewWrapperRenderer renderer,
			int widthConstraint, int heightConstraint)
		{
			var size = new Xamarin.Forms.Size(200, 200);
			return new SizeRequest(size);
		}

		// Method 2: the PCL project hands us the wrapped native control so we can make adjustments
		private void AdjustNativeControl(MainPage mainPage, Xamarin.Forms.View view)
		{
			// The native control has alread been wrapped in a NativeViewWrapper
			var wrapper = view as NativeViewWrapper;

			if (wrapper == null)
			{
				return;
			}

			// We can manipulate the wrapper itself (which is a XF View)
			wrapper.HeightRequest = 200;
			wrapper.WidthRequest = 200;
			wrapper.BackgroundColor = Color.Aqua;

			// Or we can access the wrapped native control and modify its properties
			(wrapper.NativeView as TextView).Gravity = GravityFlags.End;
		}
	}
}

