
SpecialityEditor = (function(){

    var settings = {};

    var localStrings = {};

    var searchItemId = 0;

    var oldWidth = 0;
   
    return {


            resizeCards: function () {
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
            },

            getCulture: function () {
                return document.location.href.split('/')[3].toLowerCase();
            },

            requestDeletion:  function (id) {
            window.event.stopPropagation();
            settings.deleteSubmit.data("target", id);
            settings.deleteConfirm.show(0);
        },

            deleteSpeciality: function () {
            var id = settings.deleteSubmit.data("target");
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
            settings.deleteConfirm.hide(0);
        },

            init: function (localResourses) {

                settings.inner = $("#inner");

                settings.partialView = $("#partialView");

                settings.deleteDiscard = $(".deleteDiscard");

                settings.deleteConfirm = $("#deleteConfirm");

                settings.deleteSubmit = $(".deleteSubmit");

                settings.addButton = $("#addNew");

                settings.names = $("#Name, EnglishName");

                settings.editor = $("#editor");

                settings.exitButton = $("#exitButton");

                settings.exit = $("#exit");

                settings.search = $("#search");

                localStrings = localResourses;

                this.bindUIActions();
            },

            bindUIActions: function () {

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
                            settings.inner.empty();
                            settings.partialView.show(0);
                            settings.inner.html(data);
                        }
                    }
                    );
                });

                settings.deleteDiscard.click(function () {
                    settings.deleteConfirm.hide(0);
                });

                $(document).click(function (event) {
                    if ($(event.target).closest(settings.inner).length) return;
                    settings.partialView.hide();
                    event.stopPropagation();
                });

                $('body').on("mouseover", ".card:not(#addNew)", function () {
                    $(this.children[0]).show();
                });

                $("body").on("mouseout", ".card:not(#addNew)", function () {
                    $(this.children[0]).hide();
                });

                settings.addButton.on("click", function (event) {
                    settings.inner.empty();
                    event.stopPropagation();
                    $.ajax(
                        {
                            url: "specialities/add",
                            success: function (data) {
                                settings.inner.html(data);
                                settings.partialView.show(0);

                            },
                            error: function (e) {
                                alert("error" + e.status);
                            }
                        });
                });

                settings.deleteSubmit.on('click', function () {
                    SpecialityEditor.deleteSpeciality();
                });

                settings.names.keypress(function (event) {
                    if (String.fromCharCode(event.charCode) == '<' || String.fromCharCode(event.charCode) == '>')
                        return false;
                });

                $("ul.salaries li input").attr("min", 0);

                settings.editor.submit(function (event) {
                    if ($('#Name').val().length == 0 || $('#EnglishName').val().length == 0) {
                        $("#js-validation").html(localStrings.ValidationNameCannotBeEmpty);
                        event.preventDefault();
                        return false;
                    }
                    var data = settings.editor.serialize();
                    var url = settings.editor.attr("action");
                    $.post(url, data, function (d) {
                        if (d) {
                            Notificate(localStrings.SpecialityAdditingSuccess);
                            settings.partialView.hide();
                            settings.addButton.after(d);
                        }
                        else {
                            Notificate(localStrings.SpecialityAdditingFailed);
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

                settings.search.keydown(function () {
                    clearTimeout(id);
                    $(this).removeClass('success', 100);
                    id = setTimeout(function () {
                        var value = settings.search.val();
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
            },
        }
    })();
    
SpecialityEditor.init();

SpecialityEditor.resizeCards();

    

