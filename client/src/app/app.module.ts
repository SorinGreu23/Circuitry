import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
<<<<<<< HEAD
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
=======
import { HttpClientModule } from '@angular/common/http';
>>>>>>> c3422c0709b2b8ee26575143cf56f7b4626ce53b

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
<<<<<<< HEAD
import { CoreModule } from './core/core.module';
import { HomeModule } from './home/home.module';
import { AboutComponent } from './about/about.component';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { NgxSpinnerModule } from 'ngx-spinner';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';

@NgModule({
  declarations: [AppComponent, AboutComponent],
=======
import { NavbarComponent } from './navbar/navbar.component';

@NgModule({
  declarations: [AppComponent, NavbarComponent],
>>>>>>> c3422c0709b2b8ee26575143cf56f7b4626ce53b
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    HttpClientModule,
<<<<<<< HEAD
    CoreModule,
    HomeModule,
    NgxSpinnerModule
  ],
  providers: [
    {provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
    {provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true}
  ],
=======
  ],
  providers: [],
>>>>>>> c3422c0709b2b8ee26575143cf56f7b4626ce53b
  bootstrap: [AppComponent],
})
export class AppModule {}
