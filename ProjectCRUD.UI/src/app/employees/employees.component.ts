import { Component, OnInit } from '@angular/core';
import { EmployeeDTO } from '../models/employe.dto';
import { Employee } from '../models/employee';
import { EmployeeUpdateDTO } from '../models/employee.update.dto';
import { CrudService } from '../services/crud.service';
import { Router } from '@angular/router';
import { UpdateEmployeeComponent } from "../update-employee/update-employee.component";

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {
  
  public classReference = EmployeesComponent;
  static employees: Employee[] = [];
  employeeToUpdate?: Employee;

  constructor(private crudService: CrudService, private router:Router) { }

  ngOnInit(): void {
    //* If I don't have a valid token, then forward me to login
    if (sessionStorage.getItem('authToken')== null) {
      this.router.navigate(["Login"]);
    }
    else{
      this.crudService
      .getEmployees()
      .subscribe((result: Employee[]) => (EmployeesComponent.employees = result));
    }
  }

  updateEmployeeList(employees: Employee[]) {
    EmployeesComponent.employees = employees;
  }

  createEmployee()
  {
    UpdateEmployeeComponent.flag = true;
    UpdateEmployeeComponent.resetErrors();
    this.employeeToUpdate = new Employee();
  }

  updateEmployee(employe: Employee)
  {
    UpdateEmployeeComponent.flag = true;
    UpdateEmployeeComponent.resetErrors();
    this.employeeToUpdate = employe;
  }
}
