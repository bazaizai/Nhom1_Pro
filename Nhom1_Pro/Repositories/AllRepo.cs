using AppData.IRepositories;
using Microsoft.EntityFrameworkCore;
using Nhom1_Pro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppData.Repositories
{
    public class AllRepo<T> : IAllRepo<T> where T : class
    {
        private  DBContextModel context;

        private  DbSet<T> dbset;

        public AllRepo()
        {
        }

        public AllRepo(DBContextModel context, DbSet<T> dbset)
        {
            this.context = context;
            this.dbset = dbset;
        }

        public DBContextModel Context => context;

        public DbSet<T> Dbset => dbset;
        public bool AddItem(T item)
        {
            try
            {
                dbset.Add(item);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

               return false;
            }
        }

        public bool EditItem(T item)
        {
            try
            {
                
                dbset.Update(item);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public IEnumerable<T> GetAll()
        {
           return dbset.ToList();
        }

        public bool RemoveItem(T item)
        {
            try
            {
                
                dbset.Remove(item);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
