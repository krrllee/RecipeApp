import { Component, OnInit } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';


@Component({
  selector: 'app-cook-dashboard',
  templateUrl: './cook-dashboard.component.html',
  styleUrls: ['./cook-dashboard.component.css']
})
export class CookDashboardComponent implements OnInit {
  recipeName: string = '';
  description: string = '';
  newIngredients: { name: string, quantity: string }[] = [];
  recipes!: any[]; 
  selectedRecipeId: number | null = null;
  currentRecipeId: number = -1;
  Ingredients: any[] = [];
  foundRecipes!: any[];
  searchTerm = '';


  constructor(private http: HttpClient) {}

  search() {
    this.http.get<any[]>(`http://localhost:5192/api/Recipe/Search?searchTerm=${this.searchTerm}`).subscribe((data) => {
      this.foundRecipes = [];
      this.foundRecipes = data;
    });

  }

  ngOnInit(): void {
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
      this.Ingredients = []; // Resetujte listu sastojaka
    } else {
      // Otvaranje sastojaka za odabrani recept
      this.selectedRecipeId = recipeId;
      // Pozovite API za dobijanje sastojaka za trenutni recept
      this.http.get<any[]>('http://localhost:5192/api/Recipe/GetIngredients?id=' + recipeId).subscribe(
        (data) => {
          console.log(data);
          this.Ingredients = data;
        },
        (error) => {
          console.error('Greška prilikom dobijanja sastojaka:', error);
        }
      );
    }
  }


  addIngredient() {
    this.newIngredients.push({ name: '', quantity: '' });
  }

  addRecipe() {
    const recipeData = {
      name: this.recipeName,
      description: this.description,
      ingredients: this.newIngredients
    };

    const jwtToken = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${jwtToken}`
    });

    this.http.post('http://localhost:5192/api/Recipe/AddRecipe', recipeData,{headers,responseType:'text'}).subscribe(
      (response) => {
        console.log('Recipe added:', response);
        this.newIngredients = [];
      },
      (error) => {
        console.error('Error adding recipe:', error);
        // Ovdje možete dodati kod za prikaz poruke o grešci
      }
    );
  }

  
}
