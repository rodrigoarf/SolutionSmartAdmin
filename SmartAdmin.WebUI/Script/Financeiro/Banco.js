runAllForms();

$('#btnSaveBanco').click(function () {
    jQuery('#smart-form-create').validate({
        rules: {
            NOME: { required: true },
            WEBSITE: { required: true },
            SIGLA: { required: true },
            FLAG_TIPO: { required: true },
            STATUS: { required: true },
        },
        messages: {
            NOME: { required: null },
            WEBSITE: { required: null },
            SIGLA: { required: null },
            FLAG_TIPO: { required: null },
            STATUS: { required: null },
        },
        //highlight: null,
        //unhighlight: null,
        invalidHandler: function (event, validator) {
            var erros = validator.numberOfInvalids();
            var singularPlural = erros > 1 ? 'campos' : 'campos';
            var mensagem = ['Existem ', erros, singularPlural, 'sem completar no fomulário. Por favor complete-os'].join(' ');
            for (var i = 0; i < validator.errorList.length; i++) {
                //console.log(validator.errorList[i].element.id + ' - ' + validator.errorList[i].message);
                var controlForm = validator.errorList[i].element;
                switch (controlForm.id) {
                    case 'NOME':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Nome</span> da agência bancaria.');
                        return (false);
                        break;
                    case 'WEBSITE':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Website</span> da agência bancaria.');
                        return (false);
                        break;
                    case 'SIGLA':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Sigla</span> da agência bancaria.');
                        return (false);
                        break;
                    case 'FLAG_TIPO':
                        PrintAlert('Informe o tipo de <span style="color:#10e4ea;">Agência Bancaria</span>.');
                        return (false);
                        break;
                    case 'STATUS':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Status</span> da agência bancaria.');
                        return (false);
                        break;
                }
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
});

function PrintAlert(alert) {
    $.SmartMessageBox({
        title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
        content: '<div style="margin:10px 0px 0px 5px;">' + alert + '</div>',
        buttons: '[Voltar]'
    });
}