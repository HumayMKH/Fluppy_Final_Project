$(document).ready(function () {

    //CkEDitor js

    if ($(".CKPage").length>0) {
        CKEDITOR.replace('CkText');
    }

     //with number js 
    $(".only-number").keypress(function (event) {
        return isNumber(event, this);
    });

    function isNumber(evt, element) {
        var charCode = (evt.which) ? evt.which : event.keyCode;

        if (
            /*(charCode !== 45 || $(element).val().indexOf('-') !== -1) &&*/      // “-” CHECK MINUS, AND ONLY ONE.
            (charCode !== 46 || $(element).val().indexOf('.') !== -1) &&          // “.” CHECK DOT, AND ONLY ONE.
            /*(charCode !== 44 || $(element).val().indexOf(',') !== -1) &&*/          // “,” CHECK DOT, AND ONLY ONE.
            (charCode < 48 || charCode > 57))
            return false;

        return true;
    }
    //Datetimepicker js

    $('#DateAndTime').datetimepicker({
    format: 'H:i',
    datepicker: false,
    step: 15
    });
    $('#DateAndTIme').datetimepicker({
        format: 'H:i',
        datepicker: false,
        step: 15
    });
    $('#DateAndTiMe').datetimepicker({
        format: 'H:i',
        datepicker: false,
        step: 15
    });
    $('#DateAndTimE').datetimepicker({
        format: 'H:i',
        datepicker: false,
        step: 15
    });
    $('#ArticleDateCreate').datetimepicker({
       
        yearStart: 2020
    });
    $('#ArticleDateUpdate').datetimepicker({
    
        yearStart: 2020
    });
    $('#BlogdateCreate').datetimepicker({
        yearStart: 2020
    });
    $('#BlogDateUpdate').datetimepicker({
        yearStart: 2020
    });
});