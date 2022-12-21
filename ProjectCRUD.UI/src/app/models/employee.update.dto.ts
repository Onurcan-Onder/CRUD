import { formatDate } from "@angular/common";
import { Guid } from "guid-typescript";

export class EmployeeUpdateDTO {
    id?: Guid;
    firstName = "";
    lastName = "";
    doB = formatDate(new Date(), 'yyyy-MM-dd', 'en-US');
    email = "";
    skillLevel?: number;
    active = true;
  }  