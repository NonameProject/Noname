function DataProvider() {

    this.getData = function (callback) {
        var res = JSON.parse(JSON.stringify(specialities));
        $("#js-loading-screen").toggleClass("active");
        $.post(textStrings.UrlGet, { id: $("#spec").val() }, function (data) {
            res[0].data = data[0];
            res[1].data = data[1];
            res[2].data = data[2];
            res[3].data = data[3];

            var length = res[3].data.length - 1;
            if (typeof res[3].pointStart === 'numder') {
                length += res[3].pointStart;                
            }
            res[2].data.push({ x: length, y: res[2].data[res[2].data.length - 1].y });
            res[0].data.push({ x: res[0].data[res[0].data.length - 1].x, y: 0 });
            $("#js-loading-screen").toggleClass("active");
            callback(res);
        });
    };

    this.MultiplieData = function (data, coef) {
        var data2 = [];
        for (var i = 0; i < data.length; i++) {
            var x2 = data[i].x,
                y2 = data[i].y * coef;
            data2.push({ x: x2, y: y2 });
        }
        return data2;
    };

    var specialities = [{
        name: textStrings.paymentsAxis,
        data: [],
        color: "#990033"// was red,        
    },
    {
        name: textStrings.summaryAxis,
        data: [],
        color: "green"
    },
    {
        name: textStrings.summaryCosts,
        data: [],
        color: "blue"
    },
    {
        name: textStrings.summarySalary,
        data: [],
        color: "green"
    }];
}

$(function () {
    setHash();

    var valueTypes = {
        costs: textStrings.Costs,
        year: textStrings.Year,
        oneYear: textStrings.OneYear,
        fewYears: textStrings.FewYears,
        manyYears: textStrings.ManyYears,
        UAH: textStrings.UAH,
        profit: textStrings.Profit
    };

    var provider = new DataProvider();

    var draw = function () {
        $("title").html("AbitCareer | " + textStrings.localizationPageCharts);
        if ($("#spec").val() === "noData" || $("#commit").hasOwnProperty("disabled"))
            return false;
        var butt = $('#commit');
        butt.prop('disabled', true);

        provider.getData(function (selectedSpec) {
            var data1, data2, data3;

            data1 = {
                name: textStrings.payment1Name,
                color: 'darkblue',
                data: selectedSpec[0].data,
                stack: 'payment'
            };
            data2 = {
                name: textStrings.payment2Name,
                color: 'blue',
                data: provider.MultiplieData(data1.data, 0.8),
                stack: 'payment'
            };
            data3 = {
                name: textStrings.payment3Name,
                color: 'royalblue',
                data: provider.MultiplieData(data1.data, 0.6),
                stack: 'payment'
            };            

            window.location.hash = $("#spec").val();
            var chart = new Chart();
            //$('#selectedSpeciality').html($("#spec option:selected").html());

            butt.prop('disabled', false);
            if ($("#commit").css("display") != "none") {
                $(".toFade").fadeToggle(500);
                $("#chart-container").fadeToggle(500);
            }

            chart.draw("#payments-container", [data1, data2, data3, selectedSpec[1]], textStrings.paymentsCaption, textStrings.xAxisCaption, textStrings.yAxisCaption, textStrings.dotCaption, valueTypes);

            data1.data = selectedSpec[2].data;
            data2.data = provider.MultiplieData(data1.data, 0.8);
            data3.data = provider.MultiplieData(data1.data, 0.6);

            chart.draw("#summary-container", [data1, data2, data3, selectedSpec[3]], textStrings.summaryCaption, textStrings.xAxisCaption, textStrings.yAxisCaption, textStrings.brinkCaprion, valueTypes, ['#C9F76F', '#C0F56E', '#ACF53D']);

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
    }
    
    $("#spec").on("change", function () {
        if ($("#commit").css("display") == "none")
            draw();
    });

    $("#commit").on("click", draw);
})