import ArtistListPreview from "./ArtistListPreview";

const ArtistListList = ({ artlistLists, refreshArtistLists }) => {
    return (
        <div className="space-y-4">
            {artlistLists.map((al) => <ArtistListPreview key={al.artistListId} ArtistList={al} refreshArtistLists={refreshArtistLists} />)}
        </div>
    )

}

export default ArtistListList;