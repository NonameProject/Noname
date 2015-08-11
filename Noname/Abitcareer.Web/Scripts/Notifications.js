function Notificate(text) {
    Notification.requestPermission(newMessage);

    function newMessage(permission) {
        if (permission != "granted") return false;
    };

    new Notification("AbitCareer", {
        tag: "ache-mail",
        body: text,
        icon: document.location.host + "/Content/Images/notificationImage.png"
    });
}