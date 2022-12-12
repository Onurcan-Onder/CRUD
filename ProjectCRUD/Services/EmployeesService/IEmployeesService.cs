using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCRUD.Services.EmployeesService
{
    public interface IEmployeesService
    {
        Task<List<Employee>> GetAllEmployees();
        Task<Employee> GetEmployeeById(int EmployeeID);
        Task<List<Employee>> AddEmployee(Employee newEmployee);
        Task<Employee> UpdateEmployee(Employee updatedEmployee);
        Task<List<Employee>> DeleteEmployee(int EmployeeID);
    }
}