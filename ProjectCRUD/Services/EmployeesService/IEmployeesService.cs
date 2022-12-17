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
        Task<Employee?> GetEmployeeById(int EmployeeID);
        Task<List<Employee>> AddEmployee(EmployeeDTO newEmployeeDTO);
        Task<Employee?> UpdateEmployee(EmployeeUpdateDTO updatedEmployeeDTO);
        Task<List<Employee>?> DeleteEmployee(int EmployeeID);
    }
}