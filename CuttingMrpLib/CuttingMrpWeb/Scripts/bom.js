﻿var Bom = {}

Bom.Init = function () {
    var id = $('#ID').val();
    var partnr = $('#PartNr').val();
    var vffrom = $('#VFFrom').val();
    var vfto = $('#VFTo').val();

    var vtfrom = $('#VTFrom').val();
    var vtto = $('#VTTo').val();

    Bom.add_string_label_to_div(id, 'ID like ', '.filter-p');
    Bom.add_string_label_to_div(partnr, 'PartNr like ', '.filter-p');
    Bom.add_range_label_to_div(vffrom + "~" + vfto, 'ValidFrom', '.filter-p');
    Bom.add_range_label_to_div(vtfrom + "~" + vtto, 'ValidFrom', '.filter-p');

}

Bom.click_filter = function () {
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

Bom.add_string_label_to_div = function (content, name, cls) {
    if (content != null && content != "") {
        $("<p class='label label-primary' style='margin-left:5px;font-size:.8em;white-space:normal;'>" + name + " " + content + "</p>").appendTo(cls).ready(function () {
        });
    }
}

Bom.add_range_label_to_div = function (content, name, cls) {
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

Bom.import_bom_data = function () {
    $('.import-bom-data').click(function () {
        $('#dialog_content').dialogModal({
            onOkBut: function () {
            },
            onCancelBut: function () { },
            onLoad: function () { },
            onClose: function () { },
        });
    });
}

Bom.import_result = function () {
    var CreateFailureQty = $('#CreateFailureQty').html();
    var UpdateFailureQty = $('#UpdateFailureQty').html();
    var DeleteFailureQty = $('#DeleteFailureQty').html();
    var ActionNullQty = $('#ActionNullQty').html();
    var OtherQty = $('#OtherQty').html();

    if (CreateFailureQty == 0) {
        $('.CreateFailureTable').css({ display: 'none' })
    }

    if (UpdateFailureQty == 0) {
        $('.UpdateFailureTable').css({ display: 'none' })
    }

    if (DeleteFailureQty == 0) {
        $('.DeleteFailureTable').css({ display: 'none' })
    }

    if (ActionNullQty == 0) {
        $('.ActionNullTable').css({ display: 'none' })
    }

    if (OtherQty == 0) {
        $('.OtherTable').css({ display: 'none' })
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

window.onload = function () {
    $('.navbar-nav li').removeClass("nav-choosed");
    $('.nav-basic-data').addClass("nav-choosed");
    $('.nav-bom').addClass("nav-choosed");
}