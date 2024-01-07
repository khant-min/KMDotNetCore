using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using KMDotNetCore.ConsoleApp.Models;
using Newtonsoft.Json;

namespace KMDotNetCore.ConsoleApp.HttpClientExamples
{
    internal class HttpClientExample
    {
        public async Task Run()
        {
            await Read();
        }

        public async Task Read()
        {
            HttpClient client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5192/api/blog");
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                var model = JsonConvert.DeserializeObject<BlogListResponseModel>(jsonStr);
                foreach (var item in model!.Data)
                {
                    Console.WriteLine(item.Blog_Id);
                    Console.WriteLine(item.Blog_Title);
                    Console.WriteLine(item.Blog_Author);
                    Console.WriteLine(item.Blog_Content);
                }
            }
        }

        public async Task Edit()
        {

        }

        public async Task Create()
        {

        }
    }
}
