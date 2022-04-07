using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using YouTubeDownloader.Contexts;
using YouTubeDownloader.Controllers;
using YouTubeDownloader.Models;

namespace YouTubeDownlaoderTests;

[TestFixture]
public class Tests
{
    // The songs db context
    private static SongsDbContext _context;

    // The controller
    private static SongController _controller;

    // A list of songs to experiment with
    private static List<Song> _songs;

    [OneTimeSetUp]
    public void Setup()
    {
        // Use the database in memory.
        var options = new DbContextOptionsBuilder<SongsDbContext>()
            .UseInMemoryDatabase("Songs")
            .Options;
        _context = new SongsDbContext(options);
        _controller = new SongController(_context);
        // Create a list of songs.
        _songs = new List<Song>
        {
            new()
            {
                Title = "Test Song",
                Artist = "Test Artist",
                Album = "Test Album",
                // NOTE: This will fail in downloading.
                Url = "https://peasenet.com"
            },
            new()
            {
                Title = "Test Song 1",
                Artist = "Test Artist 1",
                Album = "Test Album 1",
                // NOTE: This will fail in downloading.
                Url = "https://peasenet.com/1"
            },
            new()
            {
                Title = "Test Song 2",
                Artist = "Test Artist 2",
                Album = "Test Album 2",
                // NOTE: This will fail in downloading.
                Url = "https://peasenet.com/2"
            }
        };
    }

    [OneTimeTearDown]
    public void TearDown()
    {
        _context.Database.EnsureDeleted();

        _context.Dispose();
    }

    /// <summary>
    /// Tests adding a single song to the database.
    /// </summary>
    [Test, Order(1)]
    public void TestAddSong()
    {
        // Get the previous count of songs
        var count = _context.Songs.Count();
        var song = _songs[0];
        _controller.AddSong(song);
        // Ensure that it is created in the database.
        Assert.AreEqual(count + 1, _context.Songs.Count());
    }

    /// <summary>
    /// Tests adding a list of songs to the database.
    /// </summary>
    [Test, Order(2)]
    public void TestAddList()
    {
        // Get the previous count of songs
        var count = _context.Songs.Count();
        // Create + add a list of songs
        var songs = _songs.GetRange(1, 2);
        _controller.AddSongList(songs);
        // Check count
        Assert.AreEqual(count + 2, _context.Songs.Count());
    }

    /// <summary>
    /// Checks whether or not the three added songs are in the database, and the information is correct.
    /// </summary>
    [Test, Order(3)]
    public void TestAddSongsInfo()
    {
        var songsInDb = _context.Songs.ToList();
        // Songs are in newest first, need to reverse list here.
        var tmpList = new List<Song>(_songs);
        // tmpList.Reverse();
        // Check that the songs are in the database.
        Assert.AreEqual(3, songsInDb.Count);
        // Check that the information is correct.
        for (var i = 0; i < songsInDb.Count; i++)
        {
            Assert.AreEqual(tmpList[i].Title, songsInDb[i].Title);
            Assert.AreEqual(tmpList[i].Artist, songsInDb[i].Artist);
            Assert.AreEqual(tmpList[i].Album, songsInDb[i].Album);
            Assert.AreEqual(tmpList[i].Url, songsInDb[i].Url);
        }

    }
    
    /// <summary>
    /// Tests to see if all songs in the database are listed as Json.
    /// </summary>
    [Test, Order(4)]
    public void TestGetAllSongs()
    {
        var songs = _controller.GetAllSongs();
        // Deserialize
        var songsFromJsonList = (List<Song>)songs.Value;
        // Check that each song from json list are equal to the the songs in _songs.
        for (var i = 0; i < songsFromJsonList.Count; i++)
        {
            Assert.AreEqual(_songs[i].Title, songsFromJsonList[i].Title);
            Assert.AreEqual(_songs[i].Artist, songsFromJsonList[i].Artist);
            Assert.AreEqual(_songs[i].Album, songsFromJsonList[i].Album);
            Assert.AreEqual(_songs[i].Url, songsFromJsonList[i].Url);
        }
    }

    /// <summary>
    /// Tests whether or not the song with the given id is deleted.
    /// </summary>
    [Test, Order(5)]
    public void TestDeleteSong()
    {
        var id = 1;
        // Get the count of songs
        var count = _context.Songs.Count();
        // Delete the song
        _controller.DeleteSong(id);
        // Check the count is one less.
        Assert.AreEqual(count - 1, _context.Songs.Count());
        // Check that the song is not in the database.
        Assert.IsFalse(_context.Songs.Any(s => s.Id == id));
    }

    [Test, Order(6)]
    public void TestEditSong()
    {
        // Get the song to edit
        var song = _context.Songs.First();
        // Edit the song
        song.Title = "Edited Title";
        song.Artist = "Edited Artist";
        song.Album = "Edited Album";
        song.Url = "https://peasenet.com/edited";
        // Save the song
        _controller.EditSong(song.Id,song);
        // Check that the song is in the database.
        Assert.IsTrue(_context.Songs.Any(s => s.Id == song.Id));
        // Check that the song is the same as the edited song.
        Assert.AreEqual(song.Title, _context.Songs.First(s => s.Id == song.Id).Title);
        Assert.AreEqual(song.Artist, _context.Songs.First(s => s.Id == song.Id).Artist);
        Assert.AreEqual(song.Album, _context.Songs.First(s => s.Id == song.Id).Album);
        Assert.AreEqual(song.Url, _context.Songs.First(s => s.Id == song.Id).Url);
        Assert.False(song.IsDownloaded());
    }

    /// <summary>
    ///  Deletes a list of songs based off of id.
    /// </summary>
    [Test, Order(7)]
    public void TestDeleteList()
    {
        // Get the count of songs
        var count = _context.Songs.Count();
        // Get the songs in the database
        var songs = _context.Songs.ToList();
        // Get the Ids of the songs to delete
        var ids = new List<int>();
        for (var i = 0; i < songs.Count; i++)
        {
            ids.Add(songs[i].Id);
        }
        // Delete the songs
        _controller.DeleteSongList(ids);
        // Check that the count is count-ids less
        Assert.AreEqual(count - ids.Count, _context.Songs.Count());
        // Check that the songs are not in the database.
        foreach (var id in ids)
        {
            Assert.IsFalse(_context.Songs.Any(s => s.Id == id));
        }
    }
}