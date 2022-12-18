import { Skill } from "./skill";

export class EmployeeDTO {
    firstName = "";
    lastName = "";
    doB = new Date();
    email = "";
    skillLevel?: number;
    active = true;
    age?: number;
  }
