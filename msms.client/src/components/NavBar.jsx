
const NavBar = (props) => {
    return (
        <div>
            <h1>temporary navbar for app called {props.name}</h1>
            <p>The date today is {props.dateToday}</p>
        </div>);
}

export default NavBar;