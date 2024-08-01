import { useEffect, useState } from 'react';
import './App.css';
import NavBar from './components/NavBar';

function App() {
    const [forecasts, setForecasts] = useState();
    const [playlist, setPlaylist] = useState();

    const populatePlaylist = async () => {
        const response = await fetch('api/playlists/5');
        const data = await response.json();
        setPlaylist(data);
    }
    useEffect(() => {
        populateWeatherData();
        populatePlaylist();
    }, []);

    const contents = forecasts === undefined
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : <table className="table table-striped" aria-labelledby="tabelLabel">
            <thead>
                <tr>
                    <th>Date</th>
                    <th>Temp. (C)</th>
                    <th>Temp. (F)</th>
                    <th>Summary</th>
                </tr>
            </thead>
            <tbody>
                {forecasts.map(forecast =>
                    <tr key={forecast.date}>
                        <td>{forecast.date}</td>
                        <td>{forecast.temperatureC}</td>
                        <td>{forecast.temperatureF}</td>
                        <td>{forecast.summary}</td>
                    </tr>
                )}
            </tbody>
        </table>;


    const playlistContents = playlist === undefined ? 
        <p> Playlist did not load m8 but we have hot reload</p>
        :
        <p>{playlist.playlistId} {playlist.playlistName} {playlist.UserId}</p>;

    return (
        <>
            <NavBar name="MSMS" dateToday={Date()} />
            <h1 id="tabelLabel">Weather forecast</h1>
            <p>This component demonstrates fetching data from the server.</p>
            {contents}
            {playlistContents}
        </>
    );
    
    async function populateWeatherData() {
        const response = await fetch('weatherforecast');
        const data = await response.json();
        setForecasts(data);
    }

    
}

export default App;