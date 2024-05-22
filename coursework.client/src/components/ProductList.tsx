import { useState, useEffect } from 'react';
import { Row, Col, Button } from 'antd';
import Product from '../models/Product';
import { ProductComponent } from './Product';
import '../styles.css';
import productApi from "../api/productApi";
import Filter from '../models/Filter';
import { FilterModalComponent } from './FilterModal';
import ProductCategory from '../models/ProductCategory';
import categoryApi from '../api/categoryApi';


const ProductsListComponent = () => {
    const [products, setProducts] = useState<Product[]>([]);
    const [categories, setCategories] = useState<ProductCategory[]>([]);
    const [filters, setFilters] = useState<Filter>(new Filter());
    const [hasMore, setHasMore] = useState(true);
    const [isModalVisible, setIsModalVisible] = useState(false);

    useEffect(() => {
        loadCategories();
    }, []);

    useEffect(() => {
        loadProducts();
    }, [filters]);

    const loadProducts = async () => {
        try {
            const amazonProducts = await productApi.getProductsPerPage(filters);
            if (filters.pageNumber === 1) {
                setProducts(amazonProducts);
            } else {
                setProducts(prevProducts => [...prevProducts, ...amazonProducts]);
            }
            if (amazonProducts.length === 0 || amazonProducts.length < 20) {
                setHasMore(false);
            } else {
                setHasMore(true);
            }
        } catch (error) {
            console.error('Error loading products:', error);
        }
    };

    const loadCategories = async () => {
        try {
            const amazonCategories = await categoryApi.getCategories();
            setCategories(amazonCategories);
        } catch (error) {
            console.error('Error loading products:', error);
        }
    };

    const loadMore = () => {
        setFilters(prevFilters => ({
            ...prevFilters,
            pageNumber: prevFilters.pageNumber + 1
        }));
    };

    const handleFilterChange = (filterName: string, value: string | number | boolean) => {
        setFilters(prevFilters => ({
            ...prevFilters,
            [filterName]: value,
            pageNumber: 1
        }));
    };

    const showModal = () => {
        setIsModalVisible(true);
    };

    const handleOk = () => {
        setIsModalVisible(false);
        loadProducts();
    };

    const handleCancel = () => {
        setIsModalVisible(false);
    };

    return (
        <div className="products-list">
            <div style={{ textAlign: 'center', margin: '20px 0' }}>
                <Button type="primary" onClick={showModal}>
                    Filter Products
                </Button>
            </div>
            <FilterModalComponent isModalVisible={isModalVisible}
                handleFilterChange={handleFilterChange}
                handleOk={handleOk}
                handleCancel={handleCancel}
                categories={categories}
            />
            <Row gutter={[16, 16]}>
                {products.map(product => (
                    <Col key={product.asin} xs={24} sm={12} md={8} lg={6}>
                        <ProductComponent product={product} />
                    </Col>
                ))}
            </Row>
            {hasMore && (
                <div style={{ textAlign: 'center', margin: '20px 0' }}>
                    <Button className="load-more-button" onClick={loadMore} type="primary">Load More</Button>
                </div>
            )}
        </div>
    );
};

export default ProductsListComponent;
