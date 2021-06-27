import axios, { AxiosResponse } from 'axios'
import toast from 'react-hot-toast';
import { TaxFileNumber } from '../models/TaxFileNumber';

axios.defaults.baseURL = "http://localhost:5000/";

const responseBody = function (response: AxiosResponse) { 
    return response.data; }

    axios.interceptors.response.use(undefined, (error) => {
        if (error.message === 'Network Error' ) {
          toast.error('Network error - make sure API is running');
        }
        const { status } = error.response;
        if (status === 404) {
          toast.error('Network error - request not found');
        }
        if (status === 500) {
          toast.error('Server error - check the terminal for more info!');
        }
        throw error.response;
    });

const requests = {
    get: (url: string) => axios.get(url).then(responseBody),
    post: (url: string, body: {}) => axios.post(url, body).then(responseBody)
}

export const TFN = {
    validate: (taxFileNumber: TaxFileNumber) => requests.post(`/TFN`, taxFileNumber)
}