using System;
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
            var expected = 2;
            var actual = ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 1);
            Assert.InRange(actual, 0, expected);
        }

        [Fact]
        public void AverageResponseTime_100Requests_2MaxParallel()
        {
            var expected = 2;
            var actual = ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 2);
            Assert.InRange(actual, 0, expected);
        }

        [Fact]
        public void AverageResponseTime_100Requests_4MaxParallel()
        {
            var expected = 4;
            var actual = ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 4);
            Assert.InRange(actual, 0, expected);
        }

        private double ExecuteParallelRequests(string url, int totalNumberOfRequests, int maxDegreeOfParallelism = -1)
        {
            var results = new double[totalNumberOfRequests];
            var options = new ParallelOptions
            {
                MaxDegreeOfParallelism = maxDegreeOfParallelism
            };

            using (var client = new HttpClient())
            {
                var loopResult = Parallel.For(0, totalNumberOfRequests, options, (i) =>
                {
                        var startTime = DateTime.Now;
                        var response = client.GetAsync(url).Result;
                        var endTime = DateTime.Now;

                        results[i] = ((endTime - startTime).TotalMilliseconds);
                });
            }

            return results.Average();
        }
    }
}