using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace FragmentThing
{
	public class FragmentThing : Fragment
	{
		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			var tv = new TextView(Activity) {Text = "I'm a fragment."};
			tv.SetBackgroundColor(Color.Bisque);
			tv.Gravity = GravityFlags.Center;

			return tv;
		}
	}

	public class FragmentThingViewGroup : ViewGroup
	{
		public FragmentThingViewGroup(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
		{
		}

		public FragmentThingViewGroup(Context context) : base(context)
		{
			Id = Android.Views.View.GenerateViewId();
			var fragmentThing = new FragmentThing();

			var activity = context as Activity;

			if (activity == null)
			{
				return;
			}

			var transaction = activity.FragmentManager.BeginTransaction();
			transaction.Add(Id, fragmentThing);
			transaction.CommitAllowingStateLoss();
			activity.FragmentManager.ExecutePendingTransactions();
		}

		public FragmentThingViewGroup(Context context, IAttributeSet attrs) : base(context, attrs)
		{
		}

		public FragmentThingViewGroup(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
		{
		}

		public FragmentThingViewGroup(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes) : base(context, attrs, defStyleAttr, defStyleRes)
		{
		}

		protected override void OnMeasure(int widthMeasureSpec, int heightMeasureSpec)
		{
			View v = GetChildAt(0);
			v.Measure(widthMeasureSpec, heightMeasureSpec);
			SetMeasuredDimension(v.MeasuredWidth, v.MeasuredHeight);
		}

		protected override void OnLayout(bool changed, int l, int t, int r, int b)
		{
			View v = GetChildAt(0);
			v.Layout(l, t, r, b);
		}
	}
}
