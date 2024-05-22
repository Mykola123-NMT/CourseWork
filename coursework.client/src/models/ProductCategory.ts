import Product from "./Product";

export default class ProductCategory {
    id: number;
    categoryName: string;
    products: Product[];

    constructor() {
        this.id = 0;
        this.categoryName = "";
        this.products = [];
    }
}