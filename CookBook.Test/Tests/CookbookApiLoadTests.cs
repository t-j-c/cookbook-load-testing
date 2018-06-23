using CookBook.Test.Utilities;
using Xunit;

namespace CookBook.Test.Tests
{
    public class CookbookApiLoadTests
    {
        private const string _cookbookNamePrefix = "LoadTestCookbook";

        [Fact]
        public void AverageResponseTime_100Requests_2MaxParallel()
        {
            // Arrange
            CookbookTestDataSetup.DeleteCookbooks();
            CookbookTestDataSetup.CreateCookbooks(_cookbookNamePrefix);

            // Act
            var results = HttpHelper.ExecuteParallelRequests($"{CookbookTestDataSetup.CookbookApiUrl}/{_cookbookNamePrefix}_1", 100, 2);

            // Assert
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 6);
        }

        [Fact]
        public void AverageResponseTime_100Requests_4MaxParallel()
        {
            // Arrange
            CookbookTestDataSetup.DeleteCookbooks();
            CookbookTestDataSetup.CreateCookbooks(_cookbookNamePrefix);

            // Act
            var results = HttpHelper.ExecuteParallelRequests($"{CookbookTestDataSetup.CookbookApiUrl}/{_cookbookNamePrefix}_1", 100, 4);

            // Assert
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 6);

            CookbookTestDataSetup.DeleteCookbooks();
        }

        [Fact]
        public void AverageResponseTime_100Requests_6MaxParallel()
        {
            // Arrange
            CookbookTestDataSetup.DeleteCookbooks();
            CookbookTestDataSetup.CreateCookbooks(_cookbookNamePrefix);

            // Act
            var results = HttpHelper.ExecuteParallelRequests($"{CookbookTestDataSetup.CookbookApiUrl}/{_cookbookNamePrefix}_1", 100, 6);

            // Assert
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 8);

            CookbookTestDataSetup.DeleteCookbooks();
        }
    }
}
