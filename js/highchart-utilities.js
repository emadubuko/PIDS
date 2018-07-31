
function percentage(num, per) {
    var result = num * (per / 100);
    return Math.round(result);
}

Highcharts.getOptions().colors = Highcharts.map(Highcharts.getOptions().colors, function (color) {
    return {
        radialGradient: {
            cx: 0.5,
            cy: 0.3,
            r: 0.7
        },
        stops: [
            [0, color],
            [1, Highcharts.Color(color).brighten(-0.3).get('rgb')] // darken
        ]
    };
});

Highcharts.setOptions({
    lang: {
        decimalPoint: '.',
        thousandsSep: ','
    }
});

var pieColors = (function () {
    var colors = [],
        i;

    for (i = 0; i < 50; i += 3) {
        base = Highcharts.getOptions().colors[i],
            colors.push(Highcharts.Color(base).get());
    }
    return colors;
}());


function BuildDonut(data) { //id, title, data_array) {

    var chart = new Highcharts.Chart({
        chart: {
            renderTo: data.contanerid,
            type: 'pie'
        },
        credits: false,
        title: {
            text: title,
            style: {
                fontSize: '12px'
            }
        },
        legend: {
            reversed: true
        },
        plotOptions: {
            pie: {
                colors: pieColors,
                shadow: false,
                dataLabels: {
                    format: '{point.y:,.0f}<br /> ({point.percentage:.1f} %)'
                }
            }
        },
        tooltip: {
            pointFormat: '{point.y} ({point.percentage:.1f}%)'
        },
        series: [{
            data: data.data_array,
            size: '60%',
            innerSize: '50%',
            showInLegend: true,

        }],
        responsive: {
            rules: [{
                condition: {
                    maxWidth: 400
                }
            }]
        }
    });
}

function Build_Pie_Chart(data) { //id, title, data_array) {
    Highcharts.chart('container', {
        chart: {
            plotBackgroundColor: null,
            plotBorderWidth: null,
            plotShadow: false,
            type: 'pie'
        },
        credits : false,
        title: {
            text: data.title,
            style: {
                fontSize: '12px'
            }
        },
        tooltip: {
            pointFormat: '{point.y} ({point.percentage:.1f}%)'
        },
        plotOptions: {
            pie: {
                allowPointSelect: true,
                cursor: 'pointer',
                dataLabels: {
                    format: '{point.y:,.0f} ({point.percentage:.1f} %)'
                },
                showInLegend: true,
            }
        },
        series: [{
            data: data.data_array,
        }],
        responsive: {
            rules: [{
                condition: {
                    maxWidth: 400
                }
            }]
        },
        colors: pieColors //['grey', '#F2784B', '#08bf0f'],//'#1ba39c'
    });
}

function Build_Pos_Neg_Chart(data) { //id, title, categories, series_data, max) {

    Highcharts.chart('container', {
        chart: {
            type: 'bar'
        },
        credits: false,
        title: {
            text: data.title,
            style: {
                fontSize: '12px'
            }
        },
        xAxis: [{
            categories: data.categories,
            reversed: true,
            labels: {
                step: 1
            }
        }, { // mirror axis on right side
            opposite: true,
            reversed: true,
            categories: data.categories,
            linkedTo: 0,
            labels: {
                step: 1
            }
        }],
        yAxis: {
            title: {
                text: null
            },
            labels: {
                formatter: function () {
                    return Math.abs(this.value);
                }
            },
            max: (max + percentage(max, 10)),
            min: -(max + percentage(max, 10))
        },
        plotOptions: {
            series: {
                stacking: 'normal',
            }
        },
        tooltip: {
            formatter: function () {
                return '<b>' + this.series.name + ', age ' + this.point.category + '</b><br/>' +
                    'Total: ' + Highcharts.numberFormat(Math.abs(this.point.y), 0);
            }
        },
        series: data.series_data
    });
}

