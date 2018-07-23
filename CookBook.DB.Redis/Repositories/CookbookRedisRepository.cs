using System.Collections.Generic;
using CookBook.Model.Entities;
using CookBook.Model.Repositories;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;

namespace CookBook.DB.Redis.Repositories
{
    public class CookbookRedisRepository : ICookbookRepository
    {
        private readonly ICookbookRepository _parentRepository;
        private readonly IDatabase _cache;
        public CookbookRedisRepository(ICookbookRepository parentRepository, IConfiguration configuration)
        {
            _parentRepository = parentRepository;
            _cache = ConnectionMultiplexer.Connect(configuration["CookBook.Redis:Connection"]).GetDatabase();
        }

        public void Add(string name)
        {
            _parentRepository.Add(name);
        }

        public int Count()
        {
            return _parentRepository.Count();
        }

        public void Delete()
        {
            _cache.Execute("FLUSHALL");
            _parentRepository.Delete();
        }

        public ICollection<Cookbook> Get()
        {
            return _parentRepository.Get();
        }

        public Cookbook Get(string name)
        {
            var cacheResult = _cache.StringGet(name);
            if (!cacheResult.IsNullOrEmpty) return JsonConvert.DeserializeObject<Cookbook>(cacheResult);

            var result = _parentRepository.Get(name);
            _cache.StringSet(name, JsonConvert.SerializeObject(result));
            return result;
        }
    }
}
