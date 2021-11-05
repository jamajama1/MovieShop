import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { MovieCard } from 'src/app/shared/models/moviecard';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MovieService {
  // private readonly httpClient _http
  constructor(private http: HttpClient) { }

  getTopRevenueMovies(): Observable<MovieCard[]>{
    return this.http.get<MovieCard[]>('https://localhost:44315/api/Movies/toprevenue');  
  }

  getTopRatedMovies(): Observable<MovieCard[]>{
    return this.http.get<MovieCard[]>('https://localhost:44315/api/Movies/toprated');  
  }

  getMovieDetails(): Observable<MovieCard[]>{
    return this.http.get<MovieCard[]>('https://localhost:44315/api/Movies/2');  
  }

}
