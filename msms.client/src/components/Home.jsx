import { Link } from 'react-router-dom';

const Home = ({ user }) => {
    return (
        <div>
            <h1>Home</h1>
            {user ? (
                <div>
                    <p>Welcome, {user.username}!</p>
                    <Link to="/search">Go to Search</Link>
                </div>
            ) : (
                <a href="/api/auth/login">Log in with Spotify</a>
            )}
        </div>
    );
};

export default Home;