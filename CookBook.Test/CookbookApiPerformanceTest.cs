using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xunit;

namespace CookBook.Test
{
    public class CookbookApiPerformanceTest
    {
        [Fact]
        public void AverageResponseTime_100Requests()
        {
            var allResponseTimes = new List<(DateTime Start, DateTime End)>();
            
            for (var i = 0; i < 100; i++)
            {
                using (var client = new HttpClient())
                {
                    var start = DateTime.Now;
                    var response = client.GetAsync("http://localhost:5000/api/cookbook").Result;
                    var end = DateTime.Now;

                    allResponseTimes.Add((start, end));
                }
            }

            var expected = 8;
            var actual = (int)allResponseTimes.Select(r => (r.End - r.Start).TotalMilliseconds).Average();
            Assert.True(actual <= expected, $"Expected average response time of less than or equal to {expected} ms but was {actual} ms.");
        }
    }
}