import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthorsComponent } from './authors/authors.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomeComponent } from './home/home.component';
import { AuthorDetailsComponent } from './author-details/author-details.component';
import { AuthGuardService} from './auth-guard.service';
import { LoginComponent } from './login/login.component';
import { MsalGuard } from '@azure/msal-angular';
const routes: Routes = [
  // { path: 'authors', component: AuthorsComponent },
  // { path: 'author/:genre/:id', component: AuthorDetailsComponent },
  { path: 'authors', component: AuthorsComponent, canActivate:[MsalGuard] },
  { path: 'author/:genre/:id', component: AuthorDetailsComponent, canActivate:[MsalGuard] },
  { path: 'home', component: HomeComponent }, 
  { path: '', redirectTo: '/home', pathMatch: 'full' }, // redirect to `first-component`
  { path: '**', component: PageNotFoundComponent }  // Wildcard route for a 404 page
  
];

@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation:'disabled'
  })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
