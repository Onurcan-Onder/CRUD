import { Component, OnInit } from '@angular/core';
import { EmployeeDTO } from '../models/employe.dto';
import { Employee } from '../models/employee';
import { EmployeeUpdateDTO } from '../models/employee.update.dto';
import { CrudService } from '../services/crud.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  employees: Employee[] = [];
  employeeToUpdate?: Employee;

  constructor(private crudService: CrudService) { }

  ngOnInit(): void {
    this.crudService
      .getEmployees()
      .subscribe((result: Employee[]) => (this.employees = result));
  }

  updateEmployeeList(employees: Employee[]) {
    this.employees = employees;
  }

  createEmployee()
  {
    this.employeeToUpdate = new Employee();
  }

  updateEmployee(employe: Employee)
  {
    this.employeeToUpdate = employe;
  }
}
