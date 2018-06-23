using CookBook.Test.Utilities;
using Xunit;

namespace CookBook.Test.Tests
{
    public class CookbookApiPerformanceTest
    {
        private const string _cookbookNamePrefix = "PerformanceTestCookbook";

        [Fact]
        public void AverageResponseTime_100Requests_1MaxParallel()
        {
            // Arrange
            CookbookTestDataSetup.DeleteCookbooks();
            CookbookTestDataSetup.CreateCookbooks(_cookbookNamePrefix);

            // Act
            var results = HttpHelper.ExecuteParallelRequests($"{CookbookTestDataSetup.CookbookApiUrl}/{_cookbookNamePrefix}_1", 100, 1);

            // Assert
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 6);
        }
    }
}