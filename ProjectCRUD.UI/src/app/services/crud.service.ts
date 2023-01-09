import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Guid } from 'guid-typescript';
import { Observable } from 'rxjs/internal/Observable';
import { EmployeeDTO } from '../models/employe.dto';
import { Employee } from '../models/employee';
import { EmployeeUpdateDTO } from '../models/employee.update.dto';

@Injectable({
  providedIn: 'root'
})
export class CrudService {

  constructor(private http: HttpClient) { }

  public getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>('http://localhost:5026/api/Employees');
  }

  public updateEmployee(employeeUpdateDTO: EmployeeUpdateDTO): Observable<Employee[]> {
    return this.http.put<Employee[]>('http://localhost:5026/api/Employees', employeeUpdateDTO);
  }

  /*
  public createEmployee(employeeDTO: EmployeeDTO): Observable<Guid> {
    return this.http.post<Guid>('http://localhost:5026/api/Employees', employeeDTO);
  }
  */

  public createEmployee(employeeDTO: EmployeeDTO): Observable<Employee[]> {
    return this.http.post<Employee[]>('http://localhost:5026/api/Employees', employeeDTO);
  }

  public deleteEmployee(employee: Employee): Observable<Employee[]> {
    return this.http.delete<Employee[]>(`http://localhost:5026/api/Employees/${employee.id}`);
  }
}
