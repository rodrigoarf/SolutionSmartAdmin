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
                        PrintAlert('Informe seu Login de acesso ao sistema.');
                        return (false);
                        break;
                    case 'EMAIL':
                        PrintAlert('Informe seu E-mail.');
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
                        PrintAlert('Informe o Login para acesso ao sistema.');
                        return (false);
                        break;
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