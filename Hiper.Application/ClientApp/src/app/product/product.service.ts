// core
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

// services
import { AppConfig } from '../app-config/app-config';

// models
import { IProduct, Product } from './product.model';

@Injectable({
    providedIn: 'root'
})
export class ProductService {
    constructor(private readonly http: HttpClient) { }

    public getProducts(): Observable<Product[]> {
        return this.http.get<IProduct[]>(AppConfig.getPath('api', 'product', 'list')).pipe(
            map(iProducts => Product.fromList(iProducts))
        );
    }

    public createProduct(productName: string): Observable<Product> {
        return this.http.post<IProduct>(
            AppConfig.getPath('api', 'product', 'create'),
            { productName }
        ).pipe(
            map(iProduct => new Product(iProduct))
        );
    }

    public editProduct(product: IProduct): Observable<Product> {
        return this.http.put<IProduct>(AppConfig.getPath('api', 'product', 'update'), product).pipe(
            map(iProduct => new Product(iProduct))
        );
    }

    public deleteProduct(productId: number): Observable<number> {
        return this.http.delete<number>(AppConfig.getPath('api', 'product', 'delete') + productId);
    }
}
