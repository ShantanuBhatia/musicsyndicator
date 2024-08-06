import { useEffect, useState } from 'react';
import { artistListApi } from '../services/apiService';

import ArtistListList from './ArtistListList';

const Home = ({ user, logoutCallback }) => {

    const [artistLists, setArtistLists] = useState([]);

    const handleLogin = () => {
        window.location.href = 'https://localhost:7183/api/auth/login';
    };

    const fetchArtistLists = async () => {
        if (user?.isAuthenticated) {
            const myArtistLists = await artistListApi.getAll();
            setArtistLists(myArtistLists);
        }
    }

    useEffect(() => {
        fetchArtistLists();
    }, [user?.isAuthenticated])

    return (
        <div>
            {user?.isAuthenticated ? (
                <div>
                    <h1>Welcome, {user?.name}!</h1>
                    <h2>Your Artist Lists:</h2>
                    {artistLists.length > 0 ? <ArtistListList artlistLists={artistLists} refreshArtistLists={fetchArtistLists} />: <></>}
                    <button onClick={logoutCallback}>Log Out</button>
                </div>
            ) : (
                    <button onClick={handleLogin}>Log in with Spotify</button>
            )}
        </div>
    );
};

export default Home;