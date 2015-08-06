function setHash() {
    $('.changeLanguage').attr('href', function () {
        var hash = window.location.hash.slice(1);
        if (this.href[0] == '#')
            return '#' + hash;
        else
            return this.href.split('anchor')[0] + '&anchor=' + hash;
    });
}