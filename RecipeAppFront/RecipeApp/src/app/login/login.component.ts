import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import jwt_decode from 'jwt-decode';
import { Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

interface JwtPayload {
  role: string;
  // Add other properties if needed
}

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  userData = {
    username: '',
    password: ''
  };
  private ngUnsubscribe = new Subject();
  constructor(private http: HttpClient,private router: Router) {}

  loginUser() {
    this.http
    .post('http://localhost:5192/api/Login/login',this.userData,{ responseType: 'text' })
    .pipe(takeUntil(this.ngUnsubscribe))
    .subscribe((data) => {
      localStorage.clear();
      localStorage.setItem('authToken', data);
      console.log(localStorage);

      const jwtToken = localStorage.getItem('authToken')as string;
      const decodedToken: any = jwt_decode(jwtToken);
      const userRole: string = decodedToken['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
      console.log('Uloga korisnika:', userRole);
      if(userRole == 'Admin')
      {
        this.router.navigate(['/admin'])
      }
      else if(userRole == 'Cook')
      {
        this.router.navigate(['/cook'])
      }
      else if(userRole == 'User'){
        this.router.navigate(['/user'])
      }
      
    });
    ;
  }

  private getRoleFromToken(token: string): string {
  try {
    const decodedToken: JwtPayload = jwt_decode(token);
    return decodedToken.role;
  } catch (error) {
    console.error('Error decoding JWT token:', error);
    return ''; // Handle error gracefully
  }
}
}
