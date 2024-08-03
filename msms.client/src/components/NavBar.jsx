import './Navbar.css'

const Navbar = ({ user, logoutCallback, randomProp }) => {

    const handleLogin = () => {
        window.location.href = 'https://localhost:7183/api/auth/login';
    };

    return (
        <div className="sticky top-0 z-50 bg-[#111813] dark group/design-root "
            style={{
                fontFamily: ["Plus Jakarta Sans", "Noto Sans", "sans-serif"]
            }} >
            <div className="layout-container flex h-full grow flex-col">
                <header className="flex items-center justify-between whitespace-nowrap border-b border-solid border-b-[#29382e] px-10 py-3">
                    <div className="flex items-center gap-4 text-white">
                        <h2 className="text-white text-lg font-bold leading-tight tracking-[-0.015em]">{user.isAuthenticated? `${user.name}'s Lineups` : "Lineup"}</h2>
                    </div>
                    <div className="flex flex-1 justify-end gap-8">
                        <div className="flex gap-2">
                            {user?.isAuthenticated ?
                                <button
                                className="flex min-w-[84px] max-w-[480px] cursor-pointer items-center justify-center overflow-hidden rounded-full h-10 px-4 bg-[#19cc58] text-[#111813] text-sm font-bold leading-normal tracking-[0.015em]"
                                    onClick={logoutCallback}
                                >
                                    <span className="truncate">Log Out</span>
                                </button> 
                                :
                                <button
                                    className="flex min-w-[84px] max-w-[480px] cursor-pointer items-center justify-center overflow-hidden rounded-full h-10 px-4 bg-[#19cc58] text-[#111813] text-sm font-bold leading-normal tracking-[0.015em]"
                                    onClick={handleLogin}
                                >
                                    <span className="truncate">Log In with Spotify</span>
                                </button> 
                            }
                            
                        </div>
                    </div>
                </header>
            </div>
        </div>
    )
}

export default Navbar;