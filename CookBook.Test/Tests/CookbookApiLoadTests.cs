using CookBook.Test.Utilities;
using System.Linq;
using Xunit;
using Xunit.Abstractions;

namespace CookBook.Test.Tests
{
    public class CookbookApiLoadTests
    {
        private const string _cookbookNamePrefix = "LoadTestCookbook";
        private readonly ITestOutputHelper _outputHelper;

        public CookbookApiLoadTests(ITestOutputHelper outputHelper)
        {
            _outputHelper = outputHelper;
        }

        [Fact]
        public void AverageResponseTime_100Requests_2MaxParallel()
        {
            // Arrange
            CookbookTestDataSetup.CreateCookbooks(_cookbookNamePrefix);

            // Act
            var results = HttpHelper.ExecuteParallelRequests($"{CookbookTestDataSetup.CookbookApiUrl}/{_cookbookNamePrefix}_1", 100, 2);
            OutputResults(results);

            // Assert
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 1);
        }

        [Fact]
        public void AverageResponseTime_100Requests_4MaxParallel()
        {
            // Arrange
            CookbookTestDataSetup.CreateCookbooks(_cookbookNamePrefix);

            // Act
            var results = HttpHelper.ExecuteParallelRequests($"{CookbookTestDataSetup.CookbookApiUrl}/{_cookbookNamePrefix}_1", 100, 4);
            OutputResults(results);

            // Assert
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 1);
        }

        [Fact]
        public void AverageResponseTime_100Requests_6MaxParallel()
        {
            // Arrange
            CookbookTestDataSetup.CreateCookbooks(_cookbookNamePrefix);

            // Act
            var results = HttpHelper.ExecuteParallelRequests($"{CookbookTestDataSetup.CookbookApiUrl}/{_cookbookNamePrefix}_1", 100, 6);
            OutputResults(results);

            // Assert
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 1);
        }

        [Fact]
        public void AverageResponseTime_100Requests_20MaxParallel()
        {
            // Arrange
            CookbookTestDataSetup.CreateCookbooks(_cookbookNamePrefix);

            // Act
            var results = HttpHelper.ExecuteParallelRequests($"{CookbookTestDataSetup.CookbookApiUrl}/{_cookbookNamePrefix}_1", 100, 20);
            OutputResults(results);

            // Assert
            Assert.InRange(results.AverageTimeInMilliseconds, 0, 1);
        }

        private void OutputResults(HttpResults results)
        {
            _outputHelper.WriteLine($"Http Result times:");
            int count = 1;
            foreach (var result in results.Results.OrderBy(r => r.Start))
            {
                _outputHelper.WriteLine($"{count}\t: {result.Start.ToString("HH:mm:ss:fff")} - {result.End.ToString("HH:mm:ss:fff")}");
                count++;
            }
        }
    }
}
