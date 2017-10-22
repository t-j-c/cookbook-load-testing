using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace CookBook.Test
{
    public class CookbookApiPerformanceTest
    {
        [Fact]
        public void AverageResponseTime_100Requests_1MaxParallel()
        {
            var expected = 8;
            var actual = ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 1);
            Assert.True(actual <= expected, $"Expected average response time of less than or equal to {expected} ms but was {actual} ms.");
        }

        [Fact]
        public void AverageResponseTime_100Requests_2MaxParallel()
        {
            var expected = 8;
            var actual = ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 2);
            Assert.True(actual <= expected, $"Expected average response time of less than or equal to {expected} ms but was {actual} ms.");
        }

        private double ExecuteParallelRequests(string url, int totalNumberOfRequests, int maxDegreeOfParallelism = -1)
        {
            var results = new double[totalNumberOfRequests];
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };
            var loopResult = Parallel.For(0, totalNumberOfRequests, options, (i) =>
            {
                using (var client = new HttpClient())
                {
                    var startTime = DateTime.Now;
                    var response = client.GetAsync(url).Result;
                    var endTime = DateTime.Now;

                    results[i] = ((endTime - startTime).TotalMilliseconds);
                }
            });

            return results.Average();
        }
    }
}