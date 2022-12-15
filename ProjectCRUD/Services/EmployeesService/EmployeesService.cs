using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectCRUD.Data;

namespace ProjectCRUD.Services.EmployeesService
{
    public class EmployeesService : IEmployeesService
    {
        private readonly DataContext _context;

        public EmployeesService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            var dbEmployees = await _context.Employees.ToListAsync();
            return dbEmployees;
        }

        public async Task<Employee> GetEmployeeById(int EmployeeID)
        {
            var dbEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == EmployeeID);
            return dbEmployee;
        }

        public async Task<List<Employee>> AddEmployee(Employee newEmployee)
        {
            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            var dbEmployees = await _context.Employees.ToListAsync();
            return dbEmployees;
        }

        public async Task<Employee?> UpdateEmployee(Employee updatedEmployee)
        {
            try
            {
                var dbEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Id == updatedEmployee.Id);

                dbEmployee.FirstName = updatedEmployee.FirstName;
                dbEmployee.LastName = updatedEmployee.LastName;
                dbEmployee.DoB = updatedEmployee.DoB;
                dbEmployee.Email = updatedEmployee.Email;
                dbEmployee.SkillLevel = updatedEmployee.SkillLevel;
                dbEmployee.Active = updatedEmployee.Active;
                dbEmployee.Age = updatedEmployee.Age;

                await _context.SaveChangesAsync();
                
                return dbEmployee;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public async Task<List<Employee>?> DeleteEmployee(int EmployeeID)
        {
            try
            {
                Employee employee = await _context.Employees.FirstAsync(e => e.Id == EmployeeID);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                var dbEmployees = await _context.Employees.ToListAsync();
                return dbEmployees;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}