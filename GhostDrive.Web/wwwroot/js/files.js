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