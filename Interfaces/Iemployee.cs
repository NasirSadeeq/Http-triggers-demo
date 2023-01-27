using HttpTrigger.Models;
using System.Diagnostics;

namespace HttpTrigger.Interfaces
{
    public interface Iemployee
    {
        Task<Employee> AddUser(Employee employee);
        Task<Employee> UpdateUser(Employee employee);
        Task DeleteUser(string Email);
        Task<IEnumerable<Employee>> GetAllUsers();
        Task<Employee> GetUser(string Email);
    }
}
