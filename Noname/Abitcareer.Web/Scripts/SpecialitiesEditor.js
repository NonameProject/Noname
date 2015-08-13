getCulture = function () {
    return document.location.href.split('/')[3].toLowerCase();
};

deleteSpeciality = function(id) {
    $.ajax(
        {
            url: "deletespeciality",
            type: "POST",
            data: { id: id },
            success: function () {
                Notificate(LocalizationRemoveSuccess)
                $("#"+id).remove();
            },
            error: function () {
                Notificate(LocalizationRemoveFailed)
            }
        });
};

$(function () {
    $(document).ajaxStart(function () {
        $("body").toggleClass("loading");
    }).ajaxStop(function () {
        $("body").toggleClass("loading");
    })

    $('body').on("click", ".specButton", function (event) {
        event.stopPropagation();
        $.post("specialities/edit", { id: $(this).attr("id") }, function (data) {
            if (!data) {
                Notificate(LocalizationForRemove);
            }
            else {
                $("#inner").empty();
                $("#partialView").show(0);
                $("#inner").html(data);
            }
        }
        );
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

    $("#addNew").on("click", function (event) {
        $("#inner").empty();
        event.stopPropagation();
        $.ajax(
            {
                url: "specialities/add",
                success: function (data) {
                    $("#inner").html(data);
                    $("#partialView").show(0);

                },
                error: function (e) {
                    alert("error" + e.status);
                }
            });
    });
});


