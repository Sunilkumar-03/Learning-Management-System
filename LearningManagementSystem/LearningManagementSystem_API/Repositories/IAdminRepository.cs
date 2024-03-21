using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearningManagementSystem.API.Repositories
{
    public interface IAdminRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        void Add(T obj);
        void Delete(int id);
        void Update(T obj);
        T GetDetails(int id);
        bool CheckUserExists(string email);
    }
}
