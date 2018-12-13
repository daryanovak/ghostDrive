$(document).ready(function () {
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