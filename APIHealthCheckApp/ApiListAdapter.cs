using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using APIHealthCheckApp.Model;

namespace APIHealthCheckApp
{
    class ApiListAdapter : BaseAdapter<API>
    {
        Activity context;
        List<API> list;

        public ApiListAdapter(Activity _context, List<API> _list)
        : base()
        {
            this.context = _context;
            this.list = _list;
        }
        public override API this[int position]
        {
            get { return list[position]; }
        }

        public override int Count
        {
            get { return list.Count; }
        }

        public override long GetItemId(int position)
        {
            return position;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView;

            // re-use an existing view, if one is available
            // otherwise create a new one
            if (view == null)
                view = context.LayoutInflater.Inflate(Resource.Layout.apiListRow, parent, false);

            API item = this[position];
            view.FindViewById<TextView>(Resource.Id.apiName).Text = item.apiName;
            view.FindViewById<TextView>(Resource.Id.apiStatus).Text = item.apiStatus;
            return view;
        }
    }
}