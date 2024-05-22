import ProductCategory from '../models/ProductCategory';
import api from './api';
import handleError from './errorHandler';

const getCategories = async () => {
    const response = await api.get("AmazonCategories")
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const getCategory = async (id: number) => {
    const response = await api.get(`AmazonCategories/${id}`)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const putCategory = async (id: number, category: ProductCategory) => {
    const response = await api.put(`AmazonCategories/${id}`, category)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
        });
    return response;
}

const postCategory = async (category: ProductCategory) => {
    const response = await api.post("AmazonCategories", category)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
        });
    return response;
}

const deleteCategory = async (id: number) => {
    const response = await api.remove(`AmazonCategories/${id}`)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
        });
    return response;
}

export default { getCategories, getCategory, putCategory, postCategory, deleteCategory }