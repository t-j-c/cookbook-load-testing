using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;

namespace CookBook.LoadTest.AzFunc
{
    public static class ExecuteRequest
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        [FunctionName("ExecuteRequest")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            string url = req.Query["url"];

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            dynamic data = JsonConvert.DeserializeObject(requestBody);
            url = url ?? data?.url;

            if (url == null)
            {
                return new BadRequestObjectResult("Please pass a url on the query string or in the request body");
            }

            var stopwatch = new Stopwatch();
            stopwatch.Start();
            await _httpClient.GetAsync(url);
            stopwatch.Start();

            return new OkObjectResult($"GET request to {url} took {stopwatch.ElapsedMilliseconds} ms.");
        }
    }
}
