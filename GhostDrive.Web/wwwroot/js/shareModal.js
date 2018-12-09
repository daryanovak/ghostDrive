function share(e, id, shortLinkEmptyMessage) {
    e.stopPropagation();
    $("#shareErrorAlert").hide();
    $("#shareModal #fileId").val(id);
    $("#shareModal #userToShare").val("");
    $("#shareModal #shortLink").val(shortLinkEmptyMessage);
    $("#shareModal").modal("show");
    $.ajax({
        type: "GET",
        url: "/api/FileTransfer/GetShortLink",
        data: { id: id },
        async: true,
        success: function (shortLink) {
            $("#shareModal #shortLink").val(shortLink);
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
            $("#shareModal").modal("hide");
        },
        error: function (error) {
            $("#shareErrorAlert").text(error.responseText);
            $("#shareErrorAlert").show();
        }
    });
}