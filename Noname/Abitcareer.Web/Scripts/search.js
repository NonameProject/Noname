var id =0;
$("#search").keydown(function () {
    clearTimeout(id);
    id = setTimeout(function () {
        var value = $('#search').val();
        if (!value && !value.trim()) {
            $(".card").show(0);
            return;
        }
        $.ajax(
            {
                url: "searchforspeaciality",
                type: "POST",
                data: { name: $("#search").val() },
                success: function (result) {
                    $(".card:not(#addNew)").hide(0);
                    for (var i = 0; i < result.length; i++) {
                        $("#" + result[i]).show(0);
                    }
                }
            });
    }, 500);
});