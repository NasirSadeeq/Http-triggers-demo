using HttpTrigger.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace HttpTrigger.Db_Context_class
{
    public class EmployeeDbContext:DbContext
    {
        private readonly DbContextOptions<EmployeeDbContext> options;

        public EmployeeDbContext(DbContextOptions<EmployeeDbContext>options):base(options)
        {
            this.options = options;
        }
        public DbSet<Employee> Employees { get; set; }
    }
}
