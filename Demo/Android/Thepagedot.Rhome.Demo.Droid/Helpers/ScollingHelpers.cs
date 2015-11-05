using System;
using Android.Widget;
using Android.Views;

namespace Thepagedot.Rhome.Demo.Droid
{
    public static class ScollingHelpers
    {
        public static void SetListViewHeightBasedOnChildren(AbsListView listView) 
        {
            var listAdapter = listView.Adapter;
            if (listAdapter == null)
                return;

            int desiredWidth = Android.Views.View.MeasureSpec.MakeMeasureSpec(listView.Width, MeasureSpecMode.Unspecified);
            int totalHeight = 0;
            View view = null;
            for (int i = 0; i < listAdapter.Count; i++) {
                view = listAdapter.GetView(i, view, listView);
                if (i == 0)
                    view.LayoutParameters = new ViewGroup.LayoutParams(desiredWidth, WindowManagerLayoutParams.WrapContent);

                //view.Measure(desiredWidth, MeasureSpecMode.Unspecified);
                view.Measure(desiredWidth, 0);
                totalHeight += view.MeasuredHeight;
            }
            ViewGroup.LayoutParams p = listView.LayoutParameters;

            if (listView is ListView)
                p.Height = totalHeight + (((ListView)listView).DividerHeight * (listAdapter.Count - 1));
            else
                p.Height = totalHeight / 2;

            listView.LayoutParameters = p;
        }
    }        
}

