using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RestSharp;
using RestSharp.Authenticators;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AdventureConsole
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var client = new RestClient("http://partners.api.skyscanner.net/apiservices/autosuggest/v1.0/US/USD/en-US");
            //1
            var request = new RestRequest("?query=paris&apiKey=br263217536383875054898079171468", Method.GET);

            //2
            var response = new RestResponse();
            //3a
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
            //4
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            Console.WriteLine(jsonResponse["Places"]);

            Console.ReadLine();
        }
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}
