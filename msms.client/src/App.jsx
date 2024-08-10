import { BrowserRouter as Router, Route, Routes} from 'react-router-dom';
import './App.css';
import { UserProvider, useUser, PrivateRoute } from './hooks/UserContext'
import NavigationBar from './components/NavigationBar';
import Home from './components/Home';
import Search from './components/Search';
import CreateNewList from './components/CreateNewList';
import NotFound from './components/NotFound';
import MobileDetection from './hooks/MobileDetection';

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

    const isMobile = MobileDetection();

    if (loading) {
        return (<div className="bg-[#111813] dark group/design-root"></div>);
    }
    return (
        <div className="flex flex-col size-full min-h-svh">
            <NavigationBar user={user} logoutCallback={handleLogout} isMobile={isMobile} />
            <div
                className="relative flex flex-1 flex-col bg-[#111813] dark group/design-root "
                style={{
                    fontFamily: ["Plus Jakarta Sans", "Noto Sans", "sans-serif"]

                }}
            >
                <Routes>
                    <Route exact path="/" element={<Home user={user} isMobile={isMobile} />} />
                    <Route path="/search" element={<Search user={user} isMobile={isMobile} />} />
                    <Route path="/create" element={
                        <PrivateRoute>
                            <CreateNewList user={user} isMobile={isMobile} />
                        </PrivateRoute>
                    } />
                    <Route path='*' element={<NotFound />} />
                </Routes>
            </div>
        </div>
    )
}

export default App;