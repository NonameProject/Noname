var Chart = (function () {

    var height = 400;

    var getLineIntersection = function (p0, p1, p2, p3) {
        var p0_x = p0.x,
        p0_y = p0.y,
        p1_x = p1.x,
        p1_y = p1.y,
        p2_x = p2.x,
        p2_y = p2.y,
        p3_x = p3.x,
        p3_y = p3.y;

        var s1_x, s1_y, s2_x, s2_y;
        s1_x = p1_x - p0_x; s1_y = p1_y - p0_y;
        s2_x = p3_x - p2_x; s2_y = p3_y - p2_y;

        var s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y),
            t = (s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

        if (s >= 0 && s <= 1 && t >= 0 && t <= 1) {
            return [p0_x + (t * s1_x), p0_y + (t * s1_y)];
        }

        return false;
    };

    var tuneChart = function (chart) {

        var customizeIntersectedData = function () {
            var s0 = chart.series[0].points,
            s1 = chart.series[1].points,
            s2 = chart.series[1];
            var n0 = s0.length,
                n1 = s1.length;
            var i, j, isect;
            var saveIsect;
            var mx = -100;
            for (i = 1; i < n0; i++) {
                for (j = 1; j < n1; j++) {
                    if (s0[i - 1].y > mx) mx = s0[i - 1].y;
                    if (s0[i].y > mx) mx = s0[i].y;
                    if (isect = getLineIntersection(s0[i - 1], s0[i],
                                        s1[j - 1], s1[j])) {
                        s2.addPoint(isect, false, false);
                        var ob;

                        saveIsect = isect;
                    }
                }

            }

            chart.yAxis[0].addPlotBand({
                inverted: true,
                from: saveIsect[1],
                to: mx,
                color: ' #FFEFD5',
            })
            chart.redraw();

            for (var p = 0; p < s2.data.length; p++) {
                if (s2.data[p].x == saveIsect[0] && s2.data[p].y == saveIsect[1]) {
                    s2.data[p].select();
                    chart.redraw();
                };
            }
        };

        chart.setSize($(".chartWrapper").width(), height, false);

        if (chart.series[0].points.length != 0 || chart.series[1].points.length != 0) {
            customizeIntersectedData();
        }

        $(window).resize(function () {
            chart.redraw();
            chart.setSize($(".chartWrapper").width(), height, false);
        });
    };

    return {
        draw: function (conteiner, dataObj, title, xAxisCaption, yAxisCaption, dotCaption, valueTypes) {
            $(conteiner).highcharts({
                chart: {
                    type: 'spline',
                    inverted: true,
                    zoomtype: "xy"
                },
                title: {
                    text: title
                },
                xAxis: {
                    reversed: false,
                    title: {
                        enabled: true,
                        text: xAxisCaption
                    },
                    labels: {
                        formatter: function () {
                            return this.value;
                        }
                    },
                    maxPadding: 0.1,
                    showLastLabel: true,
                    tickInterval: 1000
                },
                yAxis: {
                    categories: ['0'],
                    showEmpty: false,
                    title: {
                        text: yAxisCaption
                    },
                    labels: {
                        formatter: function () {
                            if (this.y === 0) {
                            } else {
                                return this.value;
                            }
                        }
                    },
                    lineWidth: 1,
                },
                legend: {
                        layout: 'vertical',
                        align: 'right',
                        verticalAlign: 'middle',
                        borderWidth: 0
                },
                tooltip: {
                    headerFormat: '<b>{series.name}</b><br/>',
                    formatter: function () {

                        var header = "<strong>" + this.series.name + "</strong><br/>";
                        if (this.series.color == "green")
                            return header + valueTypes.profit + ":{" + this.x.toFixed(1) + "}," + valueTypes.year + "{" + this.y.toFixed(1) + "}";
                        return header + valueTypes.costs + ":{" + this.x.toFixed(1) + "}," + valueTypes.year + "{" + this.y.toFixed(1) + "}";
                    }
                },
                plotOptions: {
                    spline: {
                        marker: {
                            enable: false
                        }
                    }
                },
                series: dataObj
            }, function (chart) { tuneChart(chart)});
        }
    };
})();
