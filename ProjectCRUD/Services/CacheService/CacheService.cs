using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace ProjectCRUD.Services.CacheService
{
    public class CacheService : ICacheService
    {
        private IDatabase _cacheDb;

        public CacheService()
        {
            var redis = ConnectionMultiplexer.Connect("localhost:6379");
            _cacheDb = redis.GetDatabase();
        }

        public List<Employee>? GetData()
        {
            var value = _cacheDb.StringGet("employees");
            if (!string.IsNullOrEmpty(value))
                return JsonSerializer.Deserialize<List<Employee>>(value);
            
            return default;
        }

        public List<Employee>? RemoveData(Employee employee,  DateTimeOffset expirationTime)
        {
            var value = _cacheDb.StringGet("employees");
            List<Employee> employees = JsonSerializer.Deserialize<List<Employee>>(value);

            if (employees.Any(e => e.Id == employee.Id))
            {
                employees = employees.Where(e => e.Id != employee.Id).ToList();
                var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
                _cacheDb.StringSet("employees", JsonSerializer.Serialize(employees), expiryTime);
                return employees;
            }
            
            return default;
        }

        public bool SetData(List<Employee> employees, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.Now);
            return _cacheDb.StringSet("employees", JsonSerializer.Serialize(employees), expiryTime);
        }
    }
}
