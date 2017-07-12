using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Runtime;
using Android.Util;
using FragmentThing;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Debug = System.Diagnostics.Debug;
using View = Android.Views.View;

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
			var vg = new FragmentThingViewGroup(this);

			// Wrap the native control and set it as the Content of the ContentView
			view.Content = new NativeViewWrapper(vg);
			
			// If we want to override the way the control is measured, laid out, or sized, we can do it 
			// with the additional parameters of NativeViewWrapper:
			view.Content = new NativeViewWrapper(vg, getDesiredSizeDelegate: SizeRequestDelegate);
		}

		private SizeRequest? SizeRequestDelegate(NativeViewWrapperRenderer renderer,
			int widthConstraint, int heightConstraint)
		{
			var size = new Xamarin.Forms.Size(double.PositiveInfinity, 500);
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

			// Or we can access the wrapped native control and modify its properties
			(wrapper.NativeView as ViewGroup).SetBackgroundColor(Color.Aqua.ToAndroid());;
			(wrapper.NativeView as ViewGroup).SetPadding(20, 20, 20, 20);
		}
	}
}

