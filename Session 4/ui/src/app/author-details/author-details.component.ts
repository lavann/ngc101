import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthorsService } from '../authors.service';
import { Author } from '../models/author';

@Component({
  selector: 'app-author-details',
  templateUrl: './author-details.component.html',
  styleUrls: ['./author-details.component.css'],
})
export class AuthorDetailsComponent implements OnInit {
  genre: string;
  id: string;
  author: Author;
  constructor(
    private route: ActivatedRoute,
    private authorService: AuthorsService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.id = params['id'];
      this.genre = params['genre'];

      this.authorService
        .returnAuthorByGenreAndId(this.genre, this.id)
        .subscribe(
          (resp) => {
            console.log(resp);
            this.author = resp;
          },
          (error) => {
            console.log(error);
          }
        );
    });
  }

  updateAuthor() {
    this.authorService.updateAuthor(this.author).subscribe(
      (response) => {
        console.log(response);
        this.author = response;
      },
      (error) => {
        console.log(error);
      }
    );
  }
}
