SpecialityEditor = (function () {

    var settings = {},
        localStrings = {},
        searchItemId = 0,
        oldWidth = 0,
        prevTop = 0;

    var partialView = $('#partialView'),
        inner = $('#inner');
    var onDocScroll = function (e) {
        var currentTop = $(this).scrollTop();
        if (partialView.css('display') == 'none') {
            prevTop = currentTop;
            return;
        }

        if (currentTop == prevTop) return;

        var containerHeight = partialView.height(),
            height = inner.innerHeight();
        if (height <= containerHeight) {
            inner.css('top', 0);
            return;
        }

        var pos = parseInt(inner.css('top'));
        pos -= currentTop - prevTop;
        if (pos > 0) pos = 0;
        else if (height + pos <= containerHeight) pos = containerHeight - height;

        prevTop = currentTop;
        inner.css('top', pos);
    };

    var onRemoteComplete = function () {

        var res = false;

        $('#Name').rules().remote.complete = function (xhr) {
            $("span[data-valmsg-for='Name']").removeClass();
            if (xhr.status == 200 && xhr.responseText === 'true') {
                $("span[data-valmsg-for='Name']").addClass("glyphicon glyphicon-ok success");
                res = true;
            }
            else {
                $("span[data-valmsg-for='Name']").addClass("glyphicon glyphicon-remove failure");
                res = false;
                $("#createSpeciality").prop("disabled", true);
            }
        };
    }

    var resizeCards = function () {
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
        card.width(computedWidth)
    };
    resizeCards.reset = function () {
        oldWidth = 0;
    }

    var find = function () {
        clearTimeout(searchItemId);
        setTimeout(function (settings) {
            if (!settings.search.val().trim()) $("#searchclear").hide();
            else
                $("#searchclear").show();
        }, 10, settings);
        $(this).removeClass('success', 100);
        searchItemId = setTimeout(function () {
            var value = settings.search.val();
            if (!value && !value.trim()) {
                $('#search').addClass('success');
                $(".cardWrapper").show(0);
                return;
            }
            $.ajax(
                {
                    url: "[Route:SearchForSpeaciality]",
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
    }

    return {

        resizeCards: resizeCards,

        getCulture: function () {
            return document.location.href.split('/')[3].toLowerCase();
        },

        requestDeletion: function (id, event) {
            event.stopPropagation();
            settings.deleteSubmit.data("target", id);
            settings.deleteConfirm.show(0);
        },

        deleteSpeciality: function () {
            var id = settings.deleteSubmit.data("target");
            $.ajax(
                {
                    url: "[Route:DeleteSpeciality]",
                    type: "POST",
                    data: { id: id },
                    success: function (name) {
                        Notificate("[" + name + "] - [Resx:SpecialityRemoveSuccess]");
                        $("#" + id).remove();
                    },
                    error: function () {
                        Notificate("[" + name + "] - [Resx:SpecialityRemoveFailed]");
                    }
                });
            settings.deleteConfirm.hide(0);
        },

        init: function (localResourses) {

            settings.inner = $("#inner");

            settings.partialView = $("#partialView");

            settings.deleteDiscard = $(".deleteDiscard");

            settings.deleteConfirm = $("#deleteConfirm");

            settings.deleteSubmit = $(".deleteSubmit");

            settings.addButton = $("#addCard");

            settings.names = $("#Name");

            settings.editor = $("#editor");

            settings.exitButton = $("#exitButton");

            settings.exit = $("#exit");

            settings.search = $("#search");


            localStrings = localResourses;

            this.unbindUIActions();
            this.bindUIActions();
        },

        unbindUIActions: function () {
            $(document).off();
            $('body').off();
            settings.deleteDiscard.off();
            settings.addButton.off();
            settings.deleteSubmit.off();
            settings.names.off();
            settings.editor.off();
            settings.exitButton.off();
            settings.exit.off();
            settings.search.off();
        },

        bindUIActions: function () {
            $(document).scroll(onDocScroll);

            $(document).ajaxStart(function () {
                $("body").toggleClass("loading");

            }).ajaxStop(function () {
                $("body").toggleClass("loading");
            })

            $('body').on("click", ".cardWrapper", function (event) {
                event.stopPropagation();
                if (event.target === this)
                    return;
                if ($(this).data('isauthenticated') === 'False') {
                    window.location.assign('[Route:Default]#' + $(this).attr('id'));
                    return;
                }
                settings.inner.empty();
                settings.partialView.show(0);
                $.post('[Route:editSpeciality]', { id: $(this).attr('id') }, function (data) {
                    if (!data) {
                        settings.partialView.hide(0);
                        Notificate('[Resx:SpecialityOpenFailed]');
                    }
                    else {
                        settings.inner.css('top', 0);
                        settings.inner.html(data);
                    }
                }).fail(function () {
                    settings.partialView.hide(0);
                });
            });

            settings.deleteDiscard.on('click', function () {
                settings.deleteConfirm.hide(0);
            });

            $(document).on('click', function (event) {
                if ($(event.target).closest(settings.inner).length) return;
                settings.partialView.hide();
                event.stopPropagation();
            });

            settings.addButton.on("click", function (event) {
                settings.inner.empty();
                event.stopPropagation();
                $.ajax(
                    {
                        url: "[Route:addSpeciality]",
                        success: function (data) {
                            settings.inner.html(data);
                            settings.partialView.show(0);
                            settings.inner.css('top', 0);
                            $.validator.unobtrusive.parse('#editor');
                            onRemoteComplete();
                        }
                    });
            });

            settings.deleteSubmit.on('click', function () {
                SpecialityEditor.deleteSpeciality();
            });

            settings.names.on('keypress', function (event) {
                if (String.fromCharCode(event.charCode) == '<' || String.fromCharCode(event.charCode) == '>')
                    return false;
            });


            settings.editor.children('input').prop('maxlength', 300);
            var payment = $("ul.tuition-fee li input");
            var salary = $("ul.salaries li input");
            salary.attr("min", 0);
            payment.attr("min", 0);

            settings.editor.submit(function (event) {
              

                if ($('#Name').val().length == 0) {
                    specialityName = $('#Name').val();
                    $("#js-validation").html(localStrings.ValidationNameCannotBeEmpty);
                    event.preventDefault();
                    return false;
                }

                var salaries = [1, 2, 3, 4, 5, 10, 20];
                for (var i = 0; i < salaries.length; i++) 
                    if (parseInt($('#Salaries_' + salaries[i] + '_').val()) < 0) {
                        $("#js-validation").html(localStrings.BanSalariesBelowZero);
                        event.preventDefault();
                        return false;
                    }
                var prices = ["_TopUniversityPrice_", "_MiddleUniversityPrice_", "_LowUniversityPrice_"];
                for (var i = 0; i < prices.length; i++)
                    if (parseInt($('#Prices_' + salaries[i] + '_').val()) < 0) {
                        $("#js-validation").html(localStrings.BanPaymentsBelowZero);
                        event.preventDefault();
                        return false;
                    }
                var data = settings.editor.serialize(),
                    url = settings.editor.attr("action"),
                    id = $('#Id').val(),
                    resize = resizeCards,
                    specialityName = '';
                if (!settings.editor.hasClass('addForm')) {
                    specialityName = "[" + $('#' + id).find('div').find('.name').html().replace(new RegExp("\n", 'g'), "").replace(new RegExp(" ", 'g'), "") + '] - ';
                }
                var revertZeroSubmit = function () {
                    var input = $('.salaries[value]')
                    var id = [1, 2, 3, 4, 5, 10, 20];
                    for (var i = 0; i < id.length; i++) {
                        if ($('#Salaries_' + id[i] + '_').val() == '') {
                            $('#Salaries_' + id[i] + '_').css('color', 'transparent').val('0');
                        }
                    };
                    if ($('#Prices_TopUniversityPrice_').val() == '') $('#Prices_TopUniversityPrice_').css('color', 'transparent').val('0');
                    if ($('#Prices_MiddleUniversityPrice_').val() == '') $('#Prices_MiddleUniversityPrice_').css('color', 'transparent').val('0');
                    if ($('#Prices_LowUniversityPrice_').val() == '') $('#Prices_LowUniversityPrice_').css('color', 'transparent').val('0');
                }
                revertZeroSubmit();
                $.post(url, data, function (d) {
                    if (d) {
                        Notificate(specialityName + localStrings.SpecialityChangeSuccess);
                        settings.partialView.hide();
                        var card = $('#' + id);
                        if (card.length) card.replaceWith(d);
                        else settings.addButton.after(d);
                        resize.reset();
                        resize();
                    }
                    else {
                        Notificate(specialityName + localStrings.SpecialityChangeFailed);
                    }

                });
                event.preventDefault();
            });

            settings.exitButton.click(function () {
                settings.partialView.hide();
            });

            settings.exit.on("click", function () {
                settings.partialView.hide();
            });

            $("#searchclear").click(function () {
                $("#search").val('');
                find();
            });

            settings.search.keydown(find);
        },
    }
})();

SpecialityEditor.init();

SpecialityEditor.resizeCards();
