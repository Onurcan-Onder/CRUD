import { Skill } from "./skill";

export class EmployeeUpdateDTO {
    id?: number;
    firstName = "";
    lastName = "";
    doB = new Date();
    email = "";
    skillLevel?: number;
    active = true;
    age?: number;
  }  