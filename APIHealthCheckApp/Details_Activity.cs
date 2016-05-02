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
using Newtonsoft.Json;

namespace APIHealthCheckApp
{
    [Activity(Label = "API HealthCheck")]
    public class Details_Activity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            int position = Convert.ToInt32(Intent.GetStringExtra("Position") ?? null);
            string list = Intent.GetStringExtra("List") ?? null;
            List<API> listData = JsonConvert.DeserializeObject<List<API>>(list);
            // Create your application here
            SetContentView(Resource.Layout.APIDetails);

            API api = listData[position];

            TextView apiName = FindViewById<TextView>(Resource.Id.apiName);
            TextView apiStatus = FindViewById<TextView>(Resource.Id.apiStatus);
            TextView apiDate = FindViewById<TextView>(Resource.Id.apiDate);
            apiName.Text = api.apiName;
            if (api.apiStatus != "No Status")
            {
                apiStatus.Text = "Current Status : " + api.apiStatus;
                apiDate.Text = "Date Pinged : " + api.dates[0];

                ListView resultList = FindViewById<ListView>(Resource.Id.ResultList);
                List<Model.Result> resultsList = new List<Model.Result>();
                Model.Result result;
                for (int i = 0; i < api.dates.Length; i++)
                {
                    result = new Model.Result();
                    result.resultDate = api.dates[i];
                    result.resultStatus = api.results[i];
                    resultsList.Add(result);
                }
                resultList.Adapter = new ResultListAdapter(this, resultsList);
            }
            else
            {
                apiStatus.Text = "Current Status : " + api.apiStatus;
                apiDate.Text = "Date Pinged : -/-/-";
            }

        }
    }
}