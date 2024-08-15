import { useEffect, useState } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { artistListApi } from '../services/apiService';
import ArtistListList from './ArtistListList';
import heroBanner from "../assets/herobanner.jpg";
import FadeInSection from './FadeInSection';

const placeholderLineups = ["Pop Girlies?", "Sadboi Classics?", "The Big Three?", "BTS Member Solos?"];

const Home = ({ user, isMobile }) => {

    const [artistLists, setArtistLists] = useState([]);
    const [placeholderIndex, setPlaceholderIndex] = useState(0);
    const [lineupName, setLineupName] = useState("");
    const [showInterestModal, setShowInterestModal] = useState(null);
    const [userEmail, setUserEmail] = useState("");

    const location = useLocation();

    useEffect(() => {
        if (location.state?.showInterestModal) {
            setShowInterestModal(true);
        }
    }, [location]);
    

    useEffect(() => {
        let ignore = false;
        const fetchArtistLists = async () => {
            if (user?.isAuthenticated) {
                const myArtistLists = await artistListApi.getAll();
                if (!ignore) setArtistLists(myArtistLists);
            }
        }
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

    const InterestModal = (
        <div className="fixed inset-0 z-50 overflow-auto bg-black bg-opacity-50 flex items-center justify-center">
            <div className="relative bg-[#111813] w-full max-w-md m-auto flex-col flex rounded-lg shadow-lg">
                <div className="px-6 py-4">
                    <h2 className="text-white text-xl font-bold mb-4">You just wait, sunshine...</h2>
                    <p className="text-white text-base mb-4">Lineup is currently in limited release - we&apos;ll let you know when it&apos;s ready for you</p>
                    <input
                        type="email"
                        placeholder="user@example.com"
                        maxLength="32"
                        onChange={(e) => { setUserEmail(e.target.value) }}
                        value={userEmail}
                        className="w-full p-2 mb-4 border rounded text-gray-200 bg-gray-700 border-gray-600"
                    />
                    <div className="flex justify-end">
                        <button
                            className="bg-[#19cc58] text-[#111813] rounded-full font-bold hover:bg-[#16b850] px-4 py-2"
                            onClick={() => setShowInterestModal(false)}
                        >
                            Submit
                        </button>
                    </div>
                </div>
                <button
                    className="absolute top-0 right-0 mt-4 mr-4 text-gray-500 hover:text-gray-700 dark:text-gray-400 dark:hover:text-gray-200"
                    onClick={() => setShowInterestModal(false)}
                >
                    <svg className="h-6 w-6" fill="none" viewBox="0 0 24 24" stroke="currentColor">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                    </svg>
                </button>
            </div>
        </div>
    );

    return (
        <div className={`flex flex-col flex-1 ${isMobile ? 'px-4' : 'px-24'} py-5`}>

            {showInterestModal && <>{InterestModal}</>}
            
            <FadeInSection delay={0.2}>
                <div className="max-w-[960px] mx-auto w-full">
                    <div className={isMobile ? 'p-2' : 'p-4'}>
                        <div
                            className={`flex flex-col bg-cover bg-center bg-no-repeat items-start justify-end p-6 ${isMobile ? 'min-h-[400px] rounded-lg' : 'min-h-[480px] rounded-xl p-10'} gap-4`}
                            style={{
                                backgroundImage: `linear-gradient(rgba(0, 0, 0, 0.1) 0%, rgba(0, 0, 0, 0.4) 100%), url(${heroBanner})`,
                            }}
                        >
                            <div className="flex flex-col gap-2 text-left text-white">
                                <h1 className="text-3xl font-black leading-tight tracking-tight">
                                    Never miss a beat
                                </h1>
                                <h2 className="text-base font-normal">
                                    Curate lineups of your favourite artists - we&apos;ll keep you up to date with their latest drops
                                </h2>
                                <h2 className="text-base font-normal">
                                    Sign in with spotify, create a new Lineup, and add your favourite artists. When you're done, generate a Spotify playlist from your lineup. We&apos;ll add new releases as they drop.
                                </h2>
                            </div>
                            {user.isAuthenticated && (
                                isMobile ? 
                                        <Link to="/create" state={{ lineupName: lineupName }}>
                                            <button className="rounded-full bg-[#19cc58] text-[#111813] font-bold px-5 py-3">
                                                Create New Lineup
                                            </button>
                                        </Link>
                                    :
                                <label className="flex w-full max-w-[480px]">
                                    <input
                                        placeholder={placeholderLineups[placeholderIndex]}
                                        className="flex-1 h-16 rounded-l-xl border border-r-0 border-[#3c5344] bg-[#1c2620] text-white placeholder-[#9db8a7] focus:outline-none focus:ring-0 px-2"
                                        value={lineupName}
                                        onChange={(e) => setLineupName(e.target.value)}
                                    />
                                    <div className="flex items-center justify-center rounded-r-xl border border-l-0 border-[#3c5344] bg-[#1c2620] pr-2">
                                        <Link to="/create" state={{ lineupName: lineupName }}>
                                            <button className="rounded-full bg-[#19cc58] text-[#111813] font-bold px-5 py-3">
                                                Create New Lineup
                                            </button>
                                        </Link>
                                    </div>
                                </label>
                            )}
                        </div>
                    </div>
                </div>
            </FadeInSection>
            <FadeInSection delay={0.3}>
                <div className="max-w-[960px] w-full mx-auto">
                    {user?.isAuthenticated && artistLists.length > 0 && (
                        <div className="px-4 py-5">
                            <ArtistListList user={user} />
                        </div>
                    )}
                </div>
            </FadeInSection>
        </div>
    );
};

export default Home;