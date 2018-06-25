using System.Collections.Generic;
using CookBook.Model.Entities;

namespace CookBook.Model.Repositories
{
    public interface ICookbookRepository
    {
        int Count();
        ICollection<Cookbook> Get();
        Cookbook Get(string name);
        void Add(string name);
        void Delete();
    }
}