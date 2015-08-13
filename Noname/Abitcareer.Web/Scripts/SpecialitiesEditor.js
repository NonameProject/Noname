getCulture = function () {
    return document.location.href.split('/')[3].toLowerCase();
};

var id;

deleteSpeciality = function (Id) {
    id = Id;
    $("#dialog-confirm").dialog('open');
};

$("#dialog-confirm").dialog({
    zIndex: 10000,
    autoOpen: false,
    resizable: false,
    width: 400,
    height: 200,
    modal: true,
    buttons: [{
        text: SpecialityDeleteAgreeConfirm,
        click: function () {
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
            $(this).dialog("close");
        }
    },
    {
        text: SpecialityDeleteCancelConfirm,
        click: function () {
            $(this).dialog("close");
        }
    }]
});

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


