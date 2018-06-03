using CookBook.Test.Utilities;
using Xunit;

namespace CookBook.Test.Tests
{
    public class CookbookApiPerformanceTest
    {
        [Fact]
        public void AverageResponseTime_100Requests_1MaxParallel()
        {
            var results = HttpHelper.ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 1);
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 4);
        }
    }
}