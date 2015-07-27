(function (i, s, o, g, r, a, m) {
    i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
        (i[r].q = i[r].q || []).push(arguments)
    }, i[r].l = 1 * new Date(); a = s.createElement(o),
    m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
})(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
ga('create', 'UA-65612868-1', 'auto');
ga('send', 'pageview');

window.onerror = function (message, file, line) {
    var sendObj = "line: " + line + "; message: " + message;
    ga('send', 'event', 'Error', file, sendObj);
}