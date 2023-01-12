import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Router } from '@angular/router';
import { Guid } from 'guid-typescript';
import { EmployeeDTO } from '../models/employe.dto';
import { Employee } from '../models/employee';
import { EmployeeUpdateDTO } from '../models/employee.update.dto';
import { CrudService } from '../services/crud.service';
@Component({
  selector: 'app-update-employee',
  templateUrl: './update-employee.component.html',
  styleUrls: ['./update-employee.component.css']
})
export class UpdateEmployeeComponent implements OnInit {
  @Input() employee?: Employee;
  @Output() employeesUpdated = new EventEmitter<Employee[]>();

  static flag: boolean;
  public classReference = UpdateEmployeeComponent;

  static firstNameError: boolean;
  static lastNameError: boolean;
  static emailError: boolean;

  emailRegEx: RegExp = /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,}$/i
  static emailRegEx: any;

  constructor(private crudService: CrudService, private router:Router) {
    this.classReference.flag = true;
  }

  ngOnInit(): void {
  }

  updateEmployee(employee:Employee) {

    //* If I don't have a valid token, then forward me to login
    if (sessionStorage.getItem('authToken')== null) {
      this.router.navigate(["Login"]);
    }
    else {
      if (this.validInput(employee)) {
        var employeeUpdateDTO: EmployeeUpdateDTO = new EmployeeUpdateDTO();
        employeeUpdateDTO.id = employee.id;
        employeeUpdateDTO.firstName = employee.firstName;
        employeeUpdateDTO.lastName = employee.lastName;
        employeeUpdateDTO.doB = employee.doB;
        employeeUpdateDTO.email = employee.email;
        employeeUpdateDTO.skillLevel = employee.skillLevel.id;
        employeeUpdateDTO.active = employee.active;
  
        this.crudService
          .updateEmployee(employeeUpdateDTO)
          .subscribe((employees: Employee[]) => this.employeesUpdated.emit(employees));
        
        this.classReference.flag = false;
      }
    }
  }

  deleteEmployee(employee:Employee) {

    //* If I don't have a valid token, then forward me to login
    if (sessionStorage.getItem('authToken')== null) {
      this.router.navigate(["Login"]);
    }
    else {
      if(confirm("Are you sure to delete "+ employee.firstName + "?")) {
        this.crudService
          .deleteEmployee(employee)
          .subscribe((employees: Employee[]) => this.employeesUpdated.emit(employees));
        //window.location.reload();
        this.classReference.flag = false;
      }
    }
  }

  createEmployee(employee:Employee) {

    //* If I don't have a valid token, then forward me to login
    if (sessionStorage.getItem('authToken')== null) {
      this.router.navigate(["Login"]);
    }
    else {
      if (this.validInput(employee)) {
        var employeeDTO: EmployeeDTO = new EmployeeDTO();
        employeeDTO.firstName = employee.firstName;
        employeeDTO.lastName = employee.lastName;
        employeeDTO.doB = employee.doB;
        employeeDTO.email = employee.email;
        employeeDTO.skillLevel = employee.skillLevel.id;
        employeeDTO.active = employee.active;

        this.crudService
          .createEmployee(employeeDTO)
          //.subscribe((id: Guid) => id);
          .subscribe((employees: Employee[]) => this.employeesUpdated.emit(employees));
        
        /*
        this.crudService
          .getEmployees()
          .subscribe((employees: Employee[]) => this.employeesUpdated.emit(employees));
        */

        this.classReference.flag = false;    
      }
    }
  }

  validInput(employee: Employee)
  {
    var validInput = true;
    UpdateEmployeeComponent.resetErrors();

    if (employee.firstName == '') {
      validInput = false;
      UpdateEmployeeComponent.firstNameError = true;
    }

    if (employee.lastName == '') {
      validInput = false;
      UpdateEmployeeComponent.lastNameError = true;
    }

    if (!this.emailRegEx.test(employee.email) && employee.email != "") {
      validInput = false;
      UpdateEmployeeComponent.emailError = true;
    }

    return validInput;
  }

  public static resetErrors() {
    UpdateEmployeeComponent.firstNameError = false;
    UpdateEmployeeComponent.lastNameError = false;
    UpdateEmployeeComponent.emailError = false;
  }
}
