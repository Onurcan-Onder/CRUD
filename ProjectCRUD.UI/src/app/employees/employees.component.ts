import { Component, OnInit } from '@angular/core';
import { Employee } from '../models/employee';
import { CrudService } from '../services/crud.service';

@Component({
  selector: 'app-employees',
  templateUrl: './employees.component.html',
  styleUrls: ['./employees.component.css']
})
export class EmployeesComponent implements OnInit {

  employees: Employee[] = [];

  constructor(private crudService: CrudService) { }

  ngOnInit(): void {
    /*
    this.employees = this.crudService.getEmployees();
    console.log(this.employees);
    */
    this.crudService.getEmployees().subscribe((result: Employee[]) => (this.employees = result));
  }

}
