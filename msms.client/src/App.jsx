import { useEffect, useState } from 'react';
import axios from 'axios';
import { BrowserRouter as Router, Route, Routes, Link } from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar';

function App() {
    const [user, setUser] = useState(null);

    useEffect(() => {
        fetchUser();
    }, []);

    const fetchUser = async () => {
        try {
            const response = await axios.get('/api/auth/user');
            setUser(response.data);
        } catch (error) {
            console.error('Error fetching user:', error);
        }
    };

    return (
        <Router>
            <Navbar name={"MSMS"} />
            <Routes>
                <Route path="/" element={<h1>This is the home paaaaaaaaaaage</h1>} />
                <Route path="/login" element={<h1>This is the login page</h1>} />
                <Route path="/about" element={<h1>This is the about page</h1>} />
            </Routes>
        </Router>
    );
    


    
}

export default App;