function build_drilldown_bar_chart(data) {//id, title, yaxistitle, principal_data, drill_down_data, addSigntovalue = "") {


    var colors = get_color_shades(2);


    Highcharts.chart('container', {

        chart: {
            type: 'column'
        },
        title: {
            text: data.title,
            style: {
                fontSize: '12px'
            }
        },
        credits: false,
        subtitle: {
            text: 'Click the columns to drill down'
        },
        xAxis: {
            type: 'category'
        },
        yAxis: {
            title: {
                text: data.yaxistitle
            }
        },
        legend: {
            enabled: false
        },
        plotOptions: {
            series: {
                colors: colors,
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    format: '{point.y:, 1f} ' + data.addSigntovalue
                    //format: '{point.y:.1f}%'
                },

            }
        },

        tooltip: {
            headerFormat: '<span style="font-size:11px">{series.name}</span><br>',
            pointFormat: '<span style="color:{point.color}">{point.name}</span>: <b>{point.y}' + addSigntovalue + '</b> '
        },

        series: [{
            name: ' State',
            colorByPoint: true,
            data: data.principal_data
        }],
        drilldown: {
            series: data.drill_down_data
        }
    });
}

function build_Column_chart(data) { //id, title, yaxistitle, xaxisCategory, data, average_value, height) {
    var colors = get_color_shades(2);
    Highcharts.chart(id, {
        chart: {
            type: 'column',
            //height: height
        },
        title: {
            text: data.title,
            style: {
                fontSize: '12px'
            }
        },
        credits: false,
        xAxis: {
            categories: data.xaxisCategory,
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: data.yaxistitle,
            },
            plotLines: [{
                color: 'red',
                value: data.average_value,
                width: '1',
                zIndex: 2
            }]
        },
        legend: {
            enabled: false
        },
        tooltip: {
            pointFormat: '<b>{point.y:, 1f}</b>',
        },
        colors: colors,
        plotOptions: {
            column: {
                pointPadding: 0.2,
                borderWidth: 0
            },
            series: {
                borderWidth: 0,
                dataLabels: {
                    enabled: true,
                    format: '{point.y:, 1f}'
                }
            }
        },
        series: [{
            data: data.data
        }]
    });
}

function build_bar_chart_dual_axis(data) {//container_id, title, y1_title, y2_title, xaxisCategory, parent_data, parent_data_name, child_data, child_data_name, percent_data, percent_data_name, useLine=true,height) {

    Highcharts.chart('container', {
        chart: {
            zoomType: 'xy',
            //height: height
        },
        title: {
            text: title,
            style: {
                fontSize: '12px'
            }
        },
        credits: false,
        xAxis: [{
            categories: data.xaxisCategory,
            crosshair: true
        }],
        yAxis: [
            { // Secondary yAxis
                title: {
                    text: data.y1_title,
                    rotation: 270,
                },
                labels: {
                    format: '{value}',
                },
                max: Math.max.apply(Math, data.parent_data),
                min: 0
            },
            { // Primary yAxis
                labels: {
                    format: '{value} %',
                },
                title: {
                    text: data.y2_title,
                },
                opposite: true,
                max: 100,
                min: 0
            }],
        tooltip: {
            shared: true
        },
        colors: ['steelblue', 'sandybrown', 'green'],
        legend: {
            enabled: true,
        },
        series: [{
            name: data.parent_data_name,
            type: 'column',
            data: data.parent_data

        }, {
            name: data.child_data_name,
            type: 'column',
            data: data.child_data
        },
        {
            name: data.percent_data_name,
            type: data.useLine ? 'spline' : 'scatter',
            data: data.percent_data,
            yAxis: 1,
            tooltip: {
                pointFormat: '<b>{point.y:.1f}%</b>',
                //valueSuffix: ' %'
            }
        }]
    });
}


function build_trend_chart(data) { //container_id, title, yAxistitle, xaxisCategory, series_data) {
    //var colors = get_color_shades(2); 
    Highcharts.chart('container', {

        title: {
            text: data.title,
            style: {
                fontSize: '12px'
            }
        },
        credits: false,
        yAxis: {
            title: {
                text: data.yAxistitle
            }
        },
        xAxis: {
            categories: data.xaxisCategory,
        },
        colors: ['#808000', '#46f0f0', '#e6194b', '#3cb44b', '#f58231', '#0082c8', '#911eb4', '#000000', '#f032e6', '#008080', '#aa6e28', '#000080', '#d2f53c'],
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            title: {
                text: '<span style="font-size: 12px;text-decoration: underline;"> Cohorts </span>',
                style: {
                    fontStyle: 'italic'
                }
            },
            labelFormatter: function () {
                return '<span style="color:' + this.color + ';">' + this.name + '</span>';
            },
        },
        tooltip: {
            formatter: function () {
                return '<b>Report period:</b> ' + this.point.category + '<br /><b> Cohort </b>:' + this.series.name + '<br /><b> No. of patient:</b>' + this.point.y
            }
        },

        plotOptions: {
            line: {
                marker: {
                    enabled: false
                }
            }
        },

        series: data.series_data,
    });
}

