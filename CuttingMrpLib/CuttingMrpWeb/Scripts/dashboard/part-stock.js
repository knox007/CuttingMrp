﻿var PartStock = {};
PartStock.InitPartNr = function () {
    var part_nr_value=$('.part-nr').val();
    $('#part_nr').typeahead({
        //source: [
        //    { id: 1, full_name: 'Toronto', first_two_letters: 'To' },
        //    { id: 2, full_name: 'Montreal', first_two_letters: 'Mo' },
        //    { id: 3, full_name: 'New York', first_two_letters: 'Ne' },
        //    { id: 4, full_name: 'Buffalo', first_two_letters: 'Bu' },
        //    { id: 5, full_name: 'Boston', first_two_letters: 'Bo' },
        //    { id: 6, full_name: 'Columbus', first_two_letters: 'Co' },
        //    { id: 7, full_name: 'Dallas', first_two_letters: 'Da' },
        //    { id: 8, full_name: 'Vancouver', first_two_letters: 'Va' },
        //    { id: 9, full_name: 'Seattle', first_two_letters: 'Se' },
        //    { id: 10, full_name: 'Los Angeles', first_two_letters: 'Lo' }
        //],
        //display: 'full_name'

        ajax: {
            url: '/Parts/Fuzzies?id=91',
            type: 'get',
            triggerLength: 1
        }
    });
}

PartStock.PartStockSearch = function () {
    $('.part-stock-search').click(function () {
        var DateFrom = $('.date-from').val();
        var DateTo = $('.date-to').val();
        var Type = $('.part-type').val();
        var PartNr = $('.part-nr').val();
        $.ajax({
            url: '/Dashboard/Data',
            type: 'get',
            data: {
                Type: Type,
                PartNr: PartNr,
                DateFrom: DateFrom,
                DateTo: DateTo
            },
            success: function (data) {
                console.log("Success");
            },
            error: function () {
                console.log("Error");
            }
        })
    })
}

PartStock.DrawCharts = function () {
    $('#container').highcharts({
        title: {
            text: 'Monthly Average Temperature',
            x: -20 //center
        },
        subtitle: {
            text: 'Source: WorldClimate.com',
            x: -20
        },
        xAxis: {
            categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec']
        },
        yAxis: {
            title: {
                text: 'Temperature (°C)'
            },
            plotLines: [{
                value: 0,
                width: 1,
                color: '#808080'
            }]
        },
        tooltip: {
            valueSuffix: '°C'
        },
        legend: {
            layout: 'vertical',
            align: 'right',
            verticalAlign: 'middle',
            borderWidth: 0
        },
        series: [{
            name: 'Tokyo',
            data: [7.0, 6.9, 9.5, 14.5, 18.2, 21.5, 25.2, 26.5, 23.3, 18.3, 13.9, 9.6]
        }, {
            name: 'New York',
            data: [-0.2, 0.8, 5.7, 11.3, 17.0, 22.0, 24.8, 24.1, 20.1, 14.1, 8.6, 2.5]
        }, {
            name: 'Berlin',
            data: [-0.9, 0.6, 3.5, 8.4, 13.5, 17.0, 18.6, 17.9, 14.3, 9.0, 3.9, 1.0]
        }, {
            name: 'London',
            data: [3.9, 4.2, 5.7, 8.5, 11.9, 15.2, 17.0, 16.6, 14.2, 10.3, 6.6, 4.8]
        }]
    });
}
