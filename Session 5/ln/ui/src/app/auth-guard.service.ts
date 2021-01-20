import { Injectable } from '@angular/core';
import { AuthService }  from './auth.service';
import {  CanActivate,  Router } from '@angular/router';



@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {

  constructor(private authsService: AuthService, private router: Router )  { }
  canActivate(): boolean {
   
    if(!this.authsService.isAuthenticated){
      this.router.navigate(['login']);
      return false;
    }
    
    return true;
  }
}
