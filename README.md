# Lineup (AKA musicsyndicator)

A webapp for managing Spotify playlists based on your favourite artists. Create Lineups, and stay up to date with their latest releases!


## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Node.js](https://nodejs.org/) (LTS version recommended)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/) or [Visual Studio Code](https://code.visualstudio.com/)

## Configuration

Before running the application, make sure to update the `appsettings.json` file in `MSMS.Server` with your specific configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "server=YOUR_CONNECTION_STRING"
  },
  "Spotify": {
    "ClientId": "YOUR_CLIENT_ID",
    "ClientSecret": "YOUR_CLIENT_SECRET"
  }
}
```

Replace `YOUR_CONNECTION_STRING`, `YOUR_CLIENT_ID`, and `YOUR_CLIENT_SECRET` with your actual database connection string and Spotify API credentials.

## Running the Development Servers

### Backend 


1. Open a terminal in the `MSMS.Server` directory.
2. Run the following command:

```
dotnet run
```

The API will be available at `https://localhost:7183`.

### Frontend

To start the frontend development server:

1. Open a terminal in the `msms.client` directory.
2. Install dependencies (if you haven't already):

```
npm install
```

3. Start the development server:

```
npm run dev
```

The frontend will be available at `https://localhost:5173`.

## Visual Studio

If you're using Visual Studio, you can also run both the backend and frontend simultaneously:

1. Open the solution in Visual Studio.
2. Set the startup project to the backend project.
3. Press F5 or click the "Start Debugging" button.

Visual Studio should automatically start both the backend and frontend servers.

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details.

## Acknowledgments

- [SpotifyAPI.NET](https://github.com/JohnnyCrazy/SpotifyAPI-NET) for writing a wonderful .NET client to access the Spotify API
- [Vivek Bhatia](https://github.com/bhatiavivek/) for endless support on configuring Cloudflare Tunnel, Cloudflare Pages and the DigitalOcean droplet.
