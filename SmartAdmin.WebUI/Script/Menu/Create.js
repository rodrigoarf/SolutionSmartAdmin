//$("#btnSaveMenu").click(function () {

//    var validation = true;

//    $('#smart-form-create input, #smart-form-create select').each(function (index) {

//        var input = $(this);

//        if (input.attr('name') == 'NOME') {
//            if ($(this).val() == '') {
//                PrintAlert('Você deve preencher o campo Nome!');
//                validation = false;
//                return (false);
//            }
//        }

//        if (input.attr('name') == 'USUARIO') {
//            if ($(this).val() == '') {
//                PrintAlert('Você deve preencher o campo Usuário!');
//                validation = false;
//                return (false);
//            }
//        }

//        if (input.attr('name') == 'SENHA') {
//            if ($(this).val().length < 1) {
//                PrintAlert('Você deve preencher o campo Senha!');
//                validation = false;
//                return (false);
//            }
//        }

//        if (input.attr('name') == 'RESENHA') {
//            if ($(this).val().length < 1) {
//                PrintAlert('Você deve preencher o campo ReSenha!');
//                validation = false;
//                return (false);
//            }
//        }

//        if (input.attr('name') == 'EMAIL') {
//            if ($(this).val() == '') {
//                PrintAlert('Você deve preencher o campo E-mail!');
//                validation = false;
//                return (false);
//            }
//        }

//        if (input.attr('name') == 'SEXO') {
//            if (input.val() == '0') {
//                PrintAlert('Você deve selecionar o Sexo!');
//                validation = false;
//                return (false);
//            }
//        }
//    }
//    );

//    if (!validation) {
//        return (false);
//    }

//});


//function PrintAlert(alert) {
//    $.SmartMessageBox({
//        title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
//        content: '<div style="margin:10px 0px 0px 5px;">' + alert + '</div>',
//        buttons: '[Voltar]'
//    });
//}