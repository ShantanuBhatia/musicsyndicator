import { createContext, useContext, useState, useEffect } from 'react';
import { Navigate } from 'react-router-dom';
import { authApi } from '../services/apiService';

const UserContext = createContext();

export const UserProvider = ({ children }) => {
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
                setUser({
                    isAuthenticated: false,
                    name: null
                });
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
        } catch (error) {
            console.error('Error logging out:', error);
        } finally {
            setUser({ isAuthenticated: false });
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