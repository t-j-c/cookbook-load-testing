using CookBook.Test.Utilities;
using Xunit;

namespace CookBook.Test.Tests
{
    public class CookbookApiLoadTests
    {
        [Fact]
        public void AverageResponseTime_100Requests_2MaxParallel()
        {
            var results = HttpHelper.ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 2);
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 4);
        }

        [Fact]
        public void AverageResponseTime_100Requests_4MaxParallel()
        {
            var results = HttpHelper.ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 4);
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 6);
        }

        [Fact]
        public void AverageResponseTime_100Requests_6MaxParallel()
        {
            var results = HttpHelper.ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 6);
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 8);
        }
    }
}
