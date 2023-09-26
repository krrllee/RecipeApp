import { Component, OnInit, ChangeDetectorRef  } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';


@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  recipes!: any[]; // Ovdje trebate definirati odgovarajuću strukturu za recepte
  ingredients: any[] = [];
  selectedRecipeId: number | null = null;


  constructor(private http: HttpClient,private router: Router,private cdr: ChangeDetectorRef) {}

  ngOnInit() {
    // Dohvatanje recepta sa servera
    this.http.get<any[]>('http://localhost:5192/api/Recipe/GetAllRecipes').subscribe(
      (data) => {
        console.log(data);
        this.recipes = data;
      },
      (error) => {
        console.error('Greška prilikom dobijanja recepata:', error);
      }
    );



    
  }


  toggleIngredients(recipeId: number) {
    if (this.selectedRecipeId === recipeId) {
      // Zatvori sastojke ako su već otvoreni
      this.selectedRecipeId = null;
      this.ingredients = []; // Resetujte listu sastojaka
    } else {
      // Otvaranje sastojaka za odabrani recept
      this.selectedRecipeId = recipeId;
      // Pozovite API za dobijanje sastojaka za trenutni recept
      this.http.get<any[]>('http://localhost:5192/api/Recipe/GetIngredients?id=' + recipeId).subscribe(
        (data) => {
          console.log(data);
          this.ingredients = data;
          this.cdr.detectChanges();
        },
        (error) => {
          console.error('Greška prilikom dobijanja sastojaka:', error);
        }
      );
    }
  }


  getIngredients(id: number) {
    this.http.get<any[]>('http://localhost:5192/api/Recipe/GetIngredients?id=' + id).subscribe(
      (data) => {
        this.ingredients = data;
      },
      (error) => {
        console.error('Greška prilikom dobijanja sastojaka:', error);
      }
    );
  }

  login() {
    this.router.navigate(['/login']);
  }

  register() {
    this.router.navigate(['/registration']);
  }
}
