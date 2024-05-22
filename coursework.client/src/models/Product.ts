import ProductCategory from "./ProductCategory";

export default class Product {
    asin: string;
    title: string;
    imgUrl: string;
    productUrl: string;
    stars: number;
    reviews: number;
    price: number;
    listPrice: number;
    categoryId: number;
    isBestSeller: boolean;
    boughtLastMonth: number;
    category: ProductCategory;

    constructor() {
        this.asin = "";
        this.title = "";
        this.imgUrl = "";
        this.productUrl = "";
        this.stars = 0;
        this.reviews = 0;
        this.price = 0;
        this.listPrice = 0;
        this.categoryId = 0;
        this.isBestSeller = false;
        this.boughtLastMonth = 0;
        this.category = new ProductCategory();
    }
}