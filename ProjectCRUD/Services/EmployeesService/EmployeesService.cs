using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectCRUD.Data;
using ProjectCRUD.DTOs;
using ProjectCRUD.Services.CacheService;

namespace ProjectCRUD.Services.EmployeesService
{
    public class EmployeesService : IEmployeesService
    {
        private readonly DataContext _context;
        private readonly ICacheService _cacheService;

        public EmployeesService(DataContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        public async Task<List<Employee>> GetAllEmployees()
        {
            //return await _context.Employees.Include(s => s.SkillLevel).ToListAsync();

            //* Caching
            var cacheData = _cacheService.GetData();

            if (cacheData != null && cacheData.Count() > 0)
                return cacheData;

            cacheData = await _context.Employees.Include(s => s.SkillLevel).ToListAsync();
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData(cacheData, expiryTime);
            return cacheData;
        }

        //! Test
        public async Task<Employee?> GetEmployeeById(Guid EmployeeID)
        {
            var dbEmployee = await _context.Employees.Include(s => s.SkillLevel).FirstOrDefaultAsync(e => e.Id == EmployeeID);
            return dbEmployee;
        }

        public async Task<List<Employee>> AddEmployee(EmployeeDTO newEmployeeDTO)
        //public async Task<Guid> AddEmployee(EmployeeDTO newEmployeeDTO)
        {
            var skillLevel = await _context.SkillLevels.FirstOrDefaultAsync(s => s.Id == newEmployeeDTO.SkillLevel);

            var today = DateTime.Today;
            var age = today.Year - newEmployeeDTO.DoB.Year;
            if (newEmployeeDTO.DoB.Date > today.AddYears(-age)) age--;

            var newEmployee = new Employee {
                FirstName = newEmployeeDTO.FirstName,
                LastName = newEmployeeDTO.LastName,
                DoB = newEmployeeDTO.DoB,
                Email = newEmployeeDTO.Email,
                SkillLevel = skillLevel,
                Active = newEmployeeDTO.Active,
                Age = age
            };

            var addedEmployee = _context.Employees.Add(newEmployee);
            await _context.SaveChangesAsync();

            //return await _context.Employees.Include(s => s.SkillLevel).ToListAsync();
            
            //* Caching
            var cacheData = await _context.Employees.Include(s => s.SkillLevel).ToListAsync();
            var expiryTime = DateTimeOffset.Now.AddSeconds(30);
            _cacheService.SetData(cacheData, expiryTime);
            return cacheData;
            //return addedEmployee.Entity.Id;
        }

        public async Task<List<Employee>?> UpdateEmployee(EmployeeUpdateDTO updatedEmployeeDTO)
        //public async Task<Employee?> UpdateEmployee(EmployeeUpdateDTO updatedEmployeeDTO)
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
                
                var today = DateTime.Today;
                var age = today.Year - updatedEmployeeDTO.DoB.Year;
                if (updatedEmployeeDTO.DoB.Date > today.AddYears(-age)) age--;
                dbEmployee.Age = age;

                await _context.SaveChangesAsync();

                //return await _context.Employees.Include(s => s.SkillLevel).ToListAsync();

                //* Caching
                var cacheData = await _context.Employees.Include(s => s.SkillLevel).ToListAsync();
                var expiryTime = DateTimeOffset.Now.AddSeconds(30);
                _cacheService.SetData(cacheData, expiryTime);
                return cacheData;
                //return dbEmployee;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public async Task<List<Employee>?> DeleteEmployee(Guid EmployeeID)
        //public async Task DeleteEmployee(Guid EmployeeID)
        {
            try
            {
                Employee employee = await _context.Employees.Include(s => s.SkillLevel).FirstAsync(e => e.Id == EmployeeID);
                _context.Employees.Remove(employee);
                await _context.SaveChangesAsync();

                //return await _context.Employees.Include(s => s.SkillLevel).ToListAsync();

                //* Caching
                var expiryTime = DateTimeOffset.Now.AddSeconds(30);
                return _cacheService.RemoveData(employee, expiryTime);
                //_cacheService.RemoveData(employee, expiryTime);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
