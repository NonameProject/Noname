SpecialityEditor = (function () {

    var settings = {},
        localStrings = {},
        ItemId = 0,
        oldWidth = 0,
        saveThisSpecialityName = '',
        prevTop = 0;

    var onDocumentScroll = function (e) {
        var currentTop = $(this).scrollTop();
        if (settings.partialView.css('display') == 'none') {
            prevTop = currentTop;
            return;
        }

        if (currentTop == prevTop) return;

        var containerHeight = settings.partialView.height(),
            height = settings.inner.innerHeight();
        if (height <= containerHeight) {
            settings.inner.css('top', 0);
            return;
        }

        var pos = parseInt(settings.inner.css('top'));
        pos -= currentTop - prevTop;
        if (pos > 0) pos = 0;
        else if (height + pos <= containerHeight) pos = containerHeight - height;

        prevTop = currentTop;
        settings.inner.css('top', pos);
    };

    var setNameStatus = function () {
        var res = false;
        $('#Name').rules().remote.complete = function (xhr) {
            $("span[data-valmsg-for='Name']").removeClass();
            if ((xhr.status == 200 && xhr.responseText === 'true') || $("#Name").val() == settings.saveThisSpecialityName) {
                $("span[data-valmsg-for='Name']").addClass("glyphicon glyphicon-ok success");
                $("#saveButton").prop("disabled", false);
            }
            else {
                $("span[data-valmsg-for='Name']").attr("data-toggle", "tooltip").attr("title", localStrings.ErrorValidationNameTooltip).attr("data-placement", "bottom").addClass("glyphicon glyphicon-remove failure");
                res = false;
                $("#saveButton").prop("disabled", true);
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

    var getSearchResult = function () {
        clearTimeout(ItemId);
        setTimeout(function (settings) {
            if (!settings.getSearchResult.val()) $("#searchclear").hide();
            else
                $("#searchclear").show();
        }, 10, settings);
        $(this).removeClass('success', 100);
        ItemId = setTimeout(function () {
            var value = settings.getSearchResult.val();
            if (!value || !value.trim()) {
                $('#search').addClass('success');
                $(".cardWrapper").show(0);
                return;
            }
            $.ajax(
                {
                    url: "[Route:searchForSpeaciality]",
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

    var bindEditorFormOpening = function()
    {
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
            $.get('[Route:editSpeciality]', { id: $(this).attr('id') }, function (data) {
                if (!data) {
                    settings.partialView.hide(0);
                    NotificationApi.Failure('[Resx:SpecialityOpenFailed]');
                }
                else {
                    settings.inner.css('top', 0);
                    settings.inner.html(data);
                    $.validator.unobtrusive.parse('#editor');
                    settings.saveThisSpecialityName = $("#Name").val();
                    setNameStatus();
                }
            }).fail(function () {
                settings.partialView.hide(0);
            });
        });
    }

    var bindAddFormOpening = function()
    {
        settings.addButton.on("click", function (event) {
            settings.inner.empty();
            event.stopPropagation();
            settings.partialView.show(0);
            $.get("[Route:addSpeciality]", function (data) {
                settings.inner.html(data);
                settings.inner.css('top', 0);
                $.validator.unobtrusive.parse('#editor');
                setNameStatus();
                if (!$("#Name").val()) {
                    $("#createSpeciality").prop("disabled", false);
                }
            }).fail(function () {
                settings.partialView.hide(0);
            });
        });
    }

    var disableBracketsInput = function()
    {
        settings.names.on('keypress', function (event) {
            if (String.fromCharCode(event.charCode) == '<' || String.fromCharCode(event.charCode) == '>')
                return false;
        });
    }

    var bindDOMAttributes = function()
    {
        settings.salary.prop("min", 0).attr("min", 0);

        settings.payment.prop("min", 0).attr("min", 0);

        settings.editor.children('input').prop('maxlength', 300);
    }

    var isSpecialityNameAvailable = function()
    {
        if ($('#Name').val().length == 0) {
            specialityName = $('#Name').val();
            $("#js-validation").html(localStrings.ValidationNameCannotBeEmpty);
            event.preventDefault();
            return false;
        }
        return true;
    }

    var isSalaryValuesValid = function()
    {
        var salaries = [1, 2, 3, 4, 5, 10, 20];

        for (var i = 0; i < salaries.length; i++) {
            if (parseInt($('#Salaries__' + salaries[i] + '_').val()) < 0) {
                $("#js-validation").html(localStrings.BanSalariesBelowZero);
                event.preventDefault();
                return false;
            }
        }
        return true;
    }

    var isPriceValuesValid = function()
    {
        if (parseInt($("#Prices__LowUniversityPrice_").val()) < 0 || parseInt($("#Prices__MiddleUniversityPrice_").val()) < 0 || parseInt($("#Prices__TopUniversityPrice_").val()) < 0) {
            $("#validation-payment-salaries").html(localStrings.BanPaymentsBelowZero);
            event.preventDefault();
            return false;
        }
        return true;
    }

    var bindEditionFormSubmiting = function()
    {
        settings.editor.submit(function (event) {
            if (!isSpecialityNameAvailable() || !isSalaryValuesValid() || !isPriceValuesValid())
                return;

            var data = settings.editor.serialize(),
                url = settings.editor.attr("action"),
                id = $('#Id').val(),
                resize = resizeCards,
                specialityName = '';

            if (!settings.editor.hasClass('addForm')) {
                specialityName = "[" + $('#' + id).find('div').find('.name').html().replace(new RegExp("\n", 'g'), "").replace(new RegExp(" ", 'g'), "") + '] - ';
            }

            $.post(url, data, function (d) {
                if (d) {
                    NotificationApi.Success(specialityName + localStrings.SpecialityChangeSuccess);
                    settings.partialView.hide();
                    var card = $('#' + id);
                    if (card.length) card.replaceWith(d);
                    else settings.addButton.after(d);
                    resize.reset();
                    resize();
                }
                else {
                    NotificationApi.Failure(specialityName + localStrings.SpecialityChangeFailed);
                }

            });

            event.preventDefault();
        });
    }

    var bindButtonActions = function()
    {
        settings.deleteDiscard.on('click', function () {
            settings.deleteConfirm.hide(0);
        });

        $(document).on('click', function (event) {
            if ($(event.target).closest(settings.inner).length) return;
            settings.partialView.hide();
            event.stopPropagation();
        });

        settings.deleteSubmit.on('click', function () {
            SpecialityEditor.deleteSpeciality();
        });

        settings.exit.on("click", function () {
            settings.partialView.hide();
        });

        $("#searchclear").click(function () {
            $("#search").val('');
            getSearchResult();
        });

        settings.exitButton.click(function () {
            settings.partialView.hide();
        });
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
                        NotificationApi.Success("[" + name + "] - [Resx:SpecialityRemoveSuccess]");
                        $("#" + id).remove();
                    },
                    error: function () {
                        NotificationApi.Failure("[" + name + "] - [Resx:SpecialityRemoveFailed]");
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

            settings.getSearchResult = $("#search");

            settings.payment = $("ul.tuition-fee li input");

            settings.salary = $("ul.salaries li input");

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
            settings.getSearchResult.off();
        },

        bindUIActions: function () {
            bindEditorFormOpening();

            bindAddFormOpening();

            bindEditionFormSubmiting();

            disableBracketsInput();

            bindDOMAttributes();

            bindButtonActions();

            $(document).scroll(onDocumentScroll);

            $(document).ajaxStart(function () {
                $("body").toggleClass("loading");

            }).ajaxStop(function () {
                $("body").toggleClass("loading");
            })

            settings.getSearchResult.keyup(getSearchResult);
        },
    }
})();

SpecialityEditor.init();

SpecialityEditor.resizeCards();
