import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { User } from '../models/user';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  title = 'ProjectCRUD.UI';
  user = new User();

  constructor(private authService: AuthService, private router:Router) {}

  ngOnInit(): void {
    //throw new Error('Method not implemented.');
  }

  login(user: User) {
    this.authService.login(user).subscribe((token: string) => {
      localStorage.setItem('authToken', token);
      this.router.navigate(["Employees"]);
    });
  }

  /*
  get(pageName:string) {
    this.authService.get().subscribe((name: string) => {
      this.router.navigate([`${pageName}`]);
      console.log(name);
    });
  }
  */
}
