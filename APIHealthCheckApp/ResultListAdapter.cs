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
    class ResultListAdapter : BaseAdapter<Model.Result>
    {
        Activity context;
        List<Model.Result> list;

        public ResultListAdapter(Activity _context, List<Model.Result> _list)
        : base()
        {
            this.context = _context;
            this.list = _list;
        }
        public override Model.Result this[int position]
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
                view = context.LayoutInflater.Inflate(Resource.Layout.resultListRow, parent, false);

            Model.Result item = this[position];
            view.FindViewById<TextView>(Resource.Id.resultDate).Text = item.resultDate;
            view.FindViewById<TextView>(Resource.Id.resultStatus).Text = item.resultStatus;
            return view;
        }
    }
}