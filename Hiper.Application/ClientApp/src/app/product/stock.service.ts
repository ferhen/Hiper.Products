// core
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

// services
import { AppConfig } from '../app-config/app-config';

// models
import { IStock } from './stock.model';

@Injectable({
    providedIn: 'root'
})
export class StockService {
    constructor(private readonly http: HttpClient) { }

    public editStock(stock: IStock): Observable<IStock> {
        return this.http.put<IStock>(AppConfig.getPath('api', 'stock', 'update'), stock);
    }
}
