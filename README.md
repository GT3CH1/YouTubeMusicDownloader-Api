# YouTubeMusicDownloader-Api
A simple API for downloading music/audio from YouTube using NYoutubeDL.

### How to Use

#### Store a URL to download
```http request
POST /api/Song/Add 
{
    "Url": "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
    "Title": "Test Video",
    "Artist": "Test Artist",
    "Album": "Test Album"
}
```
#### Store a list
```http request
POST /api/Song/AddList
[
    {
        "Url": "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
        "Title": "Test Video",
        "Artist": "Test Artist",
        "Album": "Test Album"
    },
    {
        "Url": "https://www.youtube.com/watch?v=dQw4w9WgXcA",
        "Title": "Test Video",
        "Artist": "Test Artist",
        "Album": "Test Album"
    }
]
```

## Things to do
- [ ] Add a way to get a list of songs
- [ ] Add a way to remove a song/list of songs
- [ ] Add a way to download ONE song.
- [ ] Add a way to download all of the songs.
- [ ] Add a way to set the artist/album/title of a song.
- [ ] Add a way to set the wanted file type for songs
