import { formatDate } from "@angular/common";
import { Skill } from "./skill";

export class Employee {
    id?: number;
    firstName = "";
    lastName = "";
    doB = formatDate(new Date(), 'yyyy-MM-dd', 'en-US');
    email = "";
    skillLevel = new Skill();
    active = true;
    age?: number;
  }  