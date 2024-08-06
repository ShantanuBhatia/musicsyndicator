const ArtistListPreview = ({ ArtistList, isSubmitting, error, createPlaylist }) => {
    
    const handleGenerateClick = (e) => {
        e.preventDefault();
        createPlaylist(ArtistList)
    }


    const getArtistSummary = (artists) => {
        if (artists.length  < 1) {
            return "No artists in lineup";
        }
        if (artists.length === 1) {
            return `${artists[0].artistDisplayName}`;
        }
        if (artists.length === 2) {
            return `${artists[0].artistDisplayName} and ${artists[1].artistDisplayName}`;
        }

        return `${artists[0].artistDisplayName}, ${artists[1].artistDisplayName}, and ${artists.length - 2} more`
    }

    const renderActionButton = () => {
        if (error) {
            return (<p>ERROR: {error}</p>);
        }
        if (ArtistList.playlistId) {
            return (<a className="text-white hover:text-[#19cc58] text-sm font-normal leading-normal" href={`https://open.spotify.com/playlist/${ArtistList.playlistId}`} target="_blank">Open Spotify Playlist</a>);
        }
        return <button
            onClick={handleGenerateClick}
            className="text-white hover:text-[#19cc58] text-sm font-normal leading-normal" 
            disabled={isSubmitting}>
            {isSubmitting ? "Creating..." : "Create Playlist"}
        </button>
    }

    return (
        <div className="flex flex-row items-center  justify-between gap-4 bg-[#1b221d] px-8 min-h-[72px] py-2 rounded-full">
                <div className="flex flex-col ">
                    <p className="text-white text-base font-medium leading-normal line-clamp-1">{ArtistList.artistListName}</p>
                    <p className="text-[#9db8a7] text-sm font-normal leading-normal line-clamp-2">{getArtistSummary(ArtistList.artists)}</p>
                </div>
                {renderActionButton()}
        </div>
    );
};

export default ArtistListPreview;