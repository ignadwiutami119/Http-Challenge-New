using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace _3 {
    class Program {
        public static async Task Main (string[] args) {
            HttpClient client = new HttpClient ();
            HttpRequestMessage reqPost = new HttpRequestMessage (HttpMethod.Get, "https://jsonplaceholder.typicode.com/posts");
            HttpResponseMessage resPost = await client.SendAsync (reqPost);
            var getJsonPost = await resPost.Content.ReadAsStringAsync ();

            HttpRequestMessage reqUser = new HttpRequestMessage (HttpMethod.Get, "https://jsonplaceholder.typicode.com/users");
            HttpResponseMessage resUser = await client.SendAsync (reqUser);
            var getJsonUser = await resUser.Content.ReadAsStringAsync ();

            var resultPost = JsonConvert.DeserializeObject<List<Post>> (getJsonPost);
            var resultUser = JsonConvert.DeserializeObject<List<User>> (getJsonUser);

            List<Combine> result = new List<Combine> ();
            string Combine () {
                foreach (var itemPost in resultPost) {
                    foreach (var itemUser in resultUser) {
                        if (itemPost.UserId == itemUser.Id) {
                            Combine save = new Combine ();
                            save.UserId = itemPost.UserId;
                            save.Id = itemPost.Id;
                            save.Title = itemPost.Title;
                            save.Body = itemPost.Body;
                            save.user = itemUser;

                            result.Add (save);
                        }
                    }
                }
                string FinalResult = JsonConvert.SerializeObject (result, Formatting.Indented);
                return FinalResult;
            }

            Console.WriteLine (Combine ());
        }
    }

    class Combine {
        [JsonProperty ("userId")]
        public int UserId { get; set; }

        [JsonProperty ("id")]
        public int Id { get; set; }

        [JsonProperty ("title")]
        public string Title { get; set; }

        [JsonProperty ("body")]
        public string Body { get; set; }

        [JsonProperty ("user")]
        public User user { get; set; }
    }

    class Post {
        [JsonProperty ("userId")]
        public int UserId { get; set; }

        [JsonProperty ("id")]
        public int Id { get; set; }

        [JsonProperty ("title")]
        public string Title { get; set; }

        [JsonProperty ("body")]
        public string Body { get; set; }
    }

    class User {
        [JsonProperty ("id")]
        public int Id { get; set; }

        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("username")]
        public string UserName { get; set; }

        [JsonProperty ("email")]
        public string Email { get; set; }

        [JsonProperty ("address")]
        public Adress Adress { get; set; }

        [JsonProperty ("phone")]
        public string Phone { get; set; }

        [JsonProperty ("website")]
        public string Website { get; set; }

        [JsonProperty ("company")]
        public Company Company { get; set; }
    }

    class Adress {
        [JsonProperty ("street")]
        public string Street { get; set; }

        [JsonProperty ("suite")]
        public string Suite { get; set; }

        [JsonProperty ("city")]
        public string City { get; set; }

        [JsonProperty ("zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty ("geo")]
        public Geo Geo { get; set; }
    }

    class Geo {
        [JsonProperty ("lat")]
        public string Lat { get; set; }

        [JsonProperty ("lng")]
        public string Lng { get; set; }
    }

    class Company {
        [JsonProperty ("name")]
        public string Name { get; set; }

        [JsonProperty ("catchPhrase")]
        public string CatchPhrase { get; set; }

        [JsonProperty ("bs")]
        public string Bs { get; set; }
    }
}