import { createContext, useContext, useState, useEffect } from 'react';
import { Route, Navigate } from 'react-router-dom';
import { authApi } from '../services/apiService';

const UserContext = createContext();

export function UserProvider({ children }) {
    const [user, setUser] = useState(null);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        
        let ignore = false;

        const fetchUser = async () => {
            try {
                const response = await authApi.getUserInfo();
                if (!ignore) {
                    setUser(response);
                }
            } catch (error) {
                console.error('Error fetching user:', error);
            } finally {
                if (!ignore) setLoading(false);
            }
        };


        fetchUser();

        return (() => {
            ignore = true;
        })
    }, []);

    

    const handleLogout = async () => {
        try {
            await authApi.logout();
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

export const PrivateRoute = ({ children }) => {
    const { user } = useUser();

    if (!user || !user.isAuthenticated) {
        return <Navigate to="/404" replace />;
    }

    return children;
};


export const useUser = () => {
    const context = useContext(UserContext);
    if (context === undefined) {
        throw new Error('useUser must be used within a UserProvider');
    }
    return context;
};