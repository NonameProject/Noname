﻿function DataProvider() {
    var getYearSumm = function (data) {
        var res = [0];
        for (var i = 0; i < data.length; i++)
            if (typeof data[i].y === 'number')
                res.push(res[i] + data[i].y * 12);
            else res.push(res[i] + data[i] * 12);
        return res;
    };

    this.getData = function () {
        var res = JSON.parse(JSON.stringify(specialities[$("#spec").val()]));

        if (!res[2].data.length)
            res[2].data = getYearSumm(res[0].data);
        if (!res[3].data.length)
            res[3].data = getYearSumm(res[1].data);

        var length = res[3].data.length - 1;
        if (typeof res[3].pointStart === 'numder')
            length += res[3].pointStart;
        res[2].data.push({ x: length, y: res[2].data[res[2].data.length - 1] });

        return res;
    };

    this.MultiplieData = function (data, coef) {
        var data2 = [];
        for (var i = 0; i < data.length; i++) {
            var y2 = data[i] * coef;
            data2.push(y2);
        }
        return data2;
    };

    var specialities = {
        "ki": {},
        "pi": {},
        "ci": {}
    };
    specialities["pi"] = [{
        name: textStrings.paymentsAxis,
        data: [1708, 1708, 1708, 1708, 2500],
        color: "#990033"// was red,        
    },
    {
        name: textStrings.summaryAxis,
        data: [0, 0, 1500, 2800, 4000, 5200], //will be overrided
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

    specialities["ci"] = [{
        name: textStrings.paymentsAxis,
        data: [1300, 1300, 1300, 1300, 1300, 1500],
        color: "#990033"// was red
    },
    {
        name: textStrings.summaryAxis,
        data: [0, 0, 800, 3000, 5000, 7000],
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

    specialities["ki"] = [{
        name: textStrings.paymentsAxis,
        data: [1200, 1200, 1200, 1200, 1200, 1400],
        color: "#990033"
    },
    {
        name: textStrings.summaryAxis,
        data: [0, 0, 1200, 1700, 2000, 2500],
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
    $.post(textStrings.UrlGet, { polinom: 3 }, function (data) {
        specialities["ki"][3].data = getYearSumm(data);
    });
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
        if ($("#spec").val() === "noData")
            return false;

        var selectedSpec = provider.getData();
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

        $("#input").fadeToggle(500);
        $("#chart-container").fadeToggle(500);

        window.location.hash = $("#spec").val();
        var chart = new Chart();
        $('#selectedSpeciality').html($("#spec option:selected").html());
        chart.draw("#payments-container", [data1, data2, data3, selectedSpec[1]], textStrings.paymentsCaption, textStrings.xAxisCaption, textStrings.yAxisCaption, textStrings.dotCaption, valueTypes);        
        chart.draw("#summary-container", [selectedSpec[2], selectedSpec[3]], textStrings.summaryCaption, textStrings.xAxisCaption, textStrings.yAxisCaption, textStrings.brinkCaprion, valueTypes, null, ['red', 'red', 'red']);

        setHash();
        return false;
    };

    if (window.location.hash) {
        $("#spec").val(window.location.hash.slice(1));
        draw();
    }

    $("#js-changeInput").click(function () {
        window.location.hash = '';
        setHash();
        $("#input").fadeToggle(500);
        $("#chart-container").fadeToggle(500);
    });

    $("#commit").on("click", draw);
})