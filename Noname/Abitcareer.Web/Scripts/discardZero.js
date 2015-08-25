revertZero = function () {
    var input = $('.salaries[value]')
    var id = [1, 2, 3, 4, 5, 10, 20];
    for (var i = 0; i < id.length; i++) {
        if ($('#Salaries__' + id[i] + '_').val() == '-1') {
            $('#Salaries__' + id[i] + '_').val('');
        }
    };
    if ($('#Prices__TopUniversityPrice_').val() == '-1') $('#Prices__TopUniversityPrice_').val('');
    if ($('#Prices__MiddleUniversityPrice_').val() == '-1') $('#Prices__MiddleUniversityPrice_').val('');
    if ($('#Prices__LowUniversityPrice_').val() == '-1') $('#Prices__LowUniversityPrice_').val('');
    if ($('#StartOfWorking').val() == '0') $('#StartOfWorking').val('');
}

var revertZeroSubmit = function () {
    var input = $('.salaries[value]')
    var id = [1, 2, 3, 4, 5, 10, 20];
    for (var i = 0; i < id.length; i++) {
        if ($('#Salaries__' + id[i] + '_').val() == '') {
            $('#Salaries__' + id[i] + '_').css('color', 'transparent').val(-1);
        }
    };
    if ($('#Prices__TopUniversityPrice_').val() == '') $('#Prices__TopUniversityPrice_').css('color', 'transparent').val('-1');
    if ($('#Prices__MiddleUniversityPrice_').val() == '') $('#Prices__MiddleUniversityPrice_').css('color', 'transparent').val('-1');
    if ($('#Prices__LowUniversityPrice_').val() == '') $('#Prices__LowUniversityPrice_').css('color', 'transparent').val('-1');
};