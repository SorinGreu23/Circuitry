import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
<<<<<<< HEAD
import { AboutComponent } from './about/about.component';
import { NotFoundComponent } from './core/not-found/not-found.component';
import { ServerErrorComponent } from './core/server-error/server-error.component';
import { TestErrorComponent } from './core/test-error/test-error.component';
import { HomeComponent } from './home/home.component';

const routes: Routes = [
  {path: '', component: HomeComponent, data: {breadcrumb: "Acasa"}},
  {path: 'about', component: AboutComponent, data: {breadcrumb: "Despre"}},
  {path: 'test-error', component: TestErrorComponent, data: {breadcrumb: "Test Errors"}},
  {path: 'server-error', component: ServerErrorComponent, data: {breadcrumb: "Server Error"}},
  {path: 'not-found', component: NotFoundComponent, data: {breadcrumb: "Not Found"}},
  {path: 'shop', loadChildren: () => import('./shop/shop.module').then(mod => mod.ShopModule), data: {breadcrumb: "Shop"}},
  {path: 'basket', loadChildren: () => import('./basket/basket.module').then(mod => mod.BasketModule), data: {breadcrumb: "Cosul tau"}},
  {path: 'checkout', loadChildren: () => import('./checkout/checkout.module').then(mod => mod.CheckoutModule), data: {breadcrumb: "Checkout"}},
  {path: '**', redirectTo: 'not-found', pathMatch: 'full'}
];
=======

const routes: Routes = [];
>>>>>>> c3422c0709b2b8ee26575143cf56f7b4626ce53b

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
