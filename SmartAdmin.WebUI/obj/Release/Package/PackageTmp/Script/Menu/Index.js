$('#btnCreateMenuMain').click(function () {
    FormValidation('#smart-form-create');
});  

function FormValidation(form)
{
    jQuery(form).validate({
        rules: {
            NOME: { required: true },
            //CONTROLLER: { required: true },
            //ACTION: { required: true },
            ICONE: { required: true },
            STATUS: { required: true }
        },
        messages: {
            NOME: { required: null },
            //CONTROLLER: { required: null },
            //ACTION: { required: null },
            ICONE: { required: null },
            STATUS: { required: null }
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
                        PrintAlert('Informe o <span style=\"color:#10e4ea;\">Nome</span> do menu.');
                        return (false);
                        break;
                        //case 'CONTROLLER':
                        //    PrintAlert('Informe a <span style=\"color:#10e4ea;\">Controller</span> do menu.');
                        //    return (false);
                        //    break;
                        //case 'ACTION':
                        //    PrintAlert('Informe a <span style=\"color:#10e4ea;\">Action</span> do menu.');
                        //    return (false);
                        //    break;
                    case 'ICONE':
                        PrintAlert('Informe o <span style=\"color:#10e4ea;\">Icone</span>.');
                        return (false);
                        break;
                    case 'STATUS':
                        PrintAlert('Informe o <span style=\"color:#10e4ea;\">Status</span>.');
                        return (false);
                        break;
                }
            }
        },
        submitHandler: function (form) {
            form.submit();
        }
    });
}

function PrintAlert(alert) {
    $.SmartMessageBox({
        title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
        content: '<div style="margin:10px 0px 0px 5px;">' + alert + '</div>',
        buttons: '[Voltar]'
    });
}
