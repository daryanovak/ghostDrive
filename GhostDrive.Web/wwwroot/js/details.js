$(document).ready(function () {
    $("#tagsEditor").tagsInput({
        minChars: 0,
        maxChars: null,
        limit: null,
        delimiter: " ",
        validationPattern: new RegExp("^[a-zA-Z0-9]+$"),
        unique: true
    });

    updateTagsEditor("none");
});

function updateTagsEditor(displayState) {
    if (getTags() === "" && displayState === "none") {
        updateTagsInput(true);
        return;
    }
    $(".tag > .tag-remove, .tag-input").css("display", displayState);
    updateTagsInput(false);
}

function getTags() {
    return $("#tagsEditor").val();
}

function updateTagsInput(readonly) {
    $(".tag-input").prop("readonly", readonly);
}

function editTags() {
    event.currentTarget.blur();
    updateTagsEditor("block");
    $(".tag-input").focus();
}

function saveTags() {
    event.currentTarget.blur();
    updateTagsEditor("none");

    var newTags = getTags();
    var oldTags = $("#oldTags").val();

    if (newTags === oldTags) {
        return;
    }

    var fileId = $("#fileId").val();

    $.ajax({
        type: "POST",
        url: "/api/FileTransfer/UpdateTags",
        data: JSON.stringify({ fileId: fileId, tags: newTags }),
        contentType: "application/json",
        async: true,
        success: function() {
            $("#oldTags").val(newTags);
        },
        error: function (error) {
            alert(error.responseText);
        }
    });
}