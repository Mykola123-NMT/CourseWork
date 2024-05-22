import Filter from '../models/Filter';
import Product from '../models/Product';
import api from './api';
import handleError from './errorHandler';


const getProducts = async () => {
    const response = await api.get("AmazonProducts")
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const getProduct = async (id: string) => {
    const response = await api.get(`AmazonProducts/${id}`)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const getProductsPerPage = async (filter: Filter) => {
    const response = await api.get(`AmazonProducts/PerPage/`, filter)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const getBestSeller = async (id: string) => {
    const response = await api.get(`AmazonProducts/BestSellerAnalizer/${id}`)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
            return null;
        });
    return response;
}

const putProduct = async (id: string, product: Product) => {
    const response = await api.put(`AmazonProducts/${id}`, product)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
        });
    return response;
}

const postProduct = async (product: Product) => {
    const response = await api.post("AmazonProducts", product)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
        });
    return response;
}

const deleteProduct = async (id: string) => {
    const response = await api.remove(`AmazonProducts/${id}`)
        .then((response) => {
            return response.data;
        })
        .catch((error) => {
            handleError(error);
        });
    return response;
}

export default { getProduct, getProducts, getProductsPerPage, getBestSeller, putProduct, postProduct, deleteProduct }