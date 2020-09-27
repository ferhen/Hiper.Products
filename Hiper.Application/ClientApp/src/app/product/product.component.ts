// core
import { Component, OnDestroy, OnInit } from '@angular/core';
import { BehaviorSubject, Subject } from 'rxjs';
import { takeUntil } from 'rxjs/operators';

// imports
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { NotifierService } from 'angular-notifier';

// services
import { ProductService } from './product.service';
import { StockService } from './stock.service';

// models
import { Product } from './product.model';

// modals
import { CreateProductModalComponent } from './create-product-modal/create-product-modal.component';
import { DeleteProductModalComponent } from './delete-product-modal/delete-product-modal.component';

@Component({
    templateUrl: './product.component.html',
    styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit, OnDestroy {
    public products = new BehaviorSubject<Product[]>([]);
    private readonly destroy$ = new Subject();

    constructor(private readonly service: ProductService,
        private readonly stockService: StockService,
        private readonly modalService: NgbModal,
        private readonly notifier: NotifierService) { }

    public create(): void {
        this.modalService.open(CreateProductModalComponent).result.then(productName => {
            this.service.createProduct(productName)
                .pipe(takeUntil(this.destroy$))
                .subscribe(
                    product => this.products.next(
                        [...this.products.getValue(), product]
                    ),
                    err => this.notifier.notify('error', err.error.message)
                );
        }, () => { });
    }

    public edit(product: Product): void {
        product.startEdit();
    }

    public confirmEdit(product: Product): void {
        if (product.wasProductNameChanged()) {
            this.service.editProduct(product)
                .pipe(takeUntil(this.destroy$))
                .subscribe(
                    x => this.products.next((() => {
                        const products = this.products.getValue();
                        const changedProduct = products.find(y => y.productName === x.productName);
                        changedProduct.updateProductName(x.productName);
                        product.confirmEditProductName();
                        return products;
                    })()),
                    err => this.notifier.notify('error', err.error.message)
                );
        }
        if (product.wasStockQuantityChanged()) {
            this.stockService.editStock( { stockId: product.stockId, stockQuantity: product.stockQuantity })
                .pipe(takeUntil(this.destroy$))
                .subscribe(
                    x => this.products.next((() => {
                        const products = this.products.getValue();
                        const changedProduct = products.find(y => y.stockId === x.stockId);
                        changedProduct.updateStockQuantity(x.stockQuantity);
                        product.confirmEditStockQuantity();
                        return products;
                    })()),
                    err => this.notifier.notify('error', err.error.message)
                );
        }
        if (!product.wasProductNameChanged()) {
            product.confirmEditProductName();
        }
        if (!product.wasStockQuantityChanged()) {
            product.confirmEditStockQuantity();
        }
        if (!product.wasProductNameChanged() && !product.wasStockQuantityChanged()) {
            product.cancelEdit();
        }
    }

    public cancelEdit(product: Product): void {
        product.cancelEdit();
    }

    public remove(product: Product): void {
        this.modalService.open(DeleteProductModalComponent, { size: 'sm' }).result.then(() => {
            this.service.deleteProduct(product.productId)
                .pipe(takeUntil(this.destroy$))
                .subscribe(
                    () => this.products.next(
                        this.products.getValue().filter(x => x.productId !== product.productId)
                    ),
                    err => this.notifier.notify('error', err.error.message)
                );
        }, () => { });
    }

    private getProducts(): void {
        this.service.getProducts()
            .pipe(takeUntil(this.destroy$))
            .subscribe(products => this.products.next(products));
    }

    ngOnInit(): void {
        this.getProducts();
    }

    ngOnDestroy(): void {
        this.destroy$.next();
        this.destroy$.complete();
    }
}
