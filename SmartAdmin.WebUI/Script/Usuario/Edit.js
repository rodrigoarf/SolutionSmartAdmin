runAllForms();


$(document).ready(function () {
    $(".phonemask").mask("(99)9999-9999");
    $(".mobilemask").mask("(99)99999-9999");

    $('#btnSaveUsuario').click(function () {
        jQuery('#smart-form-edit').validate({
            rules: {
                NOME: { required: true },
                EMAIL: { required: true, email: true },
                SEXO: { required: true },
                //TELEFONE: { required: true },
                CELULAR: { required: true },
                ENDERECO: { required: true },
                NUMERO: { required: true, maxlength: 4 },
                BAIRRO: { required: true },
                CIDADE: { required: true },
                CEP: { required: true },
                COMPLEMENTO: { required: true },
                UF: { required: true },
                PAIS: { required: true },
                STATUS: { required: true }
            },
            messages: {
                NOME: { required: null },
                EMAIL: { required: null },
                SEXO: { required: null },
                //TELEFONE: { required: null },
                CELULAR: { required: null },
                ENDERECO: { required: null },
                NUMERO: { required: null },
                BAIRRO: { required: null },
                CIDADE: { required: null },
                CEP: { required: null },
                COMPLEMENTO: { required: null },
                UF: { required: null },
                PAIS: { required: null },
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
                            PrintAlert('Informe o nome.');
                            return (false);
                            break;
                        case 'EMAIL':
                            PrintAlert('Informe o e-mail.');
                            return (false);
                            break;
                        case 'SEXO':
                            PrintAlert('Informe o Sexo.');
                            return (false);
                            break;
                            //case 'TELEFONE':
                            //    PrintAlert('Informe o telefone.');
                            //    return (false);
                            //    break;
                        case 'CELULAR':
                            PrintAlert('Informe o celular.');
                            return (false);
                            break;
                        case 'ENDERECO':
                            PrintAlert('Informe o endereço.');
                            return (false);
                            break;
                        case 'NUMERO':
                            PrintAlert('Informe o número.');
                            return (false);
                            break;
                        case 'BAIRRO':
                            PrintAlert('Informe o bairro.');
                            return (false);
                            break;
                        case 'CIDADE':
                            PrintAlert('Informe a cidade.');
                            return (false);
                            break;
                        case 'CEP':
                            PrintAlert('Informe o cep.');
                            return (false);
                            break;
                        case 'COMPLEMENTO':
                            PrintAlert('Informe o complemento.');
                            return (false);
                            break;
                        case 'PAIS':
                            PrintAlert('Informe o país.');
                            return (false);
                            break;
                        case 'UF':
                            PrintAlert('Informe o estado.');
                            return (false);
                            break;
                        case 'STATUS':
                            PrintAlert('Informe o status.');
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

    // tratament como null para integrar ao SmartMessageBox 
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
});








