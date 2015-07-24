﻿get_line_intersection = function(p0,p1,p2,p3)
{        
    var p0_x = p0.x,
        p0_y = p0.y,
        p1_x = p1.x,
        p1_y = p1.y,
        p2_x = p2.x,
        p2_y = p2.y,
        p3_x = p3.x,
        p3_y = p3.y;  

    var s1_x, s1_y, s2_x, s2_y;
    s1_x = p1_x - p0_x;     s1_y = p1_y - p0_y;
    s2_x = p3_x - p2_x;     s2_y = p3_y - p2_y;

    var s = (-s1_y * (p0_x - p2_x) + s1_x * (p0_y - p2_y)) / (-s2_x * s1_y + s1_x * s2_y),
        t = ( s2_x * (p0_y - p2_y) - s2_y * (p0_x - p2_x)) / (-s2_x * s1_y + s1_x * s2_y);

    if (s >= 0 && s <= 1 && t >= 0 && t <= 1)
    {
        return [p0_x + (t * s1_x),p0_y + (t * s1_y)];
    }

    return false;
}

function drawCharts(conteiner, dataObj, title, xAxisCaption, yAxisCaption, valueTypes) {
    renderTheme();
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
            //gridLineWidth: 0.5
            tickInterval: 1000,
        },
        yAxis: {
            title: {
                text: yAxisCaption
            },
            labels: {
                formatter: function () {
                    return this.value;
                }
            },
            lineWidth: 1,
        },
        legend: {
            enabled: false
        },
        tooltip: {
            headerFormat: '<b>{series.name}</b><br/>',
            formatter: function()
            {                   
                var header = "<strong>" + this.series.name +  "</strong><br/>";
                if (this.series.color == "green")
                    return header+ valueTypes.profit +":{" + this.x + "}," +  valueTypes.year +  "{" + this.y + "}";
                return header + valueTypes.costs+ ":{" + this.x + "},"  +  valueTypes.year+ "{" + this.y + "}";
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
    }, function (chart) {
        chart.setSize(
       $('.chartWrapper').width(),
       $('.chartWrapper').height(),
       false
    );
        var s0 = chart.series[0].points,
            s1 = chart.series[1].points,
            s2 = chart.series[1];
        var n0 = s0.length,
            n1 = s1.length;
        var i, j, isect;
        var saveIsect;
        var mx = -100;
        for (i = 1; i < n0; i++){
            for (j = 1; j < n1; j++) {
                if (s0[i - 1].y > mx) mx = s0[i - 1].y;
                if (s0[i].y > mx) mx = s0[i].y;
                if (isect = get_line_intersection(s0[i-1],s0[i],
                                    s1[j-1],s1[j])){
                    s2.addPoint(isect, false, false);
                    saveIsect = isect;
                }
            } 
        }
        chart.yAxis[0].addPlotBand({
            inverted: true,
            from: saveIsect[1],
            to: mx,
            color: 'rgba(68, 170, 213, .2)',
        })
        chart.redraw();
    });
}
function renderTheme() {
    Highcharts.createElement('link', {
        href: '//fonts.googleapis.com/css?family=Signika:400,700',
        rel: 'stylesheet',
        type: 'text/css'
    }, null, document.getElementsByTagName('head')[0]);

    // Add the background image to the container
    Highcharts.wrap(Highcharts.Chart.prototype, 'getContainer', function (proceed) {
        proceed.call(this);
        this.container.style.background = 'url(http://www.highcharts.com/samples/graphics/sand.png)';
    });


    Highcharts.theme = {
        colors: ["#f45b5b", "#8085e9", "#8d4654", "#7798BF", "#aaeeee", "#ff0066", "#eeaaee",
           "#55BF3B", "#DF5353", "#7798BF", "#aaeeee"],
        chart: {
            backgroundColor: null,
            style: {
                fontFamily: "Signika, serif"
            }
        },
        title: {
            style: {
                color: 'black',
                fontSize: '16px',
                fontWeight: 'bold'
            }
        },
        subtitle: {
            style: {
                color: 'black'
            }
        },
        tooltip: {
            borderWidth: 0
        },
        legend: {
            itemStyle: {
                fontWeight: 'bold',
                fontSize: '13px'
            }
        },
        xAxis: {
            labels: {
                style: {
                    color: '#6e6e70'
                }
            }
        },
        yAxis: {
            labels: {
                style: {
                    color: '#6e6e70'
                }
            }
        },
        plotOptions: {
            series: {
                shadow: true
            },
            candlestick: {
                lineColor: '#404048'
            },
            map: {
                shadow: false
            }
        },

        // Highstock specific
        navigator: {
            xAxis: {
                gridLineColor: '#D0D0D8'
            }
        },
        rangeSelector: {
            buttonTheme: {
                fill: 'white',
                stroke: '#C0C0C8',
                'stroke-width': 1,
                states: {
                    select: {
                        fill: '#D0D0D8'
                    }
                }
            }
        },
        scrollbar: {
            trackBorderColor: '#C0C0C8'
        },

        // General
        background2: '#E0E0E8'

    };

    // Apply the theme
    Highcharts.setOptions(Highcharts.theme);
}