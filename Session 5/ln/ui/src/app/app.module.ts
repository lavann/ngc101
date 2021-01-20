import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http'
import { AppRoutingModule } from './app-routing.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from './app.component';
import { NavbarComponent } from './navbar/navbar.component';
import { AuthorsComponent } from './authors/authors.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { HomeComponent } from './home/home.component';
import { AuthorDetailsComponent } from './author-details/author-details.component';
import { LoginComponent } from './login/login.component';
import { 
MsalModule
,MsalInterceptor
,MSAL_CONFIG
,MSAL_CONFIG_ANGULAR
,MsalService
,MsalAngularConfiguration
,MsalGuard
,BroadcastService

}  from '@azure/msal-angular';

import { Configuration } from 'msal';

export const _protectedResourceMap: [string,string[]][] = [
  ['https://graph.microsoft.com/v1.0/me', ['user.read']]
  ,['https://localhost:44349/api/Authors', ['api://f5c8af09-e11e-49ea-8dae-a0eb86318f8e/access_data']]
]


function MSALConfigFactory(): Configuration {
  return {
    auth: {
      clientId: 'aa23d370-fae9-4acf-8ecf-2ab3ca65c397'
      , authority: 'https://login.microsoftonline.com/1c302616-bc6a-45a6-9c07-838c89d55003'
      , redirectUri: 'http://localhost:4200'
      , validateAuthority: true
      , postLogoutRedirectUri: 'http://locahost:4200'
      , navigateToLoginRequestUrl: true
    }
  }
}

function MSALAngularConfigFactor() : MsalAngularConfiguration {
  return {
    popUp: false
    , consentScopes: [
      'user.read'
    ],protectedResourceMap: _protectedResourceMap
  }
}


@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    AuthorsComponent,
    PageNotFoundComponent,
    HomeComponent,
    AuthorDetailsComponent,
    LoginComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule

  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS
      , useClass: MsalInterceptor
      , multi: true
    },
    {
      provide: MSAL_CONFIG
      , useFactory: MSALConfigFactory
    },
    {
      provide: MSAL_CONFIG_ANGULAR
      , useFactory: MSALAngularConfigFactor
    },BroadcastService, MsalService, MsalGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
