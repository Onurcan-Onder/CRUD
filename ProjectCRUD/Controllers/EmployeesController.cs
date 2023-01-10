using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectCRUD.DTOs;
using ProjectCRUD.Services.EmployeesService;

namespace ProjectCRUD.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        
        public EmployeesController(IEmployeesService employeesService)
        {
            _employeesService = employeesService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> Get()
        {
            return Ok(await _employeesService.GetAllEmployees());
        }

        [HttpPost]
        public async Task<ActionResult<List<Employee>>> Add(EmployeeDTO newEmployeeDTO)
        {
            return Ok(await _employeesService.AddEmployee(newEmployeeDTO));
        }

        [HttpPut]
        public async Task<ActionResult<List<Employee>>> Update(EmployeeUpdateDTO? updatedEmployee)
        {
            if (updatedEmployee == null)
            {
                return NotFound();
            }
            return Ok(await _employeesService.UpdateEmployee(updatedEmployee));
        }

        [HttpDelete("{EmployeeID}")]
        public async Task<ActionResult<Employee>> Delete(Guid EmployeeID)
        {
            var response = await _employeesService.DeleteEmployee(EmployeeID);
            
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);
            
            /*
            await _employeesService.DeleteEmployee(EmployeeID);
            return Ok();
            */
        }
    }
}