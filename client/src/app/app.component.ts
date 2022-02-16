<<<<<<< HEAD
import { Component, OnInit } from '@angular/core';
import { BasketService } from './basket/basket.service';
=======
import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { IPagination } from './models/pagination';
import { IProduct } from './models/product';
>>>>>>> c3422c0709b2b8ee26575143cf56f7b4626ce53b

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Circuitry';
<<<<<<< HEAD

  constructor(private basketService: BasketService) {}

  ngOnInit(): void {
    const basketId = localStorage.getItem('basket_id');
    if (basketId) {
      this.basketService.getBasket(basketId).subscribe(
        () => {
          console.log('initialized basket');
        },
        (error) => {
          console.log(error);
        }
      );
    }
=======
  products: IProduct[];

  constructor(private http: HttpClient) {}

  ngOnInit(): void {
    this.http.get('https://localhost:5001/api/products?pageSize=50').subscribe((response: IPagination) => {
      this.products = response.data;
    }, error => {
      console.log(error);
    });
>>>>>>> c3422c0709b2b8ee26575143cf56f7b4626ce53b
  }
}
