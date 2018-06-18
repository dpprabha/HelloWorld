using HelloWorld.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace HelloWorld.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {  
            HttpClient cons = new HttpClient();
            cons.BaseAddress = new Uri("http://localhost:49843/");
            cons.DefaultRequestHeaders.Accept.Clear();
            cons.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            GetData(cons);
        }

        static void GetData(HttpClient cons)
        {
            HttpResponseMessage response = cons.GetAsync("api/Data").Result;
            response.EnsureSuccessStatusCode();
            if (response.IsSuccessStatusCode)
            {
                string text = response.Content.ToString();
                Console.WriteLine(Environment.NewLine + text);
                Console.ReadLine();
            }
        }
    }
}