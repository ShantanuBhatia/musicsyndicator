import ArtistListPreview from "./ArtistListPreview";

const ArtistListList = ({ artlistLists }) => {
    return artlistLists.map((al) => <ArtistListPreview key={al.artistListId} ArtistList={al} />)
}

export default ArtistListList;