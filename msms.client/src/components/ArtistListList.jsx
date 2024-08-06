import ArtistListPreview from "./ArtistListPreview";

const ArtistListList = ({ artlistLists, refreshArtistLists }) => {
    return artlistLists.map((al) => <ArtistListPreview key={al.artistListId} ArtistList={al} refreshArtistLists={refreshArtistLists} />)
}

export default ArtistListList;