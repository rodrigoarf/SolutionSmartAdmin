runAllForms();

$('#btnSaveUsuario').click(function () {
    FormValidation('#smart-form-create');
});

function FormValidation(form) {
    $(form).validate({
        rules: {
            NOME: { required: true },
            EMAIL: { required: true, email: true },
            SEXO: { required: true },
            ENDERECO: { required: true },
            NUMERO: { required: true },
            BAIRRO: { required: true },
            CIDADE: { required: true },
            UF: { required: true },
            CEP: { required: true },
            LOGIN: { required: true },
            SENHA: { required: true }
        },
        messages: {
            NOME: { required: null },
            EMAIL: { required: null },
            SEXO: { required: null },
            ENDERECO: { required: null },
            NUMERO: { required: null },
            BAIRRO: { required: null },
            CIDADE: { required: null },
            UF: { required: null },
            CEP: { required: null },
            LOGIN: { required: null },
            SENHA: { required: null }
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
                        PrintAlert('Informe o Nome completo do usuário.');
                        return (false);
                        break;
                    case 'EMAIL':
                        PrintAlert('Informe o E-mail do usuário.');
                        return (false);
                        break;
                    case 'SEXO':
                        PrintAlert('Informe o Sexo do usuário.');
                        return (false);
                        break;
                    case 'ENDERECO':
                        PrintAlert('Informe o Endereço do usuário.');
                        return (false);
                        break;
                    case 'BAIRRO':
                        PrintAlert('Informe o Bairro do usuário.');
                        return (false);
                        break;
                    case 'CIDADE':
                        PrintAlert('Informe a Cidade do usuário.');
                        return (false);
                        break;
                    case 'NUMERO':
                        PrintAlert('Informe o Número do usuário.');
                        return (false);
                        break;
                    case 'UF':
                        PrintAlert('Informe a Estado do usuário.');
                        return (false);
                        break;
                    case 'CEP':
                        PrintAlert('Informe a Cep do usuário (somente números).');
                        return (false);
                        break;
                    case 'LOGIN':
                        PrintAlert('Informe o Login para acesso ao sistema.');
                        return (false);
                        break;
                    case 'SENHA':
                        PrintAlert('Informe a Senha de acesso');
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

jQuery.extend(jQuery.validator.messages, {
    required: null,
    remote: null,
    email: null,
    url: null,
    date: null,
    dateISO: null,
    number: null,
    digits: null,
    creditcard: null,
    equalTo: null,
    accept: null,
    maxlength: null,
    minlength: null,
    rangelength: null,
    range: null,
    max: null,
    min: null
});