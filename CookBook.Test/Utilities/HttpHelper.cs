using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CookBook.Test.Utilities
{
    public static class HttpHelper
    {
        public static HttpResults ExecuteParallelRequests(string url, int totalNumberOfRequests, int maxDegreeOfParallelism = -1)
        {
            var results = new ConcurrentBag<HttpResult>();
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };

            using (var client = new HttpClient())
            {
                var loopResult = Parallel.For(0, totalNumberOfRequests, options, (i) =>
                {
                    var startTime = DateTime.Now;
                    var response = client.GetAsync(url).GetAwaiter().GetResult();
                    var endTime = DateTime.Now;
                   
                    results.Add(new HttpResult(response, startTime, endTime));
                });
            }

            return new HttpResults(results.ToList());
        }
    }

    public class HttpResults
    {
        public HttpResults(List<HttpResult> results)
        {
            Results = results;
        }

        public List<HttpResult> Results { get; }
        public double AverageTimeInMilliseconds => Results.Average(r => r.TotalTime.TotalMilliseconds);
    }

    public class HttpResult
    {
        public HttpResult(HttpResponseMessage response, DateTime start, DateTime end)
        {
            Response = response;
            Start = start;
            End = end;
        }

        public HttpResponseMessage Response { get; }
        public DateTime Start { get; }
        public DateTime End { get; }
        public TimeSpan TotalTime => End - Start;
    }
}
