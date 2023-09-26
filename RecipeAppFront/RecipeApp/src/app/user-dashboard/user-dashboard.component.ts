import { Component, OnInit } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-user-dashboard',
  templateUrl: './user-dashboard.component.html',
  styleUrls: ['./user-dashboard.component.css']
})
export class UserDashboardComponent implements OnInit {
  recipes!: any[]; 
  recipeName: string = '';
  description: string = '';
  newIngredients: { name: string, quantity: string }[] = [];
  selectedRecipeId: number | null = null;
  currentRecipeId: number = -1;
  Ingredients: any[] = [];
  bookmarkrecipes!:any[];


  constructor(private http: HttpClient) {}

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

    const jwtToken = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${jwtToken}`
    });
    this.http.get<any[]>('http://localhost:5192/api/Recipe/GetRecipesFromBookmark',{headers}).subscribe((recipe) => {
      
      this.bookmarkrecipes = recipe;
    });
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

  

  removeFromBookmark(recipeId: number) {
    const jwtToken = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${jwtToken}`
    });

    const requestData = { id: recipeId};
    console.log(recipeId);


    this.http.post('http://localhost:5192/api/Recipe/RemoveFromBookmark', recipeId, {headers}).subscribe(
      (response) => {
        console.log(response); // Ovdje možete manipulisati odgovorom sa servera
        // Dodajte logiku za ažuriranje prikaza ili druge akcije
      },
      (error) => {
        console.error(error); // Ovdje možete rukovati greškama
      }
    );
  }

  addToBookmark(recipeId: number): void {
    const apiUrl = 'http://localhost:5192/api/Recipe/AddToBookmark'; // Replace with your API endpoint URL
    const requestData = { id: recipeId };
    const jwtToken = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${jwtToken}`
    });
    console.log(requestData);
    this.http.post(apiUrl,recipeId,{headers,responseType:'text'}).subscribe(
      (response) => {
        console.log('Recipe added to bookmarks:', response);
        // Handle success, you can display a success message or update UI here
      },
      (error) => {
        console.error('Error adding recipe to bookmarks:', error);
        // Handle error, you can display an error message or handle it as needed
      }
    );
  }
}
