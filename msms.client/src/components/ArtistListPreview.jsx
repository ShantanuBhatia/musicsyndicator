const ArtistListPreview = ({ArtistList }) => {


    return (
        <div>
            <h3>{ArtistList.artistListName}</h3>
            <ul>
                {ArtistList.artists.map((artist) => (
                    <li key={artist.artistSpotifyKey}>{artist.artistDisplayName}</li>
                ))}
            </ul>
        </div>
    );
};

export default ArtistListPreview;