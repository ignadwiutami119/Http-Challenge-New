using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _4 {
    class Program {
        public static async Task Main (string[] args) {
            string Keanu_Api = "https://api.themoviedb.org/3/discover/movie?api_key=a4c56a26e242d6de8f754831da132e90&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_cast=6384";
            string Indo_Api = "https://api.themoviedb.org/3/search/movie?api_key=a4c56a26e242d6de8f754831da132e90&language=en-US&query=Indonesia&page=1&include_adult=false";
            string RobertTom_Api = "https://api.themoviedb.org/3/discover/movie?api_key=a4c56a26e242d6de8f754831da132e90&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_cast=1136406%2C3223";
            string mov2016 = "https://api.themoviedb.org/3/discover/movie?api_key=a4c56a26e242d6de8f754831da132e90&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&primary_release_year=2016&vote_average.gte=7.5";
            Console.WriteLine("\n==================================");
            Console.WriteLine ("10+ TITLES FROM INDONESIAN MOVIES");
            Console.WriteLine("==================================");
            await Indo_Movies (Indo_Api);
            Console.WriteLine("\n==================================");
            Console.WriteLine ("MOVIE LIST PLAYED BY KEANU REEVES");
            Console.WriteLine("==================================");
            await KeanuRev (Keanu_Api);
            Console.WriteLine("\n===================================================");
            Console.WriteLine ("MOVIE LIST PLAYED BY ROBERT DOWNEY JR & TOM HOLLAND");
            Console.WriteLine("===================================================");
            await RobertTom (RobertTom_Api);
            Console.WriteLine("\n===========================================");
            Console.WriteLine ("POPULAR MOVIES LIST THAT RELEASED ON 2016 & \n(the votes above 7.5)");
            Console.WriteLine("===========================================");
            await Movies2016 (mov2016);
        }

        public static async Task Indo_Movies (string api) {
            HttpClient client = new HttpClient ();
            HttpRequestMessage req = new HttpRequestMessage (HttpMethod.Get,api);
            HttpResponseMessage res = await client.SendAsync (req);
            var json = await res.Content.ReadAsStringAsync ();
            var list = JsonConvert.DeserializeObject<Objek> (json);
            var Indo_Movies = from a in list.results select a.original_title;
            int numb = 1;
            foreach (var item in Indo_Movies) {
                Console.WriteLine (numb +". " + item);
                numb++;
            }
        }

        public static async Task KeanuRev (string api) {
            HttpClient client = new HttpClient ();
            HttpRequestMessage req = new HttpRequestMessage (HttpMethod.Get, api);
            HttpResponseMessage res = await client.SendAsync (req);
            var json = await res.Content.ReadAsStringAsync ();
            var list = JsonConvert.DeserializeObject<Objek> (json);

            var movies = from a in list.results select a.original_title;
            int numb =1;
            foreach (var item in movies) {
                Console.WriteLine (numb + ". " + item);
                numb++;
            }
        }
        public static async Task RobertTom (string api) {
            HttpClient client = new HttpClient ();
            HttpRequestMessage req = new HttpRequestMessage (HttpMethod.Get, api);
            HttpResponseMessage res = await client.SendAsync (req);
            var json = await res.Content.ReadAsStringAsync ();
            var list = JsonConvert.DeserializeObject<Objek> (json);

            var movies = from a in list.results select a.original_title;
            int numb = 1;
            foreach (var item in movies) {
                Console.WriteLine (numb +". " + item);
                numb++;
            }
        }
        public static async Task Movies2016 (string api) {
            HttpClient client = new HttpClient ();
            HttpRequestMessage req = new HttpRequestMessage (HttpMethod.Get, api);
            HttpResponseMessage res = await client.SendAsync (req);
            var getJson = await res.Content.ReadAsStringAsync ();
            var list = JsonConvert.DeserializeObject<Objek> (getJson);

            var movies = from a in list.results select a.title;
            int numb =1;
            foreach (var item in movies) {
                Console.WriteLine (numb + ". " + item);
                numb++;
            }
        }
    }

    public class Result {
        public string poster_path { get; set; }
        public bool adult { get; set; }
        public string overview { get; set; }
        public string release_date { get; set; }
        public string[] genre_ids { get; set; }
        public int id { get; set; }
        public string original_title { get; set; }
        public string original_language { get; set; }
        public string title { get; set; }
        public string backdrop_path { get; set; }
        public double popularity { get; set; }
        public int vote_count { get; set; }
        public bool video { get; set; }
        public double vote_average { get; set; }
    }

    public class Objek {
        public int page { get; set; }
        public int total_results { get; set; }
        public int total_pages { get; set; }
        public List<Result> results { get; set; }
    }
}