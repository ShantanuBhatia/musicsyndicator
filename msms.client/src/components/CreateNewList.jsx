import { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { artistListApi } from '../services/apiService';
import Search from "./Search";

const CreateNewList = ({ user }) => {
    const [selectedArtists, setSelectedArtists] = useState([]);
    
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const location = useLocation();
    const [listTitle, setListTitle] = useState(location.state ? location.state.lineupName : "");


    const handleLogin = () => {
        window.location.href = 'https://localhost:7183/api/auth/login';
    };


    //// Select/deselect artist when clicked
    const handleArtistClick = (clickedArtist) => {
        console.log(`Clicked on ${clickedArtist.name}`)
        setSelectedArtists(prevArtists =>
            prevArtists.some(artist => artist.id === clickedArtist.id)
                ? prevArtists.filter(artist => artist.id !== clickedArtist.id)
                : [...prevArtists, clickedArtist]
        );
    }

    const createArtistList = async (e) => {
        e.preventDefault();
        setIsSubmitting(true);
        setError(null);


        try {
            const submitdata = {
                artistListName: listTitle,
                artistIds: selectedArtists.map((artist) => artist.id)
            };
            console.log(JSON.stringify(submitdata));
            await artistListApi.create(submitdata);
            navigate("/");
        }
        catch (err) {
            setError('Failed to create artist list. Please try again.');
            console.error('Error creating artist list:', err);
        }
        finally {
            setIsSubmitting(false);
        }

        
    }

    return (
        <div>{
            user?.isAuthenticated ?
                <>
                    <h1>Create new Artist List</h1>
                    <form onSubmit={createArtistList}>
                        <input
                            type="text"
                            value={listTitle}
                            onChange={(e) => setListTitle(e.target.value)}
                            placeholder="Enter List name"
                        />
                        <button type="submit" disabled={isSubmitting}> {isSubmitting ? 'Creating...' : 'Save Artist List'}</button>
                        {error && <p style={{ color: 'red' }}>{error}</p>}
                    </form>
                    <p>Selected Artists:</p>
                    <ul>
                        {selectedArtists && selectedArtists.map((artist) => <li key={artist.id}>{artist.name}</li>)}
                    </ul>
                    <Search handleArtistClick={handleArtistClick} selectedArtists={selectedArtists} />
                </>
            :
                (<button onClick={handleLogin}>Log in with Spotify</button>)
        }</div>
    );
}

export default CreateNewList;