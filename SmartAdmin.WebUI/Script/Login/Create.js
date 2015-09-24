$('#btnRemember').click(function () {
    jQuery('#smart-form-remember').validate({
        rules: {
            LOGIN: { required: true },
            EMAIL: { required: true, email: true }
        },
        messages: {
            LOGIN: { required: null },
            EMAIL: { required: null }
        },
        highlight:   null,
        unhighlight: null,
        invalidHandler: function (event, validator) {
            var erros = validator.numberOfInvalids();
            var singularPlural = erros > 1 ? 'campos' : 'campos';
            var mensagem = ['Existem ', erros, singularPlural, 'sem completar no fomulário. Por favor complete-os'].join(' ');
            for (var i = 0; i < validator.errorList.length; i++) {
                //console.log(validator.errorList[i].element.id + ' - ' + validator.errorList[i].message);
                var controlForm = validator.errorList[i].element;
                switch (controlForm.id) {
                    case 'LOGIN':
                        PrintAlert('Informe seu <span style=\"color:#10e4ea;\">Login</span> de acesso ao sistema.');
                        return (false);
                        break;
                    case 'EMAIL':
                        PrintAlert('Informe seu <span style=\"color:#10e4ea;\">E-mail</span>.');
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

$("#btnSaveUsuario").click(function () {
    jQuery('#smart-form-create').validate({
        rules: {
            LOGIN:   { required: true },
            NOME:    { required: true },
            EMAIL:   { required: true, email: true },
            SEXO:    { required: true },
            SENHA:   { required: true }
        },   
        messages: {
            LOGIN:   { required: null },
            NOME:    { required: null },
            EMAIL:   { required: null },
            SEXO:    { required: null },
            SENHA:   { required: null }
        },
        highlight:   null,
        unhighlight: null,
        invalidHandler: function (event, validator) {
            var erros = validator.numberOfInvalids();
            var singularPlural = erros > 1 ? 'campos' : 'campos';
            var mensagem = ['Existem ', erros, singularPlural, 'sem completar no fomulário. Por favor complete-os'].join(' ');
            for (var i = 0; i < validator.errorList.length; i++) {
                //console.log(validator.errorList[i].element.id + ' - ' + validator.errorList[i].message);
                var controlForm = validator.errorList[i].element;
                switch (controlForm.id) {
                    case 'LOGIN':
                        PrintAlert('Informe o <span style=\"color:#10e4ea;\">Login</span> para acesso ao sistema.');
                        return (false);
                        break;
                    case 'NOME':
                        PrintAlert('Informe o <span style=\"color:#10e4ea;\">Nome</span> completo do usuário.');
                        return (false);
                        break;
                    case 'EMAIL':
                        PrintAlert('Informe o <span style=\"color:#10e4ea;\">E-mail</span> do usuário.');
                        return (false);
                        break;
                    case 'SEXO':
                        PrintAlert('Informe o <span style=\"color:#10e4ea;\">Sexo</span> do usuário.');
                        return (false);
                        break;
                    case 'SENHA':
                        PrintAlert('Informe a <span style=\"color:#10e4ea;\">Senha</span> de acesso');
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