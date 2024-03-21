using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearningManagementSystem.API.Entities;
using LearningManagementSystem.API.Context;
using Microsoft.EntityFrameworkCore;

namespace LearningManagementSystem.API.Repositories
{
    public class AdminRepository<T> : IAdminRepository<T> where T : class
    {
        private DBContext _context = null;
        private DbSet<T> table = null;

        public AdminRepository(DBContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }
        public void Add(T obj)
        {
            table.Add(obj);
            _context.SaveChanges();
        }
        public void Delete(int id)
        {
            T obj = table.Find(id);
            table.Remove(obj);
            _context.SaveChanges();
        }
        public void Update(T obj)
        {
            table.Update(obj);
            _context.SaveChanges();
        }
        public T GetDetails(int id)
        {
            return table.Find(id);
        }

        //Verifies the existing User-emails during User Registration.
        public bool CheckUserExists(string email)
        {
            User user = _context.Users.Where(i => i.Email.Equals(email)).Select(i => i).FirstOrDefault();
           if (user == null)
            {
                return true;
            }
            else
            {
                return false;
            }

           
        }
    }

}
