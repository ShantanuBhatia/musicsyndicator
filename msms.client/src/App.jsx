import { useEffect, useState } from 'react';
import axios from 'axios';
import { BrowserRouter as Router, Route, Routes, useLocation } from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar';
import Home from './components/Home';
import Search from './components/Search';

function App() {
    const [user, setUser] = useState(null);


    useEffect(() => {
        fetchUser();
    }, []);

    const fetchUser = async () => {
        try {
            const response = await axios.get('/api/auth/user', { withCredentials: true });
            setUser(response.data);
        } catch (error) {
            console.error('Error fetching user:', error);
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

    return (
        <Router>
            <Navbar name={"MSMS"} />
            <Routes>
                <Route exact path="/" element={<Home user={user} logoutCallback={handleLogout} />} />
                <Route path="/search" element={<Search user={user} />} />
            </Routes>
        </Router>
    );
    


    
}

export default App;