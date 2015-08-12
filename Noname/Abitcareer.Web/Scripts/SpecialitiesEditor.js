﻿getCulture = function () {
    return document.location.href.split('/')[3].toLowerCase();
};

$(function () {
    $(document).ajaxStart(function () {
        $("body").toggleClass("loading");
    }).ajaxStop(function () {
        $("body").toggleClass("loading");
    })

    $('.specButton').click(function (event) {
        $("#inner").html(null);
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

    $('.card:not(#addNew)').on("mouseover", function () {
        $(this.children[0]).show();
    });

    $(".card:not(#addNew)").mouseout(function () {
        $(this.children[0]).hide();
    });

    $("#addNew").on("click", function(event)
    {
        $("#inner").html(null);
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

