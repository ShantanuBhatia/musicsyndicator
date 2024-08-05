import { useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { artistListApi } from '../services/apiService';
import Search from "./Search";
import ArtistPreview from './ArtistPreview';
import FadeInSection from './FadeInSection';

const CreateNewList = ({ user }) => {
    const [selectedArtists, setSelectedArtists] = useState([]);
    
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);
    const navigate = useNavigate();
    const location = useLocation();
    const [listTitle, setListTitle] = useState(location.state ? location.state.lineupName : "");



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
        <FadeInSection>
        <div className="px-24 flex py-5 gap-16">
            <div className="order-2 flex flex-col flex-initial w-full max-w-[600px] min-w-[300px] mx-auto gap-y-4">
                <h1
                    className="text-white text-3xl font-black leading-tight"
                >
                    Artist Search
                </h1>
                <Search selectedArtists={selectedArtists} handleArtistClick={handleArtistClick} />
            </div>
            <div className="order-1 flex flex-col flex-initial w-full max-w-[600px] min-w-[300px] mx-auto gap-y-4">
                <h1
                    className="text-white text-3xl font-black leading-tight"
                >
                    Your Lineup
                </h1>

                <div className="flex w-full mx-auto items-stretch bg-[#111813]">
                        <input
                            placeholder="Enter name here (make it memorable!)"
                        className="form-input text-white h-10 focus:h-16 w-full text-lg focus:text-2xl bg-[#111813] border-b-2 border-[#19cc58] focus:outline-none transition-[font-size,_padding,_border,_height] duration-300 ease-in-out"
                            value={listTitle}
                            onChange={(e) => setListTitle(e.target.value)}
                        />
                </div>

                {selectedArtists.length !== 0 ? < div className="flex-row h-80 overflow-y-auto">
                    {selectedArtists && selectedArtists.map((artist) => (
                        <ArtistPreview key={artist.id} artist={artist} isSearchItem={false} isSelected={selectedArtists.some(a => a.id === artist.id)} onClick={() => { handleArtistClick(artist) }} />
                    ))}
                </div> :
                    <p className="text-[#9db8a7] text-base py-8">
                    It&apos;s lonely in here...add some artists to get started 
                </p>}
        </div>
            </div>
        </FadeInSection>
    );

 
}

export default CreateNewList;