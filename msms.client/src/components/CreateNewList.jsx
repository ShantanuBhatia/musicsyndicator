import { useState, useCallback, useRef } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { artistListApi } from '../services/apiService';
import Search from "./Search";
import ArtistPreview from './ArtistPreview';
import FadeInSection from './FadeInSection';

const ArtistListTitleInput = ({titleValue, inputHandler}) => (
    <input
        placeholder="Enter name here"
        className="flex-grow text-white bg-transparent border-b-2 border-[#19cc58] focus:outline-none py-2 text-lg"
        key="list-title-input"
        value={titleValue}
        onChange={inputHandler}
    />
);


const CreateNewList = ({ user, isMobile }) => {
    const [selectedArtists, setSelectedArtists] = useState([]);
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const location = useLocation();
    const [listTitle, setListTitle] = useState(location.state?.lineupName || "");
    const [isSearchOpen, setIsSearchOpen] = useState(false);

    const handleArtistClick = (clickedArtist) => {
        setSelectedArtists(prevArtists =>
            prevArtists.some(artist => artist.id === clickedArtist.id)
                ? prevArtists.filter(artist => artist.id !== clickedArtist.id)
                : [...prevArtists, clickedArtist]
        );
    }

    const handleTitleInput = (e) => { e.preventDefault();  setListTitle(e.target.value) };

    const createArtistList = async (e) => {
        e.preventDefault();
        setIsSubmitting(true);
        setError(null);

        try {
            const submitdata = {
                artistListName: listTitle,
                artistIds: selectedArtists.map((artist) => artist.id)
            };
            await artistListApi.create(submitdata);
            navigate("/");
        } catch (err) {
            setError('Failed to create artist list. Please try again.');
            console.error('Error creating artist list:', err);
        } finally {
            setIsSubmitting(false);
        }
    }


    const MobileView = (
            <div className="flex flex-col h-screen bg-[#111813]">
                <div className="flex-1 overflow-y-auto px-4 py-5 pb-20">
                    <h1 className="text-white text-2xl font-bold mb-4">Your Lineup</h1>
                    <div className="flex items-center space-x-2 mb-4">
                        <ArtistListTitleInput key={"title-input"} titleValue={listTitle} inputHandler={handleTitleInput} />
                    </div>
                    {error && <p className="text-red-500 mb-4">{error}</p>}
                    <div className="mb-4">
                        {selectedArtists.length ?
                            selectedArtists.map((artist) => (
                                <ArtistPreview
                                    key={artist.id}
                                    artist={artist}
                                    isSearchItem={false}
                                    isSelected={true}
                                    onClick={() => handleArtistClick(artist)}
                                />
                            )) :
                            <p className="text-[#9db8a7] text-base py-8">
                                It&apos;s lonely in here...add some artists to get started
                            </p>
                        }
                    </div>
                    <button
                        className="w-full py-3 bg-[#19cc58] text-black font-bold rounded-lg mb-4"
                        onClick={() => setIsSearchOpen(true)}
                    >
                        Add Artists
                    </button>
                </div>
                <div className="sticky bottom-0 left-0 right-0 px-4 py-3 bg-[#111813]">
                    <button
                        className="w-full py-3 bg-[#19cc58] text-black font-bold rounded-lg"
                        disabled={isSubmitting}
                        onClick={createArtistList}
                    >
                    {isSubmitting ? "Saving..." : "Save Lineup"}
                    </button>
                </div>
                {isSearchOpen && (
                    <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center p-4 z-50">
                        <div className="bg-[#111813] w-full max-w-md p-4 rounded-lg">
                            <div className="flex items-center mb-4 relative">
                                <button
                                    className="absolute left-0 p-2 text-white hover:text-gray-700"
                                    onClick={() => setIsSearchOpen(false)}
                                >
                                    <span className="text-xl">&#10005;</span>
                                </button>
                                <h2 className="text-white text-xl font-bold w-full text-center">Artist Search</h2>
                            </div>
                            <Search selectedArtists={selectedArtists} handleArtistClick={handleArtistClick} />
                        </div>
                    </div>
                )}
            </div>
        );


    const DesktopView = (
            <div className="flex px-24 py-5 gap-16">
                <div className="flex flex-col w-1/2 max-w-[600px] gap-y-4">
                    <h1 className="text-white text-3xl font-bold">Your Lineup</h1>
                    <div className="flex items-center space-x-2">
                        <ArtistListTitleInput  titleValue={listTitle} inputHandler={handleTitleInput} />
                        <button
                            className="px-4 py-2 rounded-full font-bold text-black bg-[#19cc58]"
                            disabled={isSubmitting}
                            onClick={createArtistList}
                        >
                        {isSubmitting ? "Saving..." : "Save Lineup"}
                        </button>
                    </div>
                    {error && <p className="text-red-500">{error}</p>}
                    <div className="h-80 overflow-y-auto mt-4">
                        {selectedArtists.length ?
                            selectedArtists.map((artist) => (
                                <ArtistPreview
                                    key={artist.id}
                                    artist={artist}
                                    isSearchItem={false}
                                    isSelected={true}
                                    onClick={() => handleArtistClick(artist)}
                                />
                            )) :
                            <p className="text-[#9db8a7] text-base py-8">
                                It&apos;s lonely in here...add some artists to get started
                            </p>
                        }
                    </div>
                </div>
                <div className="flex flex-col w-1/2 max-w-[600px] gap-y-4">
                    <h1 className="text-white text-3xl font-bold">Artist Search</h1>
                    <Search selectedArtists={selectedArtists} handleArtistClick={handleArtistClick} />
                </div>
            </div>
        );

    return (
        <FadeInSection>
            {isMobile ? MobileView : DesktopView}
        </FadeInSection>
    );
}

export default CreateNewList;