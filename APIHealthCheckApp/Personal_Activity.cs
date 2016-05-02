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
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Json;
using Newtonsoft.Json;

namespace APIHealthCheckApp
{
    [Activity(Label = "API HealthCheck")]
    public class Personal_Activity : Activity
    {
        int id;
        List<API> listData = new List<API>();
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            id = Convert.ToInt32(Intent.GetStringExtra("id") ?? null);
            popList();

            SetContentView(Resource.Layout.PersonalList);
        }

        private async void popList()
        {
            // Create an HTTP web request using the URL:
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://52.48.42.14:8080/APIHealthCheck/personal/get/" + id));
            request.ContentType = "application/json";
            request.Method = "GET";
            JsonValue jsonDoc;
            // Send the request to the server and wait for the response:
            using (WebResponse response = await request.GetResponseAsync())
            {
                // Get a stream representation of the HTTP web response:
                using (Stream stream = response.GetResponseStream())
                {
                    // Use this stream to build a JSON document object:
                    jsonDoc = await Task.Run(() => JsonObject.Load(stream));
                }
            }
            listData = new List<API>();
            foreach (JsonValue item in jsonDoc)
            {
                string str;
                API api = new API();
                api.apiID = item["id"];
                api.apiName = item["name"];
                api.results = JsonConvert.DeserializeObject<string[]>(item["lastTenResultStatus"].ToString());
                api.dates = JsonConvert.DeserializeObject<string[]>(item["lastTenDates"].ToString());
                str = item["currentStatus"];
                if (str != null)
                    api.apiStatus = item["currentStatus"];
                else
                    api.apiStatus = "No Status";
                listData.Add(api);
            }

            ListView apiList = FindViewById<ListView>(Resource.Id.ApiList);
            apiList.ItemClick += ApiList_ItemClick;
            apiList.Adapter = new ApiListAdapter(this, listData);
        }

        private void ApiList_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            string jsonList = JsonConvert.SerializeObject(listData);
            Intent intent = new Intent(this, typeof(Details_Activity));
            intent.PutExtra("Position", e.Position.ToString());
            intent.PutExtra("List", jsonList);
            StartActivity(intent);
        }
    }
}