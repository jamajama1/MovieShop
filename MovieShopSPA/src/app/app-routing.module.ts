import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';

// specify all the routes required by the angular application
const routes: Routes = [
  // path/route for my home page http://localhost:4200/

  { path: "", component: HomeComponent }
  // {path: "movies/:id", component: MovieDetailsComponent},
  // {path: "movies/:id", component:MovieDetailsComponent }
  // {path: "admin/createmovie", component: CreateMovieComponent}

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
