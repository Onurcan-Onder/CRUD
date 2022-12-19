import { formatDate } from "@angular/common";

export class EmployeeUpdateDTO {
    id?: number;
    firstName = "";
    lastName = "";
    doB = formatDate(new Date(), 'yyyy-MM-dd', 'en-US');
    email = "";
    skillLevel?: number;
    active = true;
    age?: number;
  }  