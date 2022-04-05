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
    "Url": "https://www.youtube.com/watch?v=dQw4w9WgXcQ",
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
- [ ] Add a way to set the wanted file type for songs
- [x] Add a way to edit songs