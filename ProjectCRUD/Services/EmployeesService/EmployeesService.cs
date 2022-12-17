using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectCRUD.Data;
using ProjectCRUD.DTOs;

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
            var dbEmployees = await _context.Employees.Include(s => s.SkillLevel).ToListAsync();
            return dbEmployees;
        }

        public async Task<Employee?> GetEmployeeById(int EmployeeID)
        {
            var dbEmployee = await _context.Employees.Include(s => s.SkillLevel).FirstOrDefaultAsync(e => e.Id == EmployeeID);
            return dbEmployee;
        }

        public async Task<List<Employee>> AddEmployee(EmployeeDTO newEmployeeDTO)
        {
            var skillLevel = await _context.SkillLevels.FirstOrDefaultAsync(s => s.Id == newEmployeeDTO.SkillLevel);

            var newEmployee = new Employee {
                FirstName = newEmployeeDTO.FirstName,
                LastName = newEmployeeDTO.LastName,
                DoB = newEmployeeDTO.DoB,
                Email = newEmployeeDTO.Email,
                SkillLevel = skillLevel,
                Active = newEmployeeDTO.Active,
                Age = newEmployeeDTO.Age
            };

            _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();
            var dbEmployees = await _context.Employees.Include(s => s.SkillLevel).ToListAsync();
            return dbEmployees;
        }

        public async Task<Employee?> UpdateEmployee(EmployeeUpdateDTO updatedEmployeeDTO)
        {
            try
            {
                var dbEmployee = await _context.Employees.Include(s => s.SkillLevel).FirstOrDefaultAsync(e => e.Id == updatedEmployeeDTO.Id);
                var skillLevel = await _context.SkillLevels.FirstOrDefaultAsync(s => s.Id == updatedEmployeeDTO.SkillLevel);

                dbEmployee.FirstName = updatedEmployeeDTO.FirstName;
                dbEmployee.LastName = updatedEmployeeDTO.LastName;
                dbEmployee.DoB = updatedEmployeeDTO.DoB;
                dbEmployee.Email = updatedEmployeeDTO.Email;
                dbEmployee.SkillLevel = skillLevel;
                dbEmployee.Active = updatedEmployeeDTO.Active;
                dbEmployee.Age = updatedEmployeeDTO.Age;

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
                Employee employee = await _context.Employees.Include(s => s.SkillLevel).FirstAsync(e => e.Id == EmployeeID);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();
                var dbEmployees = await _context.Employees.Include(s => s.SkillLevel).ToListAsync();
                return dbEmployees;
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}