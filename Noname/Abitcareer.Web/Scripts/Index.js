function DataProvider() {
    this.getData = function () {
        return JSON.parse(JSON.stringify(specialities[$("#spec").val()]));
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
        data: [0, 0, 800, 3000, 5000, 7000], //will be overrided
        color: "green"
    },
    {
        name: textStrings.summaryCosts,
        data: [0, 3416, 5124, 6832, 8540, 11040],
        color: "blue"
    },
    {
        name: textStrings.summarySalary,
        data: [0, 0, 9600, 36000, 60000, 84000],
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
        data: [1300, 2600, 3900, 5200, 6500, 8000],
        color: "blue"
    },
    {
        name: textStrings.summarySalary,
        data: [0, 0, 9600, 36000, 60000, 84000],
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
        name: textStrings.summarySalary,
        data: [0, 0, 9600, 36000, 60000, 84000],
        color: "green"
    },
    {
        name: textStrings.summaryCosts,
        data: [0, 0, 1200, 2900, 4900, 7400],
        color: "blue"
    }];
    $.post(textStrings.UrlGet, { polinom: 3 }, function (data) {
        specialities["pi"][1].data = data;
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
            data: [],
            stack: 'payment'
        };
        data3 = {
            name: textStrings.payment3Name,
            color: 'royalblue',
            data: [],
            stack: 'payment'
        };

        for (var i = 0; i < data1.data.length; i++) {
            var y2 = data1.data[i] * 0.8;
            var y3 = data1.data[i] * 0.6;
            data2.data.push(y2);
            data3.data.push(y3);
        }

        $("#input").fadeToggle(500);
        $("#chart-container").fadeToggle(500);

        window.location.hash = $("#spec").val();
        var chart = new Chart();
        chart.draw("#payments-container", [data1, data2, data3, selectedSpec[1]], textStrings.paymentsCaption, textStrings.xAxisCaption, textStrings.yAxisCaption, textStrings.dotCaption, valueTypes);
        chart.draw("#summary-container", [selectedSpec[2], selectedSpec[3]], textStrings.summaryCaption, textStrings.xAxisCaption, textStrings.yAxisCaption, textStrings.brinkCaprion, valueTypes);

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