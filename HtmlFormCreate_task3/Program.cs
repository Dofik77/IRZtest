using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using static HtmlFormCreate.JsonInfoClass;
using System.Diagnostics;

namespace HtmlFormCreate
{
    class Program
    {
        //Task3 - From Json file Task2 generation HTML table
        static void Main(string[] args)
        {
            string writePath = @"D:\table.html"; // directory for HTML file ( global for launch file ) 

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })) //set decompression type .Gzip 
            {
                int pageSize = 30; // Count of responses

                string uri = "https://api.stackexchange.com/2.3/search? + pageSize + &fromdate=1577836800&todate=1625788800&order=asc&sort=activity&tagged=python&site=stackoverflow"; // Adress with correct pagesize

                HttpResponseMessage response = client.GetAsync(uri).Result; // Send Get request to the uri 

                response.EnsureSuccessStatusCode(); //Throws an exception if the IsSuccessStatusCode property for the HTTP response is false.

                string result = response.Content.ReadAsStringAsync().Result; //Serialize the HTTP content to a string

                Rootobject textTest = JsonConvert.DeserializeObject<Rootobject>(result); // Deserialize json file by class "Rootobject"

                using (StreamWriter sw = new StreamWriter(writePath, false, System.Text.Encoding.Default)) //defines the area outside of which objects are delted
                {
                    System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc); // for convert unix time 

                    //create HTML table in file

                    sw.WriteLine("<table>");
                    sw.WriteLine("<tr>");
                    sw.WriteLine("<th>Creation Date</th>");
                    sw.WriteLine("<th>Title</th>");
                    sw.WriteLine("<th>Author</th>");
                    sw.WriteLine("<th>Answered?</th>");
                    sw.WriteLine("<th>Link</th>");
                    sw.WriteLine("</tr>");

                    for (int i = 0; i < pageSize; i++)
                    {
                        sw.WriteLine("<tr>");
                        sw.WriteLine("<td>" + (dtDateTime.AddSeconds(textTest.items[i].creation_date).ToLocalTime()) + "</td>");
                        sw.WriteLine("<td>" + (textTest.items[i].title.ToString()) + "</td>");
                        sw.WriteLine("<td>" + (textTest.items[i].owner.display_name.ToString()) + "</td>");
                        sw.WriteLine("<td>" + (textTest.items[i].is_answered.ToString()) + "</td>");
                        sw.WriteLine("<td>" + (textTest.items[i].link.ToString()) + "</td>");
                        sw.WriteLine("</tr>");
                    }

                    sw.WriteLine("</table>");

                    Console.WriteLine("Recording completed");
                }

            }

            var file = new Process();
            file.StartInfo = new ProcessStartInfo(writePath) { UseShellExecute = true }; 
            file.Start(); //launch HTML file 
        }
    }
}
