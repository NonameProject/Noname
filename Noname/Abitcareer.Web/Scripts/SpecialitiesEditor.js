getCulture = function () {
    return document.location.href.split('/')[3].toLowerCase();
};

$(function () {
    $(document).ajaxStart(function () {
        $("body").toggleClass("loading");
    }).ajaxStop(function () {
        $("body").toggleClass("loading");
    })

    $('body').on("click", ".specButton", function (event) {
        $("#inner").empty();
        $("#partialView").show(0);
        event.stopPropagation();
        $.post("specialities/edit", { id: $(this).attr("id") }, function (data) {
            $("#inner").html(data);
        });
    });
    $(document).click(function (event) {
        if ($(event.target).closest('#inner').length) return;
        $('#partialView').hide();
        event.stopPropagation();
    });

    $('body').on("mouseover", ".card:not(#addNew)", function () {
        $(this.children[0]).show();
    });

    $("body").on("mouseout", ".card:not(#addNew)", function () {
        $(this.children[0]).hide();
    });

    $("#addNew").on("click", function(event)
    {
        $("#inner").empty();
        event.stopPropagation();
        $.ajax(
            {
                url: "specialities/add",
                success: function(data)
                {
                    $("#inner").html(data);
                    $("#partialView").show(0);
                    
                },
                error: function(e)
                {
                    alert("error" + e.status);
                }          
            });
    })
});


