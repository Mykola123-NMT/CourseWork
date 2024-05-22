import { Input, Select, Slider, Checkbox, Modal, Form } from 'antd';
import ProductCategory from '../models/ProductCategory';

interface FilterModalProps {
    isModalVisible: boolean;
    handleFilterChange: (filterName: string, value: string | number | boolean) => void;
    handleOk: () => void;
    handleCancel: () => void;
    categories: ProductCategory[];
}

const { Option } = Select;

export const FilterModalComponent = ({ isModalVisible, handleFilterChange, handleOk, handleCancel, categories }: FilterModalProps) => {

    return (
        <Modal title="Filter Products" visible={isModalVisible} onOk={handleOk} onCancel={handleCancel}>
            <Form layout="vertical">
                <Form.Item label="Category">
                    <Select
                        placeholder="Select category"
                        onChange={value => handleFilterChange('category', value)}
                    >
                        {categories.map(c => (
                            <Option value={c.id}>{c.categoryName}</Option>
                        ))}
                        <Option value="electronics">Electronics</Option>
                        <Option value="books">Books</Option>
                    </Select>
                </Form.Item>
                <Form.Item label="Price Range">
                    <Slider
                        range
                        min={0}
                        max={1000}
                        onChange={value => {
                            handleFilterChange('minPrice', value[0]);
                            handleFilterChange('maxPrice', value[1]);
                        }}
                        tipFormatter={value => `$${value}`}
                    />
                </Form.Item>
                <Form.Item>
                    <Checkbox
                        onChange={e => handleFilterChange('discounted', e.target.checked)}
                    >
                        Discounted
                    </Checkbox>
                </Form.Item>
                <Form.Item label="Minimum Reviews">
                    <Input
                        placeholder="Minimum Reviews"
                        type="number"
                        onChange={e => handleFilterChange('minReviews', e.target.value)}
                    />
                </Form.Item>
                <Form.Item label="Minimum Rating">
                    <Slider
                        min={0}
                        max={5}
                        step={0.1}
                        onChange={value => handleFilterChange('minRating', value)}
                        tipFormatter={value => `${value} stars`}
                    />
                </Form.Item>
                <Form.Item label="Minimum Sold Last Month">
                    <Input
                        placeholder="Minimum Sold Last Month"
                        type="number"
                        onChange={e => handleFilterChange('minSoldLastMonth', e.target.value)}
                    />
                </Form.Item>
                <Form.Item label="Sort By">
                    <Select
                        placeholder="Sort by"
                        onChange={value => handleFilterChange('sortBy', value)}
                    >
                        <Option value="price">Price</Option>
                        <Option value="discountedprice">Discounted Price</Option>
                        <Option value="reviews">Reviews</Option>
                        <Option value="rating">Rating</Option>
                        <Option value="soldlastmonth">Sold Last Month</Option>
                    </Select>
                </Form.Item>
                <Form.Item label="Sort Order">
                    <Select
                        placeholder="Sort order"
                        onChange={value => handleFilterChange('sortOrder', value)}
                    >
                        <Option value="asc">Ascending</Option>
                        <Option value="desc">Descending</Option>
                    </Select>
                </Form.Item>
            </Form>
        </Modal>
    )
}