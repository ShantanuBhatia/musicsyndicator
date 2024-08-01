import { useEffect, useState } from 'react';
import axios from 'axios';
import { BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar';
import Home from './components/Home';
import Search from './components/Search';
import CreateNewList from './components/CreateNewList';

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
            <Navbar />
            <div className="relative flex size-full min-h-screen flex-col bg-[#111813] dark group/design-root overflow-x-hidden "
                style={{
                    fontFamily: ["Plus Jakarta Sans", "Noto Sans", "sans-serif"]

                }} >
            
            <Routes>
                <Route exact path="/" element={<Home user={user} logoutCallback={handleLogout} />} />
                <Route path="/search" element={<Search user={user} />} />
                <Route path="/create" element={<CreateNewList user={user} /> } />
                </Routes>
            </div>
        </Router>
    );
    


    
}

export default App;