using Microsoft.EntityFrameworkCore;
using ServiceStack.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace HttpTrigger.Models
{
    public class Employee
    {
       
        public int Id { get; set; }
        public string Name { get; set; }
        [Key]
        [EmailAddress(ErrorMessage ="Enter a valid Email address")]
        public string Email { get; set; }
    }
}
