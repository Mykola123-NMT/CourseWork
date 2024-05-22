import { useState } from 'react';
import { Card, Rate, Tag, Button, Tooltip, Badge, Progress } from 'antd';
import { MessageOutlined } from '@ant-design/icons';
import ReactCardFlip from 'react-card-flip';
import Product from "../models/Product"
import '../styles.css';
import productApi from '../api/productApi';

interface ProductProps {
    product: Product
}

const { Meta } = Card;

export const ProductComponent = ({ product }: ProductProps) => {
    const [isFlipped, setIsFlipped] = useState(false);
    const [probability, setProbability] = useState<number | null>(null);

    const handleFlip = () => {
        setIsFlipped(!isFlipped);
    };

    const fetchProbability = async () => {
        try {
            const result = await productApi.getBestSeller(product.asin);
            console.log(result);
            setProbability(result.probability);
        } catch (error) {
            console.error('Error fetching Bestseller:', error);
        }
    };

    return (
        <ReactCardFlip isFlipped={isFlipped} flipDirection="horizontal">
            <div className="flip-card-front" onClick={handleFlip}>
                <Card
                    className="product-card"
                    hoverable
                    cover={
                        <img
                            alt={product.title}
                            src={product.imgUrl}
                            className="product-image"
                        />}
                    actions={[
                        <Button
                            className="view-on-amazon-button"
                            type="link"
                            href={product.productUrl}
                            target="_blank"
                            rel="noopener noreferrer"
                            onClick={(e) => e.stopPropagation()}>
                            View on Amazon
                        </Button>
                    ]}
                >
                    <Meta
                        title={product.title}
                        description={<div className="product-meta-description">{product.category.categoryName}</div>}
                        className="product-meta"
                    />
                    <div className="product-details">
                        <Rate allowHalf disabled value={product.stars} />
                        <Tooltip title={`${product.reviews} reviews`}>
                            <Badge count={product.reviews} overflowCount={999} style={{ backgroundColor: '#52c41a' }}>
                                <MessageOutlined style={{ fontSize: '20px', color: '#08c' }} />
                            </Badge>
                        </Tooltip>
                    </div>
                    {product.isBestSeller &&
                        <Tag color="gold" className="best-seller">
                            Best Seller
                        </Tag>
                    }
                    {product.listPrice && product.listPrice > product.price ? (
                        <div className="product-prices">
                            <span className="list-price">${product.listPrice?.toFixed(2)}</span>
                            <span className="current-price">${product.price?.toFixed(2)}</span>
                            <span className="save-amount">Save ${(product.listPrice - product.price)?.toFixed(2)}</span>
                        </div>
                    ) : (
                        <div className="product-prices">
                            <span className="current-price">${product.price?.toFixed(2)}</span>
                        </div>
                    )}
                </Card>
            </div>

            <div className="flip-card-back" onClick={handleFlip}>
                <Card className="product-card">
                    <Button
                        className="show-probability-button"
                        onClick={(e) => {
                            e.stopPropagation();
                            fetchProbability();
                        }}>
                        Evaluate Probability
                    </Button>
                    <div className="card-content">
                        {probability !== null && (
                                <div className="probability-display">
                                    <Progress
                                    type="circle"
                                    percent={Number((probability*100).toFixed(2))}
                                    strokeColor={{
                                        '0%': '#108ee9',
                                        '100%': '#87d068',
                                    }}
                                    trailColor="#f0f0f0"
                                    />
                                </div>
                        )}
                    </div>                    
                </Card>
            </div>
        </ReactCardFlip>
    )
}
