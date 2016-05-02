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
    class API
    {
        public int apiID { get; set; }
        public string apiName { get; set; }
        public string apiStatus { get; set; }
        public string[] results { get; set; }
        public string[] dates { get; set; }
    }
}