import axios from 'axios';

// Create an axios instance with a base URL
//    baseURL: process.env.REACT_APP_API_URL || 'http://localhost:5173',

const api = axios.create({
    baseURL: 'https://localhost:5173',
    withCredentials: true,
});

const handleApiError = (error) => {
    let errorMessage = 'An unexpected error occurred';
    if (error.response) {
        // The request was made and the server responded with a status code
        // that falls out of the range of 2xx
        errorMessage = error.response.data.message || `Error: ${error.response.status}`;
    } else if (error.request) {
        // The request was made but no response was received
        errorMessage = 'No response received from server';
    } else {
        // Something happened in setting up the request that triggered an Error
        errorMessage = error.message;
    }
    throw new Error(errorMessage);
};

export const authApi = {
    login: async () => {
        try {
            const response = await axios.get('/api/auth/user', { withCredentials: true });
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
    logout: async () => {
        try {
            const response = axios.post('/api/auth/logout', {}, { withCredentials: true });
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
    getUserInfo: async () => {
        try {
            const response = await axios.get('/api/auth/user', { withCredentials: true });
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
};

export const artistListApi = {
    getAll: async () => {
        try {
            const response = await api.get('/api/artistlists');
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
    create: async (data) => {
        try {
            const response = await api.post('/api/artistlists', data);
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
    getById: async (id) => {
        try {
            const response = await api.get(`/api/artistlists/${id}`);
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
};

export const playlistApi = {
    getById: async (id) => {
        try {
            const response = await api.get(`/api/playlists/${id}`);
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
    create: async (data) => {
        try {
            const response = await api.post('/api/playlists/createplaylist', data);
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
};

export const artistSearchApi = {
    searchByName: async (artistName, cancellationToken) => {
        try {
            const response = await await axios.get(`/api/search/artists?query=${artistName}`, {
                cancelToken: cancellationToken
            });
            return response.data;
        } catch (error) {
            throw handleApiError(error);
        }
    },
};

export default {
    authApi,
    artistListApi,
    playlistApi,
    artistSearchApi
};
