import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Author } from './models/author';
@Injectable({
  providedIn: 'root'
})
export class AuthorsService {

  constructor(private httpClient: HttpClient) { }


  returnAuthorsByCategory(category: string) {
    return this.httpClient.get<Author[]>(`https://localhost:44349/api/Authors/${category}`);
  }

  returnAuthorByGenreAndId(genre: string, id: string) {
    // https://localhost:44349/api/Authors/Classics/ff1eba9d-f9c2-4ff9-85c9-d59a47908b96
    return this.httpClient.get<Author>(` https://localhost:44349/api/Authors/${genre}/${id}`);
  }


  updateAuthor(author: Author) {
    return this.httpClient.put<Author>('https://localhost:44349/api/Authors', author);
  }

}
