import { useState, useEffect } from 'react';
import { artistSearchApi } from '../services/apiService'
import axios from 'axios';
import ArtistPreview from './ArtistPreview';
import { useDebounce } from 'use-debounce';


const Search = ({ handleArtistClick, selectedArtists }) => {
    const [query, setQuery] = useState('');
    const [results, setResults] = useState([]);
    const [debouncedQuery] = useDebounce(query, 500);
    const [fillerText, setFillerText] = useState("Start typing to get results")



    useEffect(() => {
        const source = axios.CancelToken.source();

        const fetchResults = async () => {
            if (debouncedQuery) {
                try {
                    setFillerText("Searching...")
                    const response = await artistSearchApi.searchByName(debouncedQuery, source.token);
                    console.log(JSON.stringify(response))
                    setResults(response);
                    
                    setFillerText(response.length===0 ? "No results found" : "");
                }
                catch (error) {
                    if (axios.isCancel(source)) return;
                    setFillerText("Something went wrong. Try refreshing the page.")
                }

            } else {
                setFillerText("Start typing to get search results")
                setResults([]);
            }
        }

        fetchResults();

        return () => {
            source.cancel(
                "Canceled because of component unmounted or debounce Text changed"
            );
        };
    }, [debouncedQuery]);



    return (
        <div className="flex flex-col w-full mx-auto items-stretch gap-2">
            <input
                className="form-input text-white h-10  w-full text-lg bg-[#111813] border-b-2 border-[#19cc58] focus:outline-none"
                    type="text"
                    value={query}
                onChange={(e) => setQuery(e.target.value)}
                    placeholder="Search for artists on Spotify"
            />
            {!fillerText ? <div className="flex-row h-80 overflow-y-auto">
                {results && results.map((artist) => (
                    <ArtistPreview key={artist.id} artist={artist} isSearchItem={true}  isSelected={selectedArtists.some(a => a.id === artist.id)} onClick={() => { handleArtistClick(artist)} } />
                ))}
            </div> : <h2 className="text-[#9db8a7] text-base py-8">{fillerText}</h2>}

        </div>
    );
};

export default Search;