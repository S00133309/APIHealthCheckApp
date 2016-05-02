using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Security.Cryptography;
using System.Text;
using APIHealthCheckApp.Model;
using System.Net;
using System.Json;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace APIHealthCheckApp
{
    [Activity(Label = "API HealthCheck", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            // Get our button from the layout resource,
            // and attach an event to it
            Button btnLogin = FindViewById<Button>(Resource.Id.btnLogin);

            btnLogin.Click += BtnLogin_Click;
        }

        private void BtnLogin_Click(object sender, EventArgs e)
        {
            EditText email = FindViewById<EditText>(Resource.Id.textEmailAddress);
            EditText password = FindViewById<EditText>(Resource.Id.textPassword);
            string passwordHashed = password.Text;
            using (MD5 md5Hash = MD5.Create())
            {
                passwordHashed = GetMd5Hash(md5Hash, passwordHashed);
            }
            Person person = new Person();
            person.email = email.Text;
            person.password = passwordHashed;
            string jsonPerson = JsonConvert.SerializeObject(person);
            login(jsonPerson);

        }
        private async void login(string person)
        {
            byte[] personByte = Encoding.ASCII.GetBytes(person);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(new Uri("http://52.48.42.14:8080/APIHealthCheck/login"));
            request.ContentType = "application/json";
            request.Method = "POST";
            request.ContentLength = personByte.Length;

            Stream dataStream = request.GetRequestStream();
            dataStream.Write(personByte, 0, personByte.Length);
            dataStream.Close();

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

            if (jsonDoc != null)
            {
                Intent intent = new Intent(this, typeof(Personal_Activity));
                intent.PutExtra("id", jsonDoc["id"].ToString());
                StartActivity(intent);
            }

        }
        static string GetMd5Hash(MD5 md5Hash, string input)
        {
            byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
            StringBuilder sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}

