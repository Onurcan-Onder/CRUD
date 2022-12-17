import { Skill } from "./skill";

export class Employee {
    id?: number;
    firstName = "";
    lastName = "";
    doB = new Date();
    email = "";
    skillLevel = new Skill();
    active = true;
    age?: number;
  }  