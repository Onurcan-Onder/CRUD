using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        public async Task<List<Employee>> AddEmployee(EmployeeDTO newEmployeeDTO)
        //public async Task<Guid> AddEmployee(EmployeeDTO newEmployeeDTO)
        {
            var today = DateTime.Today;
            var age = today.Year - newEmployeeDTO.DoB.Year;
            if (newEmployeeDTO.DoB.Date > today.AddYears(-age)) age--;
            if (age < 0) age = 0;

            //! Test
            var tempData = GetAllEmployees().Result;
            if (ValidInput(tempData, null, newEmployeeDTO.FirstName, newEmployeeDTO.LastName, newEmployeeDTO.Email, newEmployeeDTO.SkillLevel))
            {
                var skillLevel = await _context.SkillLevels.FirstOrDefaultAsync(s => s.Id == newEmployeeDTO.SkillLevel);

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

            return null;
        }

        public async Task<List<Employee>?> UpdateEmployee(EmployeeUpdateDTO updatedEmployeeDTO)
        //public async Task<Employee?> UpdateEmployee(EmployeeUpdateDTO updatedEmployeeDTO)
        {
            try
            {
                var today = DateTime.Today;
                var age = today.Year - updatedEmployeeDTO.DoB.Year;
                if (updatedEmployeeDTO.DoB.Date > today.AddYears(-age)) age--;
                if (age < 0) age = 0;

                //! Test
                var tempData = GetAllEmployees().Result;
                if (ValidInput(tempData, updatedEmployeeDTO.Id, updatedEmployeeDTO.FirstName, updatedEmployeeDTO.LastName, updatedEmployeeDTO.Email, updatedEmployeeDTO.SkillLevel))
                {
                    var dbEmployee = await _context.Employees.Include(s => s.SkillLevel).FirstOrDefaultAsync(e => e.Id == updatedEmployeeDTO.Id);
                    var skillLevel = await _context.SkillLevels.FirstOrDefaultAsync(s => s.Id == updatedEmployeeDTO.SkillLevel);

                    dbEmployee.FirstName = updatedEmployeeDTO.FirstName;
                    dbEmployee.LastName = updatedEmployeeDTO.LastName;
                    dbEmployee.DoB = updatedEmployeeDTO.DoB;    
                    dbEmployee.Email = updatedEmployeeDTO.Email;
                    dbEmployee.SkillLevel = skillLevel;
                    dbEmployee.Active = updatedEmployeeDTO.Active;
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
                return null;
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

        bool ValidInput(List<Employee> tempData, Guid? id, string firstName, string lastName, string email, int skillLevel)
        {
            if (firstName == "" || !Regex.IsMatch(firstName, @"^[a-zA-Z]+$"))
                return false;
            if (lastName == "" || !Regex.IsMatch(lastName, @"^[a-zA-Z]+$"))
                return false;
            if (email != "" && !Regex.IsMatch(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                return false;
            if (skillLevel < 1 || skillLevel > 3)
                return false;
            if (email != "" && tempData.Any(x => (x.Email.ToLower() == email.ToLower()) && (x.Id != id)))
                return false;

            return true;
        }
    }
}
