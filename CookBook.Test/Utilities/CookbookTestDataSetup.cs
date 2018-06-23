using CookBook.Contracts.Messages;
using System.Net.Http;

namespace CookBook.Test.Utilities
{
    public static class CookbookTestDataSetup
    {
        public const string CookbookApiUrl = "http://localhost:5000/api/cookbook";

        public static void CreateCookbooks(string namePrefix, int amount = 100)
        {
            for (var i = 1; i <= amount; i++)
            {
                var command = new AddCookbookCommand { Name = $"{namePrefix}_{i}" };
                HttpHelper.ExecuteRequest(HttpMethod.Post, CookbookApiUrl, command);
            }
        }

        public static void DeleteCookbooks()
        {
            HttpHelper.ExecuteRequest(HttpMethod.Delete, $"{CookbookApiUrl}/all");
        }
    }
}
