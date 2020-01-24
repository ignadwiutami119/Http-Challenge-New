using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using HtmlAgilityPack;
using ScrapySharp.Extensions;

namespace _5 {
    class Program {
        public static async Task Main (string[] args) {
            HtmlWeb web = new HtmlWeb ();
            var htmlDoc = web.Load ("https://www.kompas.com/");

            Console.WriteLine("\n========================HEADLINE KOMPAS=========================");
            var result = Headline();
            foreach(var item in result){
                Console.WriteLine("\nHeadline : \n" + item[0] + "\nLink : \n" + item[1]);
                Console.WriteLine("\n========================================================\n");
            }

            List<List<string>> Headline () {
                List<List<string>> List = new List<List<string>> ();
                var get = htmlDoc.DocumentNode.SelectNodes("//a[@class='headline__thumb__link']");
                foreach (var item in get)
                {
                    var getLink = item.GetAttributeValue("href",string.Empty);
                    var getTitle = item.InnerText;
                    List.Add(new List<string>{getTitle, getLink});
                }
                return List;
            }
        }
    }
}