// UserContext.js
import { createContext, useContext, useState, useEffect } from 'react';
import axios from 'axios';
import { authApi } from '../services/apiService';

const UserContext = createContext();

export function UserProvider({ children }) {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        fetchUser();
    }, []);

    const fetchUser = async () => {
        try {
            const response = await axios.get('/api/auth/user', { withCredentials: true });
            setUser(response.data);
        } catch (error) {
            console.error('Error fetching user:', error);
        } finally {
            setLoading(false);
        }
    };

    const handleLogout = async () => {
        try {
            await axios.post('/api/auth/logout', {}, { withCredentials: true });
            setUser({ isAuthenticated: false });
        } catch (error) {
            console.error('Error logging out:', error);
        }
    };

    const value = {
        user,
        setUser,
        handleLogout,
        loading
    };

    return <UserContext.Provider value={value}>{children}</UserContext.Provider>;
}

export const useUser = () => {
    const context = useContext(UserContext);
    if (context === undefined) {
        throw new Error('useUser must be used within a UserProvider');
    }
    return context;
};