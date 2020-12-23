using Microsoft.EntityFrameworkCore;

namespace ZwajApp.API.Models
{
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options) : base(options) { }


        public DbSet<Value> values { get; set; }



    }

}