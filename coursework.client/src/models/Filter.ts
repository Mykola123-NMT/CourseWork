export default class Filter {
    pageNumber: number;
    pageSize: number;
    category: null;
    minPrice: null;
    maxPrice: null;
    discounted: null;
    minReviews: null;
    minRating: null;
    minSoldLastMonth: null;
    sortBy: string;
    sortOrder: string;

    constructor() {
        this.pageNumber = 1;
        this.pageSize = 20;
        this.category = null;
        this.minPrice = null;
        this.maxPrice = null;
        this.discounted = null;
        this.minReviews = null;
        this.minRating = null;
        this.minSoldLastMonth = null;
        this.sortBy = 'name';
        this.sortOrder = 'asc';
    }
}