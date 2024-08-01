import { Link } from 'react-router-dom';
import './Navbar.css'
const Navbar = (props) => {
    return (
        <header className="hd1">
            <nav>
                    <Link to="/">{props.name}</Link>
                    <Link to="/searchk">Search</Link>
                    <Link to="/search">Search but again</Link>
            </nav>
        </header>
    )
}

export default Navbar;