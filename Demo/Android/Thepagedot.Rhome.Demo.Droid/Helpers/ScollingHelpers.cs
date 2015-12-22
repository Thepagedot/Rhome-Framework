using System;
using Android.Widget;
using Android.Views;

namespace Thepagedot.Rhome.Demo.Droid
{
    public static class ScollingHelpers
    {
        public static void SetListViewHeightBasedOnChildren(AbsListView listView, float marginBetweenItems) 
        {
            var listAdapter = listView.Adapter;
            if (listView == null || listAdapter == null || listAdapter.Count == 0)
                return;

            int desiredWidth = View.MeasureSpec.MakeMeasureSpec(listView.Width, MeasureSpecMode.Unspecified);
            int totalHeight = 0;
            View view = null;

            for (int i = 0; i < listAdapter.Count; i++)
            {
                view = listAdapter.GetView(i, view, listView);
                if (i == 0)
                    view.LayoutParameters = new ViewGroup.LayoutParams(desiredWidth, WindowManagerLayoutParams.WrapContent);

                //view.Measure(desiredWidth, MeasureSpecMode.Unspecified);
                view.Measure(desiredWidth, 0);
                totalHeight += view.MeasuredHeight;
            }

            // Add margin
            totalHeight += Convert.ToInt32((float)(listAdapter.Count - 1) * marginBetweenItems);

            if (listView is ListView)
            {
                // Add divider
                totalHeight += (((ListView)listView).DividerHeight * (listAdapter.Count - 1));
            }

            if (listView is GridView)
            {
                // Consider multiple columns
                var columns = ((GridView)listView).NumColumns;
                var denominator = listAdapter.Count / columns;
                var singleItemHeight = totalHeight / listAdapter.Count;

                totalHeight = totalHeight / columns;
                if (listAdapter.Count % denominator != 0)
                    totalHeight += singleItemHeight / 2;
            }

            ViewGroup.LayoutParams p = listView.LayoutParameters;
            p.Height = totalHeight;

            listView.LayoutParameters = p;
        }
    }        
}

