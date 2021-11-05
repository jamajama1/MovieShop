import { Component, OnInit } from '@angular/core';
import { GenreService } from '../core/services/genre.service';
import { MovieService } from '../core/services/movie.service';
import { GenreModel } from '../shared/models/genresModel';
import { MovieCard } from '../shared/models/moviecard';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  myPageTitle= "Movie Shop SPA"
  movieCards!: MovieCard[];
  genreModel!: GenreModel[];
  constructor(private movieservice: MovieService, private genreservice: GenreService) { }

  ngOnInit(): void {
    this.movieservice.getTopRevenueMovies().subscribe(
      m=> { 
      this.movieCards = m;
      console.table(this.movieCards);
    });

    this.movieservice.getMovieDetails().subscribe(
      m=> { 
      this.movieCards = m;
      console.table(this.movieCards);
    });

    this.genreservice.getAllGenres().subscribe(g=>{
      this.genreModel = g;
      console.table(this.genreModel);
    });
  }

}
