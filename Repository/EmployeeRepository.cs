using HttpTrigger.Db_Context_class;
using HttpTrigger.Interfaces;
using HttpTrigger.Models;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace HttpTrigger.Repository
{
    public class EmployeeRepository : Iemployee
    {
        private readonly EmployeeDbContext employeeDb;

        public EmployeeRepository(EmployeeDbContext employeeDb)
        {
            this.employeeDb = employeeDb;
        }
        public async Task<Employee> AddUser(Employee employee)
        {
            var userDetailsResult = new Employee()
            {
                Id = employee.Id,
                Name = employee.Name,
            Email = employee.Email

            };
            var result = await employeeDb.Employees.AddAsync(userDetailsResult);
            employeeDb.SaveChanges();
           // await employeeDb.SaveChangesAsync();

            return result.Entity;

        }

        public async Task DeleteUser(string Email)
        {
            var result = await employeeDb.Employees.FirstOrDefaultAsync(x => x.Email == Email);
           // var result = await employeeDb.Employees.FirstOrDefault(e => e.Email.ToLower() == Email.ToLower());
            if (result != null)
            {
     
                employeeDb.Employees.Remove(result);
                await employeeDb.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Employee>> GetAllUsers()
        {
            return await employeeDb.Employees.ToListAsync();
        }

        public async Task<Employee> GetUser(string Email)
        {
            return await employeeDb.Employees.FirstOrDefaultAsync(e => e.Email.ToLower() == Email.ToLower());
        }

        public async Task<Employee> UpdateUser(Employee employee)
        {
            var result = await employeeDb.Employees.FirstOrDefaultAsync(e => e.Email == employee.Email);
            if (result != null)
            {
               
                result.Name=employee.Name;
                result.Email=employee.Email;
                await employeeDb.SaveChangesAsync();
                return result;

            }
            return null;
        }
    }
}
