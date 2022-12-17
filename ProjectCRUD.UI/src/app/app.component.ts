import { Component } from '@angular/core';
import { User } from './models/user';
import { AuthService } from './services/auth.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
})
export class AppComponent {
  title = 'ProjectCRUD.UI';
  user = new User();

  constructor(private authService: AuthService) {}

  login(user: User) {
    this.authService.login(user).subscribe((token: string) => {
      localStorage.setItem('authToken', token);
    });
  }

  get() {
    this.authService.get().subscribe((name: string) => {
      console.log(name);
    });
  }
}
