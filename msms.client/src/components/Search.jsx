import { useState } from 'react';
import axios from 'axios';
import ArtistPreview from './ArtistPreview';


const Search = ({ handleArtistClick, selectedArtists }) => {
    const [query, setQuery] = useState('');
    const [results, setResults] = useState([]);

    const handleSearch = async (e) => {
        e.preventDefault();
        try {
            const response = await axios.get(`/api/search/artists?query=${query}`);
            setResults(response.data);
        } catch (error) {
            console.error('Error searching artists:', error);
        }
    };


    return (
        <div>
            <h3>Search Artists</h3>
            <form onSubmit={handleSearch}>
                <input
                    type="text"
                    value={query}
                    onChange={(e) => setQuery(e.target.value)}
                    placeholder="Enter artist name"
                />
                <button type="submit">Search</button>
            </form>

                {results && results.map((artist) => (
                    <ArtistPreview key={artist.id} artist={artist} isSelected={selectedArtists.some(a => a.id === artist.id)} onClick={() => handleArtistClick(artist)} />
                ))}

        </div>
    );
};

export default Search;