$('#btnLogar').click(function () {

    var validation = true;

    $("#smart-form-login input").each(function (index) { 
        var input = $(this);
        if (input.attr('name') == 'LOGIN') {
            if ($(this).val() == '') {
                PrintAlert('Você deve preencher o campo <span style=\"color:#10e4ea;\">Usuário</span>!');
                validation = false;
                return (false);
            }
        } 
        if (input.attr('name') == 'SENHA') {
            if ($(this).val().length < 1) {
                PrintAlert('Você deve preencher o campo <span style=\"color:#10e4ea;\">Senha</span>!');
                validation = false;
                return (false);
            }
        }
    });

    if (!validation) { return (false) }
});

$('#REMEMBER').click(function () {
    var Check = $(this).is(':checked');
    if (Check == true) { $('#Remind').val("true"); }
    else { $('#Remind').val("false"); }
});

$('#LOGIN').keypress(function (e) {
    var Regex = new RegExp("^[a-zA-Z0-9]+$");
    var Str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (Regex.test(Str)) { return true; }
    e.preventDefault();
    return false;
});

function PrintAlert(alert) {
    $.SmartMessageBox({
        title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
        content: '<div style="margin:10px 0px 0px 5px;">' + alert + '</div>',
        buttons: '[Voltar]'
    });
}

