import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent {
  userData = {
    username: '',
    password: ''
  };

  constructor(private http: HttpClient,private router: Router) {}

  registerUser() {
    // Pozovite HTTP POST zahtev za registraciju
    const registerDto = {
      username: this.userData.username,
      password: this.userData.password
    };

    this.http.post('http://localhost:5192/api/Login/register', registerDto).subscribe(
      () => {
        // Uspešno registrovan korisnik
        alert('Uspešno ste se registrovali.');
        this.router.navigate(['/login']);


      },
      (error) => {
        // Greška prilikom registracije
        console.error('Greška prilikom registracije:', error);
        alert('Došlo je do greške prilikom registracije.');
      }
    );
  }
}
