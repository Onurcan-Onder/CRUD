import { formatDate } from "@angular/common";
import { Guid } from "guid-typescript";
import { Skill } from "./skill";

export class Employee {
    id?: Guid;
    firstName = "";
    lastName = "";
    doB = formatDate(new Date(), 'yyyy-MM-dd', 'en-US');
    email = "";
    skillLevel = new Skill();
    active = true;
    age?: number;
  }  