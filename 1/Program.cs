using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _1 {

    class Program {
        static async Task Main (string[] args) {
            Fetcher obj = new Fetcher ();
            Console.WriteLine ("=====================\nGet Method : \n{0}", obj.Get ("https://httpbin.org/get"));
            Console.WriteLine ("=====================\nDelete Method : \n{0}", obj.Delete ("https://httpbin.org/delete"));
            var jsonData = @"
  {
    ""id"": 30,
    ""name"": ""Someone""
  }
";

            Console.WriteLine ("=======================\nPost Method :\n" + obj.Post ("https://httpbin.org/post", jsonData));
            Console.WriteLine ("=======================\nPut Method :\n" + obj.Put ("https://httpbin.org/put", jsonData));
            Console.WriteLine ("=======================\nPatch Method :\n" + obj.Patch ("https://httpbin.org/patch", jsonData));
        }
    }
    public class Fetcher {
        private async Task<string> Request (string url, HttpMethod funct, string data = "") {
            HttpClient client = new HttpClient ();
            var stringCont = new StringContent (data, UnicodeEncoding.UTF8, "application/json");
            HttpRequestMessage req = new HttpRequestMessage (funct, url);
            req.Content = stringCont;
            HttpResponseMessage response = await client.SendAsync (req);
            return await response.Content.ReadAsStringAsync ();
        }

        public string Get (string url) {
            return Request (url, HttpMethod.Get).Result;
        }

        public string Delete (string url) {
            return Request (url, HttpMethod.Delete).Result;
        }

        public string Post (string url, string data) {
            return Request (url, HttpMethod.Post, data).Result;
        }

        public string Put (string url, string data) {
            return Request (url, HttpMethod.Put, data).Result;
        }

        public string Patch (string url, string data) {
            return Request (url, HttpMethod.Patch, data).Result;
        }

    }
}