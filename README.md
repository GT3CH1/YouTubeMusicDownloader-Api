# YouTubeMusicDownloader-Api

A simple API for downloading music/audio from YouTube using [NYouTubeDL](https://gitlab.com/BrianAllred/NYoutubeDL).
This was originally developed as a way to download music for transfer to my plex server.
The songs are downloaded to a folder called "Songs", and are organized by artist and album ,
`Songs/Test Artist/Test Album/Song.mp4.`
Each song will have be tagged with the artist, album, and song title as well.
As of now, it is up to the user to properly use the program and populate
all fields on the main webpage.

### Prepwork

1. Install youtube-dl

```bash
sudo curl -L https://yt-dl.org/downloads/latest/youtube-dl -o /usr/bin/youtube-dl
sudo chmod a+rx /usr/bin/youtube-dl
```

2. Ensure ASP.NET Core and dotnet 6.x is installed
3. Clone the repository

```bash
git clone https://github.com/GT3CH1/YouTubeMusicDownloader-Api
```

4. Build the project

```bash
cd YouTubeMusicDownloader-Api
dotnet build
```

5. Run the project

```bash
dotnet ./YouTubeDownloader/bin/Debug/net6.0/YouTubeDownloader.dll
```

6. Done!

### How to Use/Endpoints

### Opening the main webpage.

Navigate to the URL that is shown in your terminal when you start the application.

### Adding a song through the webpage.

At the top of the webpage, there is four fields you will have to input.

1. The title of the song
2. The artist of the song
3. The album of the song
4. The YouTube URL of the song

Once you have filled out the fields, click the "Add Song" button.
The song is stored in a database, and is ready to be downloaded.
Please see the "Downloading a Song" section for more information.

#### Store a URL to download

```http request
POST /api/Song/Add 
{
    "Url": "https://www.youtube.com/watch?v=some-url",
    "Title": "Test Video",
    "Artist": "Test Artist",
    "Album": "Test Album"
}
```

### Store a list

```http request
POST /api/Song/AddList
[
    {
        "Url": "https://www.youtube.com/watch?v=some-url",
        "Title": "Test Video",
        "Artist": "Test Artist",
        "Album": "Test Album"
    },
    {
        "Url": "https://www.youtube.com/watch?v=some-url",
        "Title": "Test Video",
        "Artist": "Test Artist",
        "Album": "Test Album"
    }
]
```

### Get a list of songs

```http request
GET /api/Song/GetList
```

### Get a song

```http request
GET /api/Song/Get/{id}
```

### Delete a song

```http request
DELETE /api/Song/Delete/{id}
```

### Delete all songs

```http request
DELETE /api/Song/DeleteAll
```

### Edit a song

```http request
PUT /api/Song/Edit/{id}
{
    "Url": "https://www.youtube.com/watch?v=some-url",
    "Title": "Test Video",
    "Artist": "Test Artist",
    "Album": "Test Album",
}
```

### Download a song

```http request
GET /api/Song/Download/{id}
```

### Download all songs

```http request
GET /api/Song/DownloadAll
```

## Things to do

- [x] Add a way to get a list of songs
- [x] Add a way to remove a song/list of songs
- [x] Add a way to download ONE song.
- [x] Add a way to download all of the songs.
- [x] Add a way to set the artist/album/title of a song.
- [x] Add a way to edit songs

If you run into any bugs, or would like to request/add a feature,
please open an issue on the [GitHub repository](https://github.com/gt3ch1/YouTubeMusicDownloader-Api).
Any and all help is appreciated.
