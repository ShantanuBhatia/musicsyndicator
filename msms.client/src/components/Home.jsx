import { Link } from 'react-router-dom';

const Home = ({ user, logoutCallback }) => {

    const handleLogin = () => {
        window.location.href = 'https://localhost:7183/api/auth/login';
    };


    return (
        <div>
            <h1>Home</h1>
            {user?.isAuthenticated ? (
                <div>
                    <p>Welcome, {user?.name}!</p>
                    <Link to="/search">Go to Search</Link>
                    <button onClick={logoutCallback}>Log Out</button>
                </div>
            ) : (
                    <button onClick={handleLogin}>Log in with Spotify</button>
            )}
        </div>
    );
};

export default Home;