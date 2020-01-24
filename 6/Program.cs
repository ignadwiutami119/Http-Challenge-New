using System;
using System.Threading.Tasks;
using HtmlAgilityPack;
using PuppeteerSharp;

namespace _6 {
    class Program {
        static async Task Main (string[] args) {
            var style = new LaunchOptions { Headless = true };
            await new BrowserFetcher ().DownloadAsync (BrowserFetcher.DefaultRevision);
            Console.WriteLine ("\nGet information from CGV...");

            using (var browser = await Puppeteer.LaunchAsync (style))
            using (var page = await browser.NewPageAsync ()) {
                await page.GoToAsync ("https://www.cgv.id/en/movies/now_playing");
                var jsSelectAllAnchors = await page.QuerySelectorAllHandleAsync (".movie-list-body > ul >li > a").EvaluateFunctionAsync<string[]> ("elements => elements.map(a => a.href)");
                Console.WriteLine ("\n=========Now Playing=========");
                HtmlWeb web = new HtmlWeb ();
                for (int i = 0; i < jsSelectAllAnchors.Length; i++) {
                    var htmlDoc = web.Load (jsSelectAllAnchors[i]);

                    Console.WriteLine ("\n\n========================================================================================================");

                    var Title = htmlDoc.DocumentNode.SelectNodes ("//div[@class='movie-info-title']");
                    foreach (var node in Title) {
                        Console.WriteLine ("\nTITLE :\n" + node.InnerText.Trim () + "\n");
                    }

                    var Starring = htmlDoc.DocumentNode.SelectNodes ("//div[@class='movie-add-info left']/ul /li");
                    foreach (var node in Starring) {
                        Console.WriteLine ("> " + node.InnerText.Trim ());
                    }

                    var Trailer = htmlDoc.DocumentNode.SelectNodes ("//div[@class='trailer-btn-wrapper']/img");
                    foreach (var node in Trailer) {
                        var getLink = node.GetAttributeValue ("onclick", string.Empty);
                        Console.WriteLine ("\n> TRAILER LINK : " + getLink.Remove(0,11));
                    }

                    var Synopsis = htmlDoc.DocumentNode.SelectNodes ("//div[@class='movie-synopsis right']");
                    foreach (var node in Synopsis) {
                        Console.WriteLine ("\n> SYNOPIS : " + node.InnerText.Trim ());
                    }
                }
            }
        }
    }
}