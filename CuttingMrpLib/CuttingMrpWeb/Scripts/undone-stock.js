﻿var UnDoneStock = {};

UnDoneStock.init = function () {
    var partnr = $('#PartNr').val();
    var status = $("#State").children("option:selected").html();
    var sourcetype = $("#SourceType").children("option:selected").html();

    UnDoneStock.add_string_label_to_div(partnr, 'PartNr like ', '.filter-p');
    UnDoneStock.add_string_label_to_div(status, 'State =', '.filter-p');
    UnDoneStock.add_string_label_to_div(sourcetype, 'SourceType =', '.filter-p');
}

UnDoneStock.click_filter = function () {
    $('#basic-addon-filter').click(function () {
        $('#basic-addon-filter').popModal({
            html: $('#extra-filter-content'),
            placement: 'bottomRight',
            showCloseBut: false,
            onDocumentClickClose: true,
            onOkBut: function () {
            },
            onCancelBut: function () {
            },
            onLoad: function () {
            },
            onClose: function () {
            }
        })
    });
}

UnDoneStock.cancel_all = function () {
    $('.cancel-all').click(function () {
        $.ajax({
            url: '/UnDoneStock/CancelAll',
            type: 'post',
            success: function (data) {
                console.log(data);
            },
            error: function () {
                console.log("Something Error!");
            }
        })
    })
}

UnDoneStock.add_string_label_to_div = function (content, name, cls) {
    if (content != null && content != "") {
        $("<p class='label label-primary' style='margin-left:5px;font-size:.8em;white-space:normal;'>" + name + " " + content + "</p>").appendTo(cls).ready(function () {
        });
    }
}

UnDoneStock.add_range_label_to_div = function (content, name, cls) {
    var from = content.split("~")[0];
    var to = content.split("~")[1];
    if ((from != "" && from != null) && (to != null && to != "")) {
        $("<p class='label label-primary' style='margin-left:5px;font-size:.8em;white-space:normal;'>" + name + " : " + from + "~" + to + "</p>").appendTo(cls).ready(function () {
        });
    } else if ((from != "" && from != null) && (to == null || to == "")) {
        $("<p class='label label-primary' style='margin-left:5px;font-size:.8em;white-space:normal;'>" + name + ">=" + from + "</p>").appendTo(cls).ready(function () {
        });
    } else if ((from == "" || from == null) && (to != null && to != "")) {
        $("<p class='label label-primary' style='margin-left:5px;font-size:.8em;white-space:normal;'>" + name + "<=" + to + "</p>").appendTo(cls).ready(function () {
        });
    }
}

$('.datetime-picker-from').datetimepicker({
    lang: 'ch',
    timepicker: false,
    format: 'Y/m/d H:i',
    formatDate: 'Y/m/d',
    formatTime: 'H:i',
    defaultTime: '00:00'
})

$('.datetime-picker-to').datetimepicker({
    lang: 'ch',
    timepicker: false,
    format: 'Y/m/d H:i',
    formatDate: 'Y/m/d',
    formatTime: 'H:i',
    defaultTime: '23:59'
})

UnDoneStock.import_force_record = function () {
    $('.import-force-record').click(function () {
        $('#dialog_content').dialogModal({
            onOkBut: function () {
            },
            onCancelBut: function () { },
            onLoad: function () { },
            onClose: function () { },
        });
    });
}

window.onload = function () {
    $('.navbar-nav li').removeClass("nav-choosed");
    $('.nav-undone-stock').addClass("nav-choosed");
}