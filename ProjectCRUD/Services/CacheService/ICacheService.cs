using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectCRUD.Services.CacheService
{
    public interface ICacheService
    {
        List<Employee>? GetData();
        bool SetData(List<Employee> employees, DateTimeOffset expirationTime);
        List<Employee>? RemoveData(Employee employee,  DateTimeOffset expirationTime);
    }
}