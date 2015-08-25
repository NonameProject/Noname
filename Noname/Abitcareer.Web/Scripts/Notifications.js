var NotificationApi = (function () {

    function newMessage(permission) {
        if (permission != "granted") return false;
    };

    return{
        Success: function(text)
        {
            Notification.requestPermission(newMessage);

            new Notification("AbitCareer", {
                tag: "ache-mail",
                body: text,
                icon: "/Content/Images/notification-success.png"
            });
        },

        Failure: function(text)
        {
            Notification.requestPermission(newMessage);

            new Notification("AbitCareer", {
                tag: "ache-mail",
                body: text,
                icon: "/Content/Images/notification-failure.png"
            });
        }
    };
})();