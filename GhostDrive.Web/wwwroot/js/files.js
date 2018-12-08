$(document).ready(function() {
    var myDropzone = Dropzone.forElement("#UploadForm");
    myDropzone.on("complete",
        function() {
            location.reload();
        });
    try {
        TagCanvas.textColour = "#ff0000";
        TagCanvas.outlineColour = "#ff00ff";
        TagCanvas.outlineThickness = 1;
        TagCanvas.depth = 0.75;
        TagCanvas.maxSpeed = 0.1;
        TagCanvas.clickToFront = 1000;
        TagCanvas.Start("myCanvas");
    } catch (e) {
        document.getElementById("myCanvasContainer").style.display = "none";
    }
});

function share(id) {
    $.ajax({
        type: "GET",
        url: "/api/FileTransfer/GetShortLink",
        data: { id: id },
        async: true,
        success: function (shortLink) {
            $("#shareErrorAlert").hide();
            $("#shareModal #fileId").val(id);
            $("#shareModal #shortLink").val(shortLink);
            $("#shareModal").modal("show");
        },
        error: function (error) {
            console.log(error);
        }
    });
}

function shareToUser() {
    var fileId = $("#shareModal #fileId").val();
    var userLogin = $("#shareModal #userToShare").val();

    $.ajax({
        type: "POST",
        url: "/api/FileTransfer/ShareFile",
        data: JSON.stringify({ fileId: fileId, userLogin: userLogin }),
        contentType: "application/json",
        async: true,
        success: function () {
            $("#shareModal #userToShare").val("");
            $("#shareModal").modal("hide");
        },
        error: function (error) {
            $("#shareErrorAlert").text(error.responseText);
            $("#shareErrorAlert").show();
        }
    });
}