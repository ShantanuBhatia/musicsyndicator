const ArtistPreview = ({ artist, isSelected, onClick }) => {
    return (
        <div onClick={onClick} className="artist-preview" style={{
            display: 'flex',
            alignItems: 'center',
            width: '300px',
            backgroundColor: isSelected ? '#d3d3d3' : '#808080',
            padding: '5px',
            margin: '5px',
            borderRadius: '4px'
        }}>
            {artist.images[0] ? <img
                src={artist.images[0]?.url}
                alt={artist.name}
                className="artist-image"
                style={{
                    width: '50px',
                    height: '50px',
                    objectFit: 'cover',
                    marginRight: '10px'
                }}
            /> : 
                <div className="artist-image" style={{
                    width: '50px',
                    height: '50px',
                    backgroundColor: '#00FF00',
                    marginRight: '10px'
                }}></div>}
            <div className="artist-info">
                <h3 className="artist-name" style={{
                    margin: '0',
                    color: 'white',
                    fontSize: '16px'
                }}>{artist.name}</h3>
                <p className="artist-genre" style={{
                    margin: '0',
                    color: 'white',
                    fontSize: '14px',
                    textTransform: 'capitalize',
                }}>{artist.genres[0]}</p>
            </div>
        </div>
    );
};


export default ArtistPreview;