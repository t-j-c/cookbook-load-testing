using System.Collections.Generic;
using CookBook.Model.Entities;
using CookBook.Model.Repositories;
using Microsoft.Azure.Documents.Client;
using System;
using Microsoft.Azure.Documents;
using System.Linq;

namespace CookBook.DB.CosmosDB.Repositories
{
    public class CookbookCosmosDbRepository : ICookbookRepository
    {
        private readonly DocumentClient _client;

        private readonly Uri _databaseUri;
        private readonly Uri _collectionUri;

        public CookbookCosmosDbRepository()
        {
            _client = new DocumentClient(
                new Uri("https://localhost:8081"), 
                "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");

            const string databaseName = "CookBookDB";
            _databaseUri = UriFactory.CreateDatabaseUri(databaseName);
            const string collectionName = "CookBookCollection";
            _collectionUri = UriFactory.CreateDocumentCollectionUri(databaseName, collectionName);
            
            _client.CreateDatabaseIfNotExistsAsync(new Database { Id = databaseName }).GetAwaiter().GetResult();
            _client.CreateDocumentCollectionIfNotExistsAsync(_databaseUri, 
                new DocumentCollection { Id = collectionName }).GetAwaiter().GetResult();
        }

        public void Add(string name)
        {
            var newCookbook = new Cookbook
            {
                Name = name
            };

            _client.CreateDocumentAsync(_collectionUri, newCookbook).GetAwaiter().GetResult();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }

        public void Delete()
        {
            _client.DeleteDocumentCollectionAsync(_collectionUri).GetAwaiter().GetResult();
            _client.CreateDocumentCollectionIfNotExistsAsync(_databaseUri,
                new DocumentCollection { Id = "CookBookCollection" }).GetAwaiter().GetResult();
        }

        public ICollection<Cookbook> Get()
        {
            var results = _client.CreateDocumentQuery<Cookbook>(_collectionUri);
            return results.ToList();
        }

        public Cookbook Get(string name)
        {
            var results = _client.CreateDocumentQuery<Cookbook>(_collectionUri);
            return results.Where(c => c.Name == name).ToList().SingleOrDefault();
        }
    }
}
