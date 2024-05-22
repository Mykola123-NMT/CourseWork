import axios from "axios";
import { API_URL } from '../config';

const get = async (
    url: string,
    filters?: unknown
) => {
    const response = await axios.get(API_URL + url, { params: filters });
    return response;
}

const post = async (
    url: string,
    data?: unknown
) => {
    const response = await axios.post(API_URL + url, data);
    return response;
}

const put = async (
    url: string,
    data?: unknown
) => {
    const response = await axios.put(API_URL + url, data);
    return response;
}

const remove = async (
    url: string
) => {
    const response = await axios.delete(API_URL + url);
    return response;
}

export default { get, post, put, remove };