
var Chart = (function () {

    var cross1,
        cross2;

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
            var retData = new Object();
            cross1 = retData[0] = addPointToLine(chart.series[0].points, chart.series[1].points, chart.series[1]);
            cross2 = retData[1] = addPointToLine(chart.series[2].points, chart.series[3].points, chart.series[3]);

            addPlotChart(retData[0], '#FFEFD5');
            addPlotChart(retData[1], '#97ABF0');
            chart.redraw();
            selectPoint(chart.series[1], retData[0]);
            selectPoint(chart.series[3], retData[1]);
            chart.redraw();
        };

        var addPlotChart = function (data, col) {
            chart.yAxis[0].addPlotBand({
                inverted: true,
                from: data.saveIsect[1],
                to: data.mx,
                color: col,
            })
        };

        var selectPoint = function (line, retData) {
            for (var p = 0; p < line.data.length; p++) {
                if (line.data[p].x == retData.saveIsect[0] && line.data[p].y == retData.saveIsect[1]) {
                    line.data[p].select(true,true);
                    chart.redraw();
                };
            }
        };

        var addPointToLine = function (linePoints1, linePoints2, linePointsTo) {
            var n0 = linePoints1.length,
            n1 = linePoints2.length;
            var i, j, isect, isect2;
            var saveIsect;
            var mx = -100;
            for (i = 1; i < n0; i++) {
                for (j = 1; j < n1; j++) {
                    if (linePoints1[i - 1].y > mx) mx = linePoints1[i - 1].y;
                    if (linePoints1[i].y > mx) mx = linePoints1[i].y;
                    if (isect = getLineIntersection(linePoints1[i - 1], linePoints1[i],
                                        linePoints2[j - 1], linePoints2[j])) {
                        linePointsTo.addPoint(isect, false, false);
                        var ob;
                        saveIsect = isect;
                    }
                }
            }
            var ret = new Object();
            ret.saveIsect = saveIsect;
            ret.mx = mx;
            return ret;
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
        draw: function (conteiner, dataObj, title, xAxisCaption, yAxisCaption, dotCaption,brinkCaption, valueTypes) {
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
                        if (cross1 && cross2) {
                            if (this.point.x == cross1.saveIsect[0] && this.point.y === cross1.saveIsect[1]) return dotCaption;
                            if (this.point.x == cross2.saveIsect[0] && this.point.y === cross2.saveIsect[1]) return  brinkCaption;
                        }
                        return header + valueTypes.costs + ":{" + this.x.toFixed(1) + "}," + valueTypes.year + "{" + this.y.toFixed(1) + "}";
                    }
                },
                plotOptions: {
                    spline: {
                        marker: {
                            enable: false
                        }
                    },
                    series: {
                        dataLabels: {
                            enabled: true,
                            formatter: function () {
                                if (!cross1 || !cross2) return;
                                if (this.point.x == cross1.saveIsect[0] && this.point.y === cross1.saveIsect[1]) return ;
                                if (this.point.x == cross2.saveIsect[0] && this.point.y === cross2.saveIsect[1]) return ;
                            }
                        }
                    }
                },
                series: dataObj
            }, function (chart) { tuneChart(chart)});
        }
    };
})();
