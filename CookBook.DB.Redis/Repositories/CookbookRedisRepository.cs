using System.Collections.Generic;
using CookBook.Model.Entities;
using CookBook.Model.Repositories;

namespace CookBook.DB.Redis.Repositories
{
    public class CookbookRedisRepository : ICookbookRepository
    {
        private readonly ICookbookRepository _parentRepository;
        public CookbookRedisRepository(ICookbookRepository parentRepository)
        {
            _parentRepository = parentRepository;
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
            _parentRepository.Delete();
        }

        public ICollection<Cookbook> Get()
        {
            return _parentRepository.Get();
        }

        public Cookbook Get(string name)
        {
            return _parentRepository.Get(name);
        }
    }
}
