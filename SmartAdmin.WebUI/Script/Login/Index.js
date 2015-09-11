$('#btnLogar').click(function () {

    var validation = true;

    $("#smart-form-login input").each(function (index) {

        var input = $(this);

        if (input.attr('name') == 'LOGIN') {
            if ($(this).val() == '') {
                PrintAlert('Você deve preencher o campo Usuário!');
                validation = false;
                return (false);
            }
        }

        if (input.attr('name') == 'SENHA') {
            if ($(this).val().length < 1) {
                PrintAlert('Você deve preencher o campo Senha!');
                validation = false;
                return (false);
            }
        }
    });

    if (!validation) { return (false) }
});


function PrintAlert(alert) {
    $.SmartMessageBox({
        title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
        content: '<div style="margin:10px 0px 0px 5px;">' + alert + '</div>',
        buttons: '[Voltar]'
    });
}