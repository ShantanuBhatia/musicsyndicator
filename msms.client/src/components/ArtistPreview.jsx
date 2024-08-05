const ArtistPreview = ({ artist, isSelected, onClick }) => {

    return (
        <div
            
            className="flex flex-row py-1 gap-x-4 pr-4">
            {artist.images[0] ? <img
                className="aspect-square rounded-lg size-14"
                src={artist.images[0]?.url}
                alt={artist.name}
            /> : 
                <div className="aspect-square rounded-lg size-14 bg-cover bg-grey "></div>}
            <div className="flex-1">
                <h3 className="text-base text-white">{artist.name}</h3>
                <p className="text-sm text-[#9db8a7] capitalize">{artist.genres[0]}</p>
            </div>
            <div
                className="shrink-0 flex items-center justify-center"
            >
                <button
                    onClick={onClick}
                    className={isSelected ?
                        "h-8 px-4 text-sm rounded-full font-medium text-black bg-[#19cc58]"
                        :
                        "h-8 px-4 text-sm rounded-full font-medium text-white bg-[#29382e] hover:text-black hover:bg-[#19cc58]"
                    }
                >
                    {isSelected ? "Remove" : "Add"}
                </button>
            </div>
            
        </div>
    );
};


export default ArtistPreview;