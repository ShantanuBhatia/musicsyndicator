import { Link } from 'react-router-dom';


const NavigationBar = ({ user, logoutCallback, isMobile}) => {

    

    const handleLogin = () => {
        window.location.href = `${import.meta.env.VITE_LINEUP_API_BASE || "https://localhost:7183"}/api/auth/login`;
    };
        
    return (
        <nav className="sticky top-0 z-50 bg-[#111813] border-b border-[#29382e]"
            style={{
                fontFamily: ["Plus Jakarta Sans", "Noto Sans", "sans-serif"]
            }} >
            <div className="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
                <header className={`flex justify-between items-center h-16`}>
                    <div className="flex-shrink-0">
                        <Link to="/" className={`text-white font-bold hover:text-gray-200 ${isMobile ? 'text-xl' : 'text-lg'}`} >
                            { user.isAuthenticated ? (isMobile ? `Hey, ${user.name}` : `${user.name}'s Lineups`) : "Lineup"}
                        </Link>
                    </div>
                    <div>
                        
                            {user?.isAuthenticated ?
                                <button
                                    className={`bg-[#19cc58] text-[#111813] rounded-full font-bold hover:bg-[#16b850] transition duration-300 px-4 py-2 text-sm`}
                                    onClick={logoutCallback}
                                >
                                    <span className="truncate">Sign Out</span>
                                </button> 
                                :
                                <button
                                className={`bg-[#19cc58] text-[#111813] rounded-full font-bold hover:bg-[#16b850] transition duration-300  px-4 py-2 text-sm`}
                                    onClick={handleLogin}
                                >
                                    Connect to Spotify
                                </button> 
                            }

                    </div>
                </header>
            </div>
        </nav>
    )
}

export default NavigationBar;