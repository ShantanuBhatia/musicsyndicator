import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import { artistListApi } from '../services/apiService';
import ArtistListList from './ArtistListList';
import heroBanner from "../assets/herobanner.jpg";
import FadeInSection from './FadeInSection';

const placeholderLineups = ["Pop Girlies?", "Sadboi Classics?", "The Big Three?", "BTS Member Solos?"];

const Home = ({ user }) => {

    const [artistLists, setArtistLists] = useState([]);
    const [placeholderIndex, setPlaceholderIndex] = useState(0);
    const [lineupName, setLineupName] = useState("");

    

    useEffect(() => {
        let ignore = false;
        console.log("Got here 1")
        const fetchArtistLists = async () => {
            if (user?.isAuthenticated) {
                const myArtistLists = await artistListApi.getAll();
                if (!ignore) setArtistLists(myArtistLists);
            }
        }

        console.log(fetchArtistLists);
        console.log("got here 2")
        fetchArtistLists();

        return (() => {
            ignore = true;
        });
    }, [user?.isAuthenticated]);

    useEffect(() => {
        let ignore = false;
        const timer = () => {
            setPlaceholderIndex(prevIndex => {
                if (prevIndex === placeholderLineups.length - 1) {
                    return 0;
                }
                return prevIndex + 1;
            })
        };
        if(!ignore) setInterval(timer, 2500);

        return (() => { clearInterval(timer); })
    }, []);


    return (
        <div className="px-24 flex flex-1 flex-col min-w-[600px] py-5">
            <FadeInSection>
            <div className="layout-content-container flex flex-col max-w-[960px] mx-auto w-full flex-1">
                <div className="@container">
                    <div className="@[480px]:p-4">
                        <div
                            className="flex min-h-[480px] flex-col rounded-md gap-6 bg-cover bg-center bg-no-repeat @[480px]:gap-8 @[480px]:rounded-xl items-start justify-end px-4 pb-10 @[480px]:px-10"
                            style={{
                                backgroundImage: `linear-gradient(rgba(0, 0, 0, 0.1) 0%, rgba(0, 0, 0, 0.4) 100%), url(${heroBanner})`,
                                
                            }}
                            alt="Photo by Elviss Railijs Bitans"
                        >
                            <div className="flex flex-col gap-2 text-left">
                                    <h1
                                        className="text-white text-4xl font-black leading-tight tracking-[-0.033em] @[480px]:text-5xl @[480px]:font-black @[480px]:leading-tight @[480px]:tracking-[-0.033em]"
                                    >
                                        <br />Never miss a beat
                                    </h1>
                                    <h2 className="text-white text-sm font-normal leading-normal @[480px]:text-base @[480px]:font-normal @[480px]:leading-normal">
                                        Curate lineups of your favourite artists - we&apos;ll keep you up to date with their latest drops
                                    </h2>
                                    <h2 className="text-white text-sm font-normal leading-normal @[480px]:text-base @[480px]:font-normal @[480px]:leading-normal">
                                        Sign in with spotify, create a new Lineup, and add your favourite artists. When you're done, generate a Spotify playlist from your lineup. We&apos;ll add new releases as they drop.
                                    </h2>
                                </div>
                                {user.isAuthenticated && < label className="flex flex-col min-w-40 h-14 w-full max-w-[480px] @[480px]:h-16">
                                    <div className="flex w-full flex-1 items-stretch rounded-xl h-full">
                                        <input
                                            placeholder={placeholderLineups[placeholderIndex]}
                                            className="form-input flex w-full min-w-0 flex-1 resize-none overflow-hidden rounded-xl text-white focus:outline-0 focus:ring-0 border border-[#3c5344] bg-[#1c2620] focus:border-[#3c5344] h-full placeholder:text-[#9db8a7] rounded-r-none border-r-0 pr-2 border-l-0 pl-2 text-sm font-normal leading-normal @[480px]:text-base @[480px]:font-normal @[480px]:leading-normal"
                                            value={lineupName}
                                            onChange={(e) => setLineupName(e.target.value)}
                                        />
                                        <div className="flex items-center justify-center rounded-r-xl border-l-0 border border-[#3c5344] bg-[#1c2620] pr-[7px]">
                                            <Link to="/create" state={{ lineupName: lineupName }}>
                                                <button
                                                    className="flex min-w-[84px] max-w-[480px] cursor-pointer items-center justify-center overflow-hidden rounded-full h-10 px-4 @[480px]:h-12 @[480px]:px-5 bg-[#19cc58] text-[#111813] text-sm font-bold leading-normal tracking-[0.015em] @[480px]:text-base @[480px]:font-bold @[480px]:leading-normal @[480px]:tracking-[0.015em]"
                                                >
                                                    <span className="truncate">Create New Lineup</span>
                                                </button>
                                            </Link>
                                        </div>
                                    </div>
                                </label>}
                        </div>
                    </div>
                </div>

            </div>
            </FadeInSection>
            <FadeInSection delay={0.3}>
                <div className="layout-content-container flex flex-col max-w-[960px] w-full mx-auto flex-1">
                    {user?.isAuthenticated ? (
                        <div className="px-4 pb-3 pt-5">
                            {artistLists.length > 0 ? <ArtistListList user={user} />: <></>}
                        </div>
                    ) : <></>}
                </div>
            </FadeInSection>
        </div>
    );
};

export default Home;