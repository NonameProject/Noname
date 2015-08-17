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
                Notificate(Localization.LocalizationRemoveSuccess)
                $("#" + id).remove();
            },
            error: function () {
                Notificate(Localization.LocalizationRemoveFailed)
            }
        });
    $("#deleteConfirm").hide(0);
};

var oldWidth = 0;
$(function () {
    $(document).ajaxStart(function () {
        $("body").toggleClass("loading");
    }).ajaxStop(function () {
        $("body").toggleClass("loading");
    })

    $('body').on("click", ".cardWrapper", function (event) {
        event.stopPropagation();
        $.post("specialities/edit", { id: $(this).attr("id") }, function (data) {
            if (!data) {
                Notificate(Localization.LocalizationForRemove);
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

    resizeCards();
});
function resizeCards() {
    var wrapperWidth = $('#wrapper').width();
    if (Math.abs(wrapperWidth - oldWidth) < 5) return;
    oldWidth = wrapperWidth;
    wrapperWidth -= 20;
    var card = $('.cardWrapper'),
        maxWidth = parseInt(card.css('maxWidth')),
        minWidth = parseInt(card.css('minWidth')),
        borderWidth = card.innerWidth() - card.width();

    var maxCount = Math.floor(wrapperWidth / (borderWidth + minWidth)),
        minCount = Math.floor(wrapperWidth / maxWidth);

    var computedWidth = Math.floor((wrapperWidth - maxCount * borderWidth) / maxCount);
    if (computedWidth > maxWidth) computedWidth = maxWidth;
    card.width(computedWidth);
}

$(window).resize(resizeCards);
