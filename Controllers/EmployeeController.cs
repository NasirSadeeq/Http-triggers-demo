using HttpTrigger.Interfaces;
using HttpTrigger.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HttpTrigger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ILogger<EmployeeController> logger;
        private readonly Iemployee iemployee;

        public EmployeeController(ILogger<EmployeeController> logger,Iemployee iemployee)
        {
            this.logger = logger;
            this.iemployee = iemployee;
        }

        [HttpGet("{Email?}")]
        public async Task<ActionResult<Employee>> Get1(string? Email = null)
        {
            try
            {
                if (Email == null)
                {
                    return Ok(await iemployee.GetAllUsers());

                }
                else
                {
                    var result = await iemployee.GetUser(Email.ToLower());
                    if (result == null)
                    {
                        return NotFound("Email not found");
                    }
                    return Ok(result);

                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPost("AddUser")]
        public async Task<ActionResult<Employee>> AddUser(Employee employee)
        {
            try
            {
                if (employee == null)
                    return BadRequest();

                var emp = await iemployee.GetUser(employee.Email.ToLower());
                if (emp != null)
                {
                    ModelState.AddModelError(emp.Email, "User with same Email is already exist");
                    return BadRequest(ModelState);
                }

                var result = await iemployee.AddUser(employee);
                // return CreatedAtAction(nameof(GetUser),new {email=result.Email},result);
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                //return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpPut("{Email}")]
        public async Task<ActionResult<Employee>> UpdateUser(string Email, Employee userDetails)
        {
            try
            {
                if (Email != userDetails.Email.ToLower())
                    return BadRequest("Email MisMatch");

                var emp = await iemployee.GetUser(Email.ToLower());
                if (emp == null)
                {
                    throw new Exception($"Employee with Email {Email} not found");
                    // return BadRequest($"Employee with Email {Email} not found");
                }

                return await iemployee.UpdateUser(userDetails);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{Email}")]
        public async Task<ActionResult> DeleteUser(string Email)
        {
            try
            {
                var emp = await iemployee.GetUser(Email.ToLower());
                if (emp == null)
                {
                    throw new Exception($"Employee with Email {Email} not found");
                    // return BadRequest($"User with Email {Email} not found");
                }

                await iemployee.DeleteUser(Email.ToLower());
                return Ok($"User with Email {Email} Deleted");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
