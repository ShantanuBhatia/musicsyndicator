import { useState } from 'react';
import axios from 'axios';

const Search = ({ user }) => {
    const [query, setQuery] = useState('');
    const [results, setResults] = useState([]);

    const handleSearch = async (e) => {
        e.preventDefault();
        if (!user) return;

        try {
            const response = await axios.get(`/api/search/artists?query=${query}`);
            setResults(response.data);
        } catch (error) {
            console.error('Error searching artists:', error);
        }
    };

    if (!user) {
        return <p>Not logged in, sorry.</p>;
    }

    return (
        <div>
            <h1>Search Artists</h1>
            <form onSubmit={handleSearch}>
                <input
                    type="text"
                    value={query}
                    onChange={(e) => setQuery(e.target.value)}
                    placeholder="Enter artist name"
                />
                <button type="submit">Search</button>
            </form>
            <ul>
                {results.map((artist) => (
                    <li key={artist.id}>{artist.name}</li>
                ))}
            </ul>
        </div>
    );
};

export default Search;