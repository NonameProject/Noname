get_line_intersection = function(p0,p1,p2,p3)
{        
    var p0_x = p0.x;
    var p0_y = p0.y;
    var p1_x = p1.x;
    var p1_y = p1.y;
    var p2_x = p2.x;
    var p2_y = p2.y;
    var p3_x = p3.x;
    var p3_y = p3.y;  

    var s1_x, s1_y, s2_x, s2_y;
    s1_x = p1_x - p0_x;     s1_y = p1_y - p0_y;
    s2_x = p3_x - p2_x;     s2_y = p3_y - p2_y;

    var s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y);
    var t = ( s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

    if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
    {
        return [p0_x + (t * s1_x),p0_y + (t * s1_y)];
    }

    return false;
}

$(function () {
    var obj = new Object();
    obj = [{
        name: 'Плата за обучение',
        data: [[900, 1], [1100, 2], [1200, 3], [1300, 4], [1400, 5]],
        color: "red"
    },
    {
        name: 'Уровень зарплаты',
        data: [[800, 1], [800, 2], [3000, 3], [5000, 4], [7000, 5]],
        color: "green"
    }]
    drawCharts("#container", obj, "Уровень платы за обучение и уровень зарплаты спустя N лет после начала учебы");
    drawCharts("#container1", obj, "Cуммарные затраты на образование и суммарный заработок спустя N лет после начала учебы");
    function drawCharts(conteiner, dataObj, title) {
        $(conteiner).highcharts({
            chart: {
                type: 'spline',
                inverted: true
            },
            title: {
                text: title
            },
            subtitle: {
                text: 'Очень крутой график'
            },
            xAxis: {
                reversed: false,
                title: {
                    enabled: true,
                    text: 'Деньги (грн)'
                },
                labels: {
                    formatter: function () {
                        return this.value;
                    }
                },
                maxPadding: 0.1,
                showLastLabel: true,
                //gridLineWidth: 0.5
                tickInterval: 1000
            },
            yAxis: {
                title: {
                    text: "Время(лет)"
                },
                labels: {
                    formatter: function () {
                        return this.value;
                    }
                },
                lineWidth: 1
            },
            legend: {
                enabled: false
            },
            tooltip: {
                headerFormat: '<b>{series.name}</b><br/>',
                pointFormat: 'прибыль: {point.x}, год: {point.y}'
            },
            plotOptions: {
                spline: {
                    marker: {
                        enable: false
                    }
                }
            },
            series: dataObj
        }, function(chart){
            var s0 = chart.series[0].points;
            var s1 = chart.series[1].points;
            var s2 = chart.series[1];
            var n0 = s0.length;
            var n1 = s1.length;
            var i,j,isect;
            for (i = 1; i < n0; i++){
                for (j = 1; j < n1; j++){
                    if (isect = get_line_intersection(s0[i-1],s0[i],
                                        s1[j-1],s1[j])){
                        s2.addPoint(isect, false, false);

                    }
                } 
            }
            chart.redraw();
        });
    }
});