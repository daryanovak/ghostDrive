﻿var hubConnection = new signalR.HubConnectionBuilder()
    .withUrl('/techsupport')
    .build();

hubConnection.on("ReceiveMessage",
    function (message, fromUserName) {
        receiveMessage(message, fromUserName);
    });

hubConnection.start();

var element = $(".floating-chat");
var myStorage = localStorage;

if (!myStorage.getItem("chatID")) {
    myStorage.setItem("chatID", createUUID());
}

setTimeout(function() {
        element.addClass("enter");
    },
    500);

element.click(openElement);

function openElement() {
    var messages = element.find('.messages');
    var textInput = element.find('.text-box');
    element.find('>i').hide();
    element.addClass('expand');
    element.find('.chat').addClass('enter');
    var strLength = textInput.val().length * 2;
    textInput.keydown(onMetaAndEnter).prop("disabled", false).focus();
    element.off('click', openElement);
    element.find('.header button').click(closeElement);
    element.find('#sendMessage').click(sendNewMessage);
    messages.scrollTop(messages.prop("scrollHeight"));
}

function closeElement() {
    element.find('.chat').removeClass('enter').hide();
    element.find('>i').show();
    element.removeClass('expand');
    element.find('.header button').off('click', closeElement);
    element.find('#sendMessage').off('click', sendNewMessage);
    element.find('.text-box').off('keydown', onMetaAndEnter).prop("disabled", true).blur();
    setTimeout(function() {
            element.find('.chat').removeClass('enter').show()
            element.click(openElement);
        },
        500);
}

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