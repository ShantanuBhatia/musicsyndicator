import { Link } from 'react-router-dom';
import './Navbar.css'
const Navbar = (props) => {
    return (
        <header className="hd1">
            <nav>
                <Link to="/">{props.name}</Link>
                <Link to="/search">Search</Link>
                <Link to="/create">Create New List</Link>
            </nav>
        </header>
    )
}

export default Navbar;