function build_side_by_side_column_chart(data) { //container_id, title, yAxistitle, xaxisCategory, series_data) {
    Highcharts.chart('container', {
        chart: {
            type: 'column'
        },
        title: {
            text: data.title
        },
        credits: false,
        xAxis: {
            categories: data.xaxisCategory,
            crosshair: true
        },
        yAxis: {
            min: 0,
            title: {
                text: data.yAxistitle
            }
        },
        colors: ['steelblue', 'sandybrown', 'green'],
        tooltip: {
            headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
            pointFormat: '<tr><td style="color:{series.color};padding:0">{series.name}: </td>' +
                '<td style="padding:0"><b>{point.y}</b></td></tr>',
            footerFormat: '</table>',
            shared: true,
            useHTML: true
        },
        plotOptions: {
            column: {
                dataLabels: {
                    enabled: true
                }
            },
            series: {
                dataLabels: {
                    crop: false,
                    enabled: true,
                    overflow: 'none'
                }
            }
        },
        series: data.series_data
    });
}


function get_color_shades(start) {
    var colors = [],
        base = Highcharts.getOptions().colors[start],
        i;

    for (i = 0; i < 100; i += 1) {
        colors.push(Highcharts.Color(base).brighten((i) / 100).get());
    }
    return colors;
}

function countUnique(one_dimensional_array_item) {
    return new Set(one_dimensional_array_item).size;
}

function getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}

function build_stacked_bar_with_percent(data) { //container_id, title, xaxis_categories, y1_axis_title, y2_axis_title, smaller_data_set, smaller_data_name, larger_data_set, bigger_data_name, percent_data, useLine = true) {

    Highcharts.chart('container', {
        chart: {
            type: 'column'
        },
        title: {
            text: data.title
        },
        credits: false,
        xAxis: {
            categories: data.xaxis_categories
        },
        yAxis: [{
            min: 0,
            title: {
                text: data.y1_axis_title
            },
        },
        {
            labels: {
                format: '{value} %',
            },
            title: {
                text: data.y2_axis_title,
            },
            opposite: true,
            max: 100,
            min: 0
        }
        ],
        legend: {
            align: 'middle',
            verticalAlign: 'bottom',
            //floating: true,
            y: 10,
            backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'white',
            borderColor: '#CCC',
            borderWidth: 1,
            shadow: false
        },
        tooltip: {
            headerFormat: '<b>{point.x}</b><br/>',
            pointFormat: '{series.name}: {point.y}<br/>Total: {point.stackTotal}'
        },
        plotOptions: {
            column: {
                stacking: 'normal',
                dataLabels: {
                    enabled: true,

                }
            }
        },
        colors: ['sandybrown', 'green', 'cornflowerblue'],
        series: [{
            name: data.smaller_data_name,
            data: data.smaller_data_set
        }, {
            name: data.bigger_data_name,
            data: data.larger_data_set
        },
        {
            name: 'percentage',
            type: data.useLine ? 'spline' : 'scatter',
            data: data.percent_data,
        }]
    });
}

function ExportFormattedData(csvFile, filename) {
    var blob = new Blob([csvFile], { type: 'text/csv;charset=utf-8;' });
    if (navigator.msSaveBlob) { // IE 10+
        navigator.msSaveBlob(blob, filename);
    } else {
        var link = document.createElement("a");
        if (link.download !== undefined) { // feature detection
            // Browsers that support HTML5 download attribute
            var url = URL.createObjectURL(blob);
            link.setAttribute("href", url);
            link.setAttribute("download", filename);
            link.style.visibility = 'hidden';
            document.body.appendChild(link);
            link.click();
            document.body.removeChild(link);
        }
    }
}
