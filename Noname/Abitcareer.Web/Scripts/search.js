var id =0;
$("#search").keydown(function () {
    clearTimeout(id);
    $(this).removeClass('success', 100);
    id = setTimeout(function () {
        var value = $('#search').val();
        if (!value && !value.trim()) {
            $(".cardWrapper").show(0);
            return;
        }
        $.ajax(
            {
                url: "searchforspeaciality",
                type: "POST",
                data: { name: value },
                success: function (result) {
                    $('#search').addClass('success');
                    $(".cardWrapper:not(#addCard)").hide(0);
                    for (var i = 0; i < result.length; i++) {
                        $("#" + result[i]).show(0);
                    }
                }
            });
    }, 500);
});