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
});