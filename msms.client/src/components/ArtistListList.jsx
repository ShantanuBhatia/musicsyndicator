import { useState, useEffect } from 'react';
import { artistListApi, playlistApi } from '../services/apiService'
import ArtistListPreview from "./ArtistListPreview";

const ArtistListList = ({ user }) => {
    const [artistLists, setArtistLists] = useState([]);
    


    const createPlaylist = async (artistList) => {

        setArtistLists(prevArtistLists =>
            prevArtistLists.map(al =>
                al.artistListId === artistList.artistListId ? {...al, isSubmitting: true} : al
            )
        );

        try {
            const newPlaylistId = await playlistApi.create({
                artistListId: artistList.artistListId,
                spotifyPlaylistName: artistList.artistListName
            });
            setArtistLists(prevArtistLists =>
                prevArtistLists.map(al =>
                    al.artistListId === artistList.artistListId ? { ...al, playlistId: newPlaylistId, isLoading: false, error: null } : al
                )
            );
        }
        catch (err) {
            setArtistLists(prevArtistLists =>
                prevArtistLists.map(al =>
                    al.artistListId === artistList.artistListId ? { ...al, isSubmitting: false, error: 'Failed to create playlist. Please try again.' } : al
                )
            );
            console.error('Error creating artist list:', err);
        }
    }


    useEffect(() => {
        let ignore = false;
        const fetchArtistLists = async () => {
            if (user?.isAuthenticated) {
                const myArtistLists = await artistListApi.getAll();

                if (!ignore) {
                    setArtistLists(myArtistLists.map(al => ({ ...al, isSubmitting: false, error: null })));
                }
                
            }
        }
        fetchArtistLists();

        return (() => {
            ignore = true;
        });
    }, [user?.isAuthenticated]);


    return (
        <div className="space-y-4">
            {artistLists.map((al) => <ArtistListPreview key={al.artistListId} ArtistList={al} isSubmitting={al.isSubmitting} error={al.error} createPlaylist={createPlaylist} />)}
        </div>
    )

}

export default ArtistListList;