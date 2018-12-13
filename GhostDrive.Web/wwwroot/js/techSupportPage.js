var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl('/techsupport')
    .build();

hubConnection.on("ReceiveMessage",
    function (message, fromUserName) {
        receiveMessage(message, fromUserName);
    });

hubConnection.start();

var element = $(".tech-support-chat");
var myStorage = localStorage;

if (!myStorage.getItem("chatID")) {
    myStorage.setItem("chatID", createUUID());
}

var messages = element.find('.messages');
var textInput = element.find('.text-box');

textInput.keydown(onMetaAndEnter).focus();
element.find('#sendMessage').click(sendNewMessage);
messages.scrollTop(messages.prop("scrollHeight"));

function createUUID() {
    var s = [];
    var hexDigits = "0123456789abcdef";
    for (var i = 0; i < 36; i++) {
        s[i] = hexDigits.substr(Math.floor(Math.random() * 0x10), 1);
    }
    s[14] = "4";
    s[19] = hexDigits.substr((s[19] & 0x3) | 0x8, 1);
    s[8] = s[13] = s[18] = s[23] = "-";

    var uuid = s.join("");
    return uuid;
}

function sendNewMessage() {
    var userInput = $(".text-box");
    var newMessage = userInput.html().replace(/\<div\>|\<br.*?\>/ig, '\n').replace(/\<\/div\>/g, "").trim()
        .replace(/\n/g, "<br>");

    var currentUserName = $("#floatingChatUserName").val();
    hubConnection.invoke("SendMessage", newMessage, currentUserName).catch(function(err) {
        return console.error(err.toString());
    });
}

function receiveMessage(newMessage, fromUserName) {
    if (!newMessage) {
        return;
    }

    var currentUserName = $("#floatingChatUserName").val();
    var messageClass = currentUserName === fromUserName ? "self" : "other";

    var messagesContainer = $(".messages");
    messagesContainer.append(['<li class="', messageClass, '">', newMessage, "</li>"].join(""));

    var userInput = $(".text-box");
    userInput.html("");
    userInput.focus();

    messagesContainer.finish().animate({
            scrollTop: messagesContainer.prop("scrollHeight")
        },
        250);
}

function onMetaAndEnter(event) {
    if ((event.metaKey || event.ctrlKey) && event.keyCode === 13) {
        sendNewMessage();
    }
}