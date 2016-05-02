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

namespace APIHealthCheckApp.Model
{
    class Result
    {
        public string resultStatus { get; set; }
        public string resultDate { get; set; }
    }
}