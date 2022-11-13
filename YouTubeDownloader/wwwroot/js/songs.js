function downloadSong(id) {
    $.ajax({
        url: "/Song/Download/" + id,
        type: "POST",
        success: function (data) {
            if(data.success) {
                $("#song-" + id + "-downloaded").text("Yes");
                alert("Downloaded!");
            } else {
                alert("Error! " + data.message);
            }
        },
        error: function (data) {
            alert("Error!");
            console.log(data);
        }
    });
}

function downloadAll() {
    $.ajax({
        url: "/Song/DownloadAll",
        type: "POST",
        success: function (data) {
            alert("Downloaded!");
            location.reload();
        },
        error: function (data) {
            console.log(data);
            alert("Error!");
        }
    });
}