export interface IProduct {
    productId: number;
    productName: string;
    stockId?: number;
    stockQuantity?: number;
}

export class Product implements IProduct {
    productId: number;
    productName: string;
    stockId?: number;
    stockQuantity?: number;
    editingProductName = false;
    editingStockQuantity = false;
    #tempProductName?: string;
    #tempStockQuantity?: number;

    constructor(iProduct?: IProduct) {
        this.productId = iProduct?.productId;
        this.productName = iProduct?.productName;
        this.stockId = iProduct?.stockId;
        this.stockQuantity = iProduct?.stockQuantity;
    }

    public static fromList(iProducts: IProduct[]): Product[] {
        return iProducts.map(iProduct => new Product(iProduct));
    }

    public startEdit(): void {
        this.#tempProductName = this.productName;
        this.#tempStockQuantity = this.stockQuantity;
        this.editingProductName = true;
        this.editingStockQuantity = true;
    }

    public confirmEditProductName(): void {
        this.#tempProductName = this.productName;
        this.editingProductName = false;
    }

    public confirmEditStockQuantity(): void {
        this.#tempStockQuantity = this.stockQuantity;
        this.editingStockQuantity = false;
    }

    public cancelEdit(): void {
        this.productName = this.#tempProductName;
        this.stockQuantity = this.#tempStockQuantity;
        this.editingProductName = false;
        this.editingStockQuantity = false;
    }

    public wasProductNameChanged(): boolean {
        return this.#tempProductName !== this.productName;
    }

    public wasStockQuantityChanged(): boolean {
        return this.#tempStockQuantity !== this.stockQuantity;
    }

    public updateProductName(productName: string): void {
        this.productName = productName;
    }

    public updateStockQuantity(stockQuantity: number): void {
        this.stockQuantity = stockQuantity;
    }
}
