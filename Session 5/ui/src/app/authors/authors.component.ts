import { Component, OnInit } from '@angular/core';
import { Author } from '../models/author';
import { AuthorsService } from '../authors.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-authors',
  templateUrl: './authors.component.html',
  styleUrls: ['./authors.component.css']
})
export class AuthorsComponent implements OnInit {
  categories: any = ['Classics', 'Fantasy', 'Detective']
  authors: Author[];
  constructor(private authorService: AuthorsService, private router: Router) { }
  ngOnInit(): void {
  }

  getAuthorsByCategory(category: string) {
    this.authorService.returnAuthorsByCategory(category).subscribe(resp => {
      console.log(resp);
      this.authors = resp;
    }, err => {
      console.log(err);
    })

  }

  goToAuthorDetails(genre: string, id: string) {
    this.router.navigate(['/author', genre, id]);
  }

}
