import { useEffect, useState } from 'react';
import axios from 'axios';
import { BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import './App.css';
import { authApi } from './services/apiService'
import { UserProvider, useUser} from './hooks/UserContext'
import Navbar from './components/Navbar';
import Home from './components/Home';
import Search from './components/Search';
import CreateNewList from './components/CreateNewList';

const App = () => { 

    return (
        <UserProvider>
            <Router>
                <AppContent />
            </Router>
        </UserProvider>
    );
}

const AppContent = () => {
    const { user, handleLogout, loading } = useUser();

    if (loading) {
        return <div>Loading...</div>;
    }
    return (
        <>
            <Navbar user={user} logoutCallback={handleLogout} />
            <div
                className="relative flex size-full min-h-screen flex-col bg-[#111813] dark group/design-root overflow-x-hidden "
                style={{
                    fontFamily: ["Plus Jakarta Sans", "Noto Sans", "sans-serif"]

                }}
            >
                <Routes>
                    <Route exact path="/" element={<Home user={user} />} />
                    <Route path="/search" element={<Search user={user} />} />
                    <Route path="/create" element={<CreateNewList user={user} />} />
                </Routes>
            </div>
        </>
    )
}

export default App;