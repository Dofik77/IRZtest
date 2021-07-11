using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace APIReq
{

    //Task 2 - Get the request and write json file 
    class Program
    {
        static void Main(string[] args)
        {

            using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })) //set decompression type .Gzip 
            {
                string url = "https://api.stackexchange.com/2.3/search?pagesize=20&fromdate=1577836800&todate=1625788800&order=asc&sort=activity&tagged=python&site=stackoverflow";
                
                HttpResponseMessage response = client.GetAsync(url).Result; // Send Get request to the uri 

                response.EnsureSuccessStatusCode(); //Throws an exception if the IsSuccessStatusCode property for the HTTP response is false.

                string result = response.Content.ReadAsStringAsync().Result; //Serialize the HTTP content to a string

                Console.WriteLine(result); //Console output

                string path = @"C:\SomeDir"; 
                DirectoryInfo dirInfo = new DirectoryInfo(path);
                if (!dirInfo.Exists)
                {
                    dirInfo.Create();
                }

                using (FileStream fstream = new FileStream(@"C:\SomeDir\note.json", FileMode.OpenOrCreate)) // write json file 
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(result);
                    fstream.Write(array, 0, array.Length);
                    Console.WriteLine("Recording completed");
                }

                var file = new Process();
                file.StartInfo = new ProcessStartInfo(path) { UseShellExecute = true };
                file.Start(); //launch directory of Json file 

            }

            
        }
    }
}
