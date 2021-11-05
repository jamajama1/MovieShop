import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import {HttpClient} from '@angular/common/http';
import { GenreModel } from 'src/app/shared/models/genresModel';


@Injectable({
  providedIn: 'root'
})
export class GenreService {

  constructor(private http: HttpClient) { }

  getAllGenres(): Observable<GenreModel[]>{
    return this.http.get<GenreModel[]>('https://localhost:44315/api/Genres');  
  }
}
