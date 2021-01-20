import { Component, OnInit } from '@angular/core';
import { MsalService } from '@azure/msal-angular';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {
  isCollapsed = false;
  constructor(private msalService: MsalService) { }

  ngOnInit(): void {
  }

  isLoggedIn(): boolean {
    return !!this.msalService.getAccount();
  }

  login() {
    this.msalService.loginRedirect();
  }

  logout() {
    this.msalService.logout();
  }

}
