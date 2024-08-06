import { useState } from 'react';
import { playlistApi } from '../services/apiService';

const ArtistListPreview = ({ ArtistList, refreshArtistLists }) => {
    const [isSubmitting, setIsSubmitting] = useState(false);
    const [error, setError] = useState(null);

    const createPlaylist = async (e) => {
        e.preventDefault();
        setIsSubmitting(true);
        setError(null);
        try {
            await playlistApi.create({
                artistListId: ArtistList.artistListId,
                spotifyPlaylistName: ArtistList.artistListName
            });
        }
        catch (err) {
            setError('Failed to create playlist. Please try again.');
            console.error('Error creating artist list:', err);
        }
        finally {
            setIsSubmitting(false);
            refreshArtistLists();
        }
    }

    const renderActionButton = () => {
        if (error) {
            return (<p>ERROR: {error}</p>);
        }
        if (ArtistList.playlistId) {
            return (<a href={`https://open.spotify.com/playlist/${ArtistList.playlistId}`} target="_blank">Open Spotify Playlist</a>);
        }
        return <button onClick={createPlaylist} disabled={isSubmitting}>{isSubmitting? "Creating..." : "Create Playlist"}</button>
    }

    return (
        <div>
            <h3>{ArtistList.artistListName}</h3>
            <ul>
                {ArtistList.artists.map((artist) => (
                    <li key={artist.artistSpotifyKey}>{artist.artistDisplayName}</li>
                ))}
            </ul>
            {renderActionButton()}
        </div>
    );
};

export default ArtistListPreview;