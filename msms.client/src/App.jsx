//import { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import Navbar from './components/Navbar';

function App() {

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