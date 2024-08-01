import { Link } from 'react-router-dom';
import './Navbar.css'
const Navbar = (props) => {
    return (
        <header className="hd1">
            <nav>
                <ul>
                    <li><b><Link to="/">{props.name}</Link></b></li>
                </ul>
                <ul>
                    <li><Link to="/">{props.name}</Link></li>
                    <li><Link to="/about">About</Link></li>
                    <li><Link to="/login">Log in</Link></li>
                </ul>
            </nav>
        </header>
    )
}

export default Navbar;