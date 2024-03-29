using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectCRUD.DTOs;

namespace ProjectCRUD.Services.EmployeesService
{
    public interface IEmployeesService
    {
        Task<List<Employee>> GetAllEmployees();
        
        Task<List<Employee>> AddEmployee(EmployeeDTO newEmployeeDTO);
        //Task<Guid> AddEmployee(EmployeeDTO newEmployeeDTO);
        
        Task<List<Employee>?> UpdateEmployee(EmployeeUpdateDTO updatedEmployeeDTO);
        //Task<Employee?> UpdateEmployee(EmployeeUpdateDTO updatedEmployeeDTO);

        Task<List<Employee>?> DeleteEmployee(Guid EmployeeID);
        //Task DeleteEmployee(Guid EmployeeID);
    }
}