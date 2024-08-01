import { useEffect, useState } from 'react';
import axios from 'axios';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
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
            const response = await axios.get('/api/Auth/user');
            setUser(response.data);
        } catch (error) {
            console.error('Error fetching user:', error);
        }
    };

    return (
        <Router>
            <Navbar name={"MSMS"} />
            <Routes>
                <Route exact path="/" element={<Home user={user} />} />
                <Route path="/search" element={<Search user={user} />} />
            </Routes>
        </Router>
    );
    


    
}

export default App;