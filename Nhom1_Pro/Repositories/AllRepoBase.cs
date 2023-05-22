using Microsoft.EntityFrameworkCore;

namespace AppData.Repositories
{
    public class AllRepoBase<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> dbset;
    }
}