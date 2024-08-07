import axios from 'axios';


const api = axios.create({
    baseURL: import.meta.env.REACT_APP_API_URL || 'https://localhost:5173',
    withCredentials: true,
});

api.interceptors.response.use(
    (response) => response,
    (error) => {
        if (error.response && error.response.status === 401) {
            window.location.href = `${import.meta.env.REACT_APP_API_URL || "https://localhost:7183"}/api/auth/login`;
        }
        return Promise.reject(error);
    }
);

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
            console.log("Firing search!")
            const response = await await api.get(`/api/search/artists?query=${artistName}`, {
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
