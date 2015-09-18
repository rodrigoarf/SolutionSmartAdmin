runAllForms();

$('#btnSaveUsuario').click(function () {
    FormValidation('#smart-form-create');
});  

$('#LOGIN').keypress(function (e) {
    var Regex = new RegExp("^[a-zA-Z0-9]+$");
    var Str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
    if (Regex.test(Str)) { return true; }
    e.preventDefault();
    return false;
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
            CPF_CNPJ: { required: true },
            PAIS: { required: true },
            LOGIN: { required: true, maxlength: 14 },
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
            CPF_CNPJ: { required: null },
            PAIS: { required: null },
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
                    case 'CPF_CNPJ':
                        PrintAlert('Informe o documento de Cpf ou Cnpj do usuário (somente números).');
                        return (false);
                        break;
                    case 'PAIS':
                        PrintAlert('Informe a País do usuário.');
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

$.validator.addMethod("cpf", function (value, element) {

    value = jQuery.trim(value);  
    value = value.replace('.', '');
    value = value.replace('.', '');
    cpf = value.replace('-', '');

    while (cpf.length < 11) cpf = "0" + cpf;
    var expReg = /^0+$|^1+$|^2+$|^3+$|^4+$|^5+$|^6+$|^7+$|^8+$|^9+$/;
    var a = [];
    var b = new Number;
    var c = 11;

    for (i = 0; i < 11; i++) {
        a[i] = cpf.charAt(i);
        if (i < 9) b += (a[i] * --c);
    }

    if ((x = b % 11) < 2) { a[9] = 0 } else { a[9] = 11 - x }

    b = 0;
    c = 11;

    for (y = 0; y < 10; y++) b += (a[y] * c--);
    if ((x = b % 11) < 2) { a[10] = 0; } else { a[10] = 11 - x; }

    var retorno = true;
    if ((cpf.charAt(9) != a[9]) || (cpf.charAt(10) != a[10]) || cpf.match(expReg)) retorno = false;

    return this.optional(element) || retorno;

}, "Informe um CPF válido");

$.validator.addMethod("cnpj", function (cnpj, element) {
    cnpj = jQuery.trim(cnpj);// retira espaços em branco
    // DEIXA APENAS OS NÚMEROS
    cnpj = cnpj.replace('/', '');
    cnpj = cnpj.replace('.', '');
    cnpj = cnpj.replace('.', '');
    cnpj = cnpj.replace('-', '');

    var numeros, digitos, soma, i, resultado, pos, tamanho, digitos_iguais;
    digitos_iguais = 1;

    if (cnpj.length < 14 && cnpj.length < 15) {
        return false;
    }
    for (i = 0; i < cnpj.length - 1; i++) {
        if (cnpj.charAt(i) != cnpj.charAt(i + 1)) {
            digitos_iguais = 0;
            break;
        }
    }

    if (!digitos_iguais) {
        tamanho = cnpj.length - 2
        numeros = cnpj.substring(0, tamanho);
        digitos = cnpj.substring(tamanho);
        soma = 0;
        pos = tamanho - 7;

        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2) {
                pos = 9;
            }
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(0)) {
            return false;
        }
        tamanho = tamanho + 1;
        numeros = cnpj.substring(0, tamanho);
        soma = 0;
        pos = tamanho - 7;
        for (i = tamanho; i >= 1; i--) {
            soma += numeros.charAt(tamanho - i) * pos--;
            if (pos < 2) {
                pos = 9;
            }
        }
        resultado = soma % 11 < 2 ? 0 : 11 - soma % 11;
        if (resultado != digitos.charAt(1)) {
            return false;
        }
        return true;
    } else {
        return false;
    }
}, "Informe um CNPJ válido.");

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

$('#extr-page-header-space').hide();