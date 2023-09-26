import { Component, OnInit } from '@angular/core';
import { HttpClient,HttpHeaders  } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

export interface IngredientDto {
  name: string;
  quantity: string;
}

@Component({
  selector: 'app-admin-dashboard',
  templateUrl: './admin-dashboard.component.html',
  styleUrls: ['./admin-dashboard.component.css']
})
export class AdminDashboardComponent implements OnInit {
  ingredientForm: FormGroup = this.formBuilder.group({
  ingredientName: '', // Kontrola za vrstu sastojka
    quantity: '',       // Kontrola za količinu
  });
  cooks: any[] = []; 
  isAddCookFormVisible = false;
  newCook: any = {
    id:null,
    username: '',
    password: ''
  };
  recipes!: any[]; 
  ingredients: any[] = [];
  selectedRecipeId: number | null = null;
  currentRecipeId: number = -1;
  newIngredients: IngredientDto[] = [];
  foundRecipes!: any[];
  searchTerm = '';



  constructor(private http: HttpClient,private formBuilder: FormBuilder) {}

  search() {
    this.http.get<any[]>(`http://localhost:5192/api/Recipe/Search?searchTerm=${this.searchTerm}`).subscribe((data) => {
      this.foundRecipes = [];
      this.foundRecipes = data;
    });

  }

  addIngredientsToRecipe(recipeId: number, ingredientName: string, quantity: string) {
    
    const apiUrl = 'http://localhost:5192/api/Recipe/AddIngredients?id=' + recipeId;

    const jwtToken = localStorage.getItem('authToken');
    
  
    const httpOptions = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json',
        Authorization: `Bearer ${jwtToken}`
      })
    };
    // Kreirajte telo zahteva sa podacima iz obrasca
    const requestBody = {
      //recipeId: recipeId,
      newIngredients: [
        {
          name: ingredientName,
          quantity: quantity
        }
      ]
    };
  
    // Izvršite HTTP POST zahtev
    this.http.post(apiUrl, requestBody, httpOptions)
      .subscribe(
        (response) => {
          console.log('Sastojci su dodati receptu.', response);
          // Ovde možete dodati logiku za rukovanje uspešnim odgovorom
        },
        (error) => {
          console.error('Došlo je do greške prilikom dodavanja sastojaka.', error);
          // Ovde možete dodati logiku za rukovanje greškom
        }
      );
  }

  onSubmit(recipeId: number) {
    if (this.ingredientForm) {
      const ingredientName = this.ingredientForm.get('ingredientName')?.value;
      const quantity = this.ingredientForm.get('quantity')?.value;
  
      if (ingredientName !== null && quantity !== null) {
        // Sada možete raditi sa vrednostima sastojaka
        const newIngredient: IngredientDto = {
          name: ingredientName,
          quantity: quantity
        };
  
        // Dodajte novi sastojak u listu
        this.ingredients.push(newIngredient);
        this.addIngredientsToRecipe(recipeId, ingredientName, quantity);
  
        // Resetujte formu nakon dodavanja sastojka
        this.ingredientForm.reset();
      } else {
        // Ovde možete rukovati situacijom kada su vrednosti null
        console.error('Vrednosti su null');
      }
      
    } else {
      // Ovde možete rukovati situacijom kada je ingredientForm null
      console.error('ingredientForm je null');

    }
   


  }

  openAddCookForm() {
    this.isAddCookFormVisible = true; // Postavite vidljivost forme na true
  }
  closeAddCookForm() {
    this.isAddCookFormVisible = false; // Postavite vidljivost forme na false
  }

  addCook() {
    const jwtToken = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${jwtToken}`
    });
    this.http.post('http://localhost:5192/api/Cook/addCook', this.newCook,{headers,responseType:'text'})
      .subscribe((data: any) => {
        console.log(data);
        this.closeAddCookForm();
        this.loadCooks();
      });
  }
  
  loadCooks() {
    this.http.get('http://localhost:5192/api/Cook/getAllCooks')
      .subscribe((data: any) => {
        this.cooks = data;
        console.log(data);
      });
  }

  deleteRecipe(id: number) {
    const url = `http://localhost:5192/api/Recipe/DeleRecipe?id=${id}`;
    const jwtToken = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${jwtToken}`
    });


    this.http
      .delete(url, { headers,responseType:'text'})
      .subscribe(
        (response) => {
          console.log('Recipe deleted:', response);
          // Handle success here
        },
        (error) => {
          console.error('Error deleting recipe:', error);
          // Handle error here
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

  addIngredient(recipeId: number) {
    this.currentRecipeId = recipeId;
    const jwtToken = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      'Content-Type':  'application/json',
      Authorization: `Bearer ${jwtToken}`
    });

    
  }


  ngOnInit(): void {
    this.ingredientForm = this.formBuilder.group({
      ingredientName: '',
      quantity: ''
    });
    
    const jwtToken = localStorage.getItem('authToken');
    const headers = new HttpHeaders({
      Authorization: `Bearer ${jwtToken}`
    });


    this.http.get('http://localhost:5192/api/Cook/getAllCooks',{ headers })
      .subscribe((data: any) => {
        this.cooks = data;
        console.log(data);
      });

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
}
