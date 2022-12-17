import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { Employee } from '../models/employee';

@Injectable({
  providedIn: 'root'
})
export class CrudService {

  constructor(private http: HttpClient) { }

  /*
  public getEmployees() : Employee[] {
    let employee = new Employee();
    
    employee.id = 1;
    employee.firstName = "Peter";
    employee.lastName = "Parker";
    employee.doB = new Date();
    employee.email = "peter@gmail.com";
    employee.skillLevel.id = 1;
    employee.skillLevel.name = "Skill 1";
    employee.skillLevel.description = "Description 1";
    employee.active = true;
    employee.age = 25;

    return [employee];
  }
  */

  public getEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>('http://localhost:5026/api/Employees');
  }
}
