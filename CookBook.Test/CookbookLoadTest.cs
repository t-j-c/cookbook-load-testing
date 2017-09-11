using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Xunit;

namespace CookBook.Test
{
    public class CookbookLoadTest
    {
        [Fact]
        public void Test1()
        {
            var client = new HttpClient();

            var startTime = DateTime.Now;
            var result = client.GetStringAsync("http://localhost:5000/api/cookbook").Result;
            var endTime = DateTime.Now;
            
            var start = startTime.ToString("HH:mm:ss.fff");
            var end = endTime.ToString("HH:mm:ss.fff");
            var span = endTime - startTime;
            Assert.Null($"Start -> {start}, End -> {end}, Total Ms -> {span.TotalMilliseconds}");
        }
    }
}