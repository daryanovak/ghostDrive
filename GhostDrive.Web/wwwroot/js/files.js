$(document).ready(function() {
    var myDropzone = Dropzone.forElement("#UploadForm");
    Dropzone.options.myDropzone = {
        parallelUploads: 1
    };
    myDropzone.on("complete",
        function () {
            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                var actionPathPart = "Files/Index";
                var index = location.href.indexOf(actionPathPart);
                location.href = location.href.substring(0, index + actionPathPart.length);
            }
        });
    try {
        TagCanvas.textColour = "#ff0000";
        TagCanvas.outlineColour = "#ff00ff";
        TagCanvas.outlineThickness = 1;
        TagCanvas.depth = 0.75;
        TagCanvas.maxSpeed = 0.1;
        TagCanvas.clickToFront = 1000;
        TagCanvas.noTagsMessage = false;
        //TagCanvas.minTags = 3;
        TagCanvas.Start("myCanvas");
    } catch (e) {
        $("#myCanvasContainer").style.display = "none";
    }
});