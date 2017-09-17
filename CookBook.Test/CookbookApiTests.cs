// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading;
// using System.Threading.Tasks;
// using Xunit;
// using Xunit.Abstractions;

// namespace CookBook.Test
// {
//     public class CookbookApiTests
//     {
//         private readonly ITestOutputHelper _outputHelper;
//         public CookbookApiTests(ITestOutputHelper outputHelper)
//         {
//             _outputHelper = outputHelper;
//         }

//         [Fact]
//         public void AverageTime_1Request()
//         {
//             var results = ExecuteParallelRequests("http://localhost:5000/api/cookbook", 1);
//             AssertAverageTime(1, results);
//         }

//         [Fact]
//         public void AverageTime_10Requests()
//         {
//             var results = ExecuteParallelRequests("http://localhost:5000/api/cookbook", 10, 10);
//             AssertAverageTime(1, results);
//         }

//         [Fact]
//         public void AverageTime_100Requests()
//         {
//             var results = ExecuteParallelRequests("http://localhost:5000/api/cookbook", 100, 100);
//             AssertAverageTime(1, results);
//         }

//         private void AssertAverageTime(int expected, List<RequestResult> results)
//         {
//             var actual = (int)results.Select(r => (r.End - r.Start).TotalMilliseconds).Average();
//             Assert.True(actual < expected, $"Expected less than {expected} millisecond(s) but was {actual}.");
//         }

//         private List<RequestResult> ExecuteParallelRequests(string url, 
//             int totalNumberOfRequests, int maxDegreeOfParallelism = -1)
//         {
//             var results = new List<RequestResult>();

//             var options = new ParallelOptions
//             {
//                 MaxDegreeOfParallelism = maxDegreeOfParallelism
//             };
//             var loopResult = Parallel.For(0, totalNumberOfRequests, (i) =>
//             {
//                 using (var client = new HttpClient())
//                 {
//                     var startTime = DateTime.Now;
//                     var response = client.GetAsync(url).Result;
//                     var endTime = DateTime.Now;

//                     results.Add(new RequestResult(Thread.CurrentThread.ManagedThreadId, startTime, endTime));
//                 }
//             });

//             // Write Results to output
//             _outputHelper.WriteLine("Request ID\t| Start Time\t| End Time\t| Total Ms");
//             foreach (var result in results.OrderBy(r => r.RequestId))
//             {
//                 // var message = $"{result.RequestId}\t\t| {result.Start.ToString("HH:mm:ss.fff")}\t| {result.End.ToString("HH:mm:ss.fff")}\t| {(int)(result.End - result.Start).TotalMilliseconds}";
//                 var message = $"{result.RequestId}\t\t| {result.Start.ToString("HH:mm:ss.fff")}\t| {result.End.ToString("HH:mm:ss.fff")}\t| {(int)(result.End - result.Start).TotalMilliseconds}";
//                 _outputHelper.WriteLine(message);
//             }

//             return results;
//         }

//         public class RequestResult
//         {
//             public RequestResult(int requestId, DateTime start, DateTime end)
//             {
//                 RequestId = requestId;
//                 Start = start;
//                 End = end;
//             }

//             public int RequestId { get; }
//             public DateTime Start { get; }
//             public DateTime End { get; }
//         }
//     }
// }