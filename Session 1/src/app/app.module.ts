import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,//provides critical services that are essential to launch and run our app in the browser
    AppRoutingModule// routing module which provides the application-wide configured services with routes in the root module
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
