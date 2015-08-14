getCulture = function () {
    return document.location.href.split('/')[3].toLowerCase();
};


requestDeletion = function (id) {
    window.event.stopPropagation();
    $(".deleteSubmit").data("target", id);
    $("#deleteConfirm").show(0);
    /*setTimeout(function () {
        $("#partialView").hide(0)
    }, 200);*/
};

deleteSpeciality = function () {
    var id = $(".deleteSubmit").data("target");
    $.ajax(
        {
            url: "deletespeciality",
            type: "POST",
            data: { id: id },
            success: function () {
                Notificate(LocalizationRemoveSuccess)
                $("#" + id).remove();
            },
            error: function () {
                Notificate(LocalizationRemoveFailed)
            }
        });
    $("#deleteConfirm").hide(0);
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

    $(".deleteDiscard").click(function () {
        $("#deleteConfirm").hide(0);
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


