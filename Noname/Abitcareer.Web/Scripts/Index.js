function DataProvider() {
    this.getData = function (callback) {
        $("#js-loading-screen").addClass("active");
        $.post('[Route:GetData]', { id: $("#spec").val() }, function (data) {
            var res = [];

            for (var i = 0; i < data.length; i++) {
                res[i] = {};
                res[i].data = data[i];
            }

            var length = res[data.length - 1].data.length - 1;
            length = res[data.length - 1].data[length].x;

            for (var i = 0; i < data.length - 2; i++) {
                if (i < 3) {
                    res[i].data.push({ x: res[i].data[res[i].data.length - 1].x, y: 0 });
                }
                else {
                    res[i].data.push({ x: length, y: res[i].data[res[i].data.length - 1].y });
                }
            }

            callback(res);
        })
            .always(function () {
                $("#js-loading-screen").removeClass("active");
            });
    };
}

$(function () {
    setHash();

    var urlContainsSpecialityHash = /[\w\s]*([\w]{8}-[\w]{4}-[\w]{4}-[\w]{4}-[\w]{12})/gi.test(document.location.href.split('#')[1]);
    if (!urlContainsSpecialityHash)
        $("#js-loading-screen").removeClass("active");

    $('a.changeLanguage:not([href*="#"])').click(function () {
        $("#js-loading-screen").addClass("active");
    });

    var valueTypes = {
        costs: '[Resx:Costs]',
        year: '[Resx:Year]',
        oneYear: '[Resx:OneYear]',
        fewYears: '[Resx:FewYears]',
        manyYears: '[Resx:ManyYears]',
        UAH: '[Resx:UAH]',
        profit: '[Resx:Profit]'
    };

    var provider = new DataProvider();

    var draw = function () {
        $("#js-loading-screen").removeClass("active");
        var spec = $("#spec");
        if (spec.val() === "noData" || $("#commit").hasOwnProperty("disabled"))
            return false;        
        $("title").html(spec.find('option:selected').html() + ' - AbitCareer');
        var butt = $('#commit');
        butt.prop('disabled', true);

        provider.getData(function (selectedSpec) {
            var length = selectedSpec.length;

            data1 = {
                name: '[Resx:FirstPaymentName]',
                color: 'darkblue',
                data: selectedSpec[0].data,
                stack: 'payment'
            };
            data2 = {
                name: '[Resx:SecondPaymentName]',
                color: 'blue',
                data: selectedSpec[1].data,
                stack: 'payment'
            };
            data3 = {
                name: '[Resx:ThirdPaymentName]',
                color: 'royalblue',
                data: selectedSpec[2].data,
                stack: 'payment'
            };
            data4 = {
                name: '[Resx:SummaryGraphCaption]',
                color: 'green',
                data: selectedSpec[length - 2].data
            };

            window.location.hash = $("#spec").val();
            var chart = new Chart();

            butt.prop('disabled', false);
            if ($("#commit").css("display") != "none") {
                $(".toFade").fadeToggle(500);
                $("#chart-container").fadeToggle(500);
            }

            chart.draw("#payments-container", [data1, data2, data3, data4], '[Resx:PaymentsCaption]', '[Resx:xChartAxisCaption]', '[Resx:yChartAxisCaption]', '[Resx:DotCaption]', valueTypes);

            data1.data = selectedSpec[3].data;
            data2.data = selectedSpec[4].data;
            data3.data = selectedSpec[5].data;
            data4.data = selectedSpec[length - 1].data;
            data4.name = '[Resx:SummarySalary]';

            chart.draw("#summary-container", [data1, data2, data3, data4], '[Resx:SummaryCaption]', '[Resx:xChartAxisCaption]', '[Resx:yChartAxisCaption]', '[Resx:BrinkCaption]', valueTypes, ['#C9F76F', '#C0F56E', '#ACF53D']);

            setHash();
        });

        return false;
    };

    if (window.location.hash) {
        var spec = $("#spec"),
            hash = window.location.hash.slice(1);
        if (spec.find('option[value="' + hash + '"]').length) {
            spec.val(hash);
            draw();
        }
        else
        {
            $("#js-loading-screen").removeClass("active");
        }
    }
    
    $("#spec").on("change", function () {
        if ($("#commit").css("display") == "none")
            draw();
    });

    $("#commit").on("click", draw);
})