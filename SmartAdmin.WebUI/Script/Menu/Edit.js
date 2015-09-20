// abre modal de create menu (validaçao e salvamento async)
function OpenModalCreate(IdItem, IdSubItem) {
    $.ajax({
        url: '/Menu/CreateSubMenuPartial?IdItem=' + IdItem + '&IdSubItem=' + IdSubItem,
        cache: false,
        success: function (html) {
            //console.log(html);
            $("#modal-submenu-create").html(html);
            $("#modal-submenu-create").modal('show');

            // delegate On
            $("#modal-submenu-create").on("click", "#btnSaveSubMenu", function () {
                FormValidation('#modal-submenu-create', '#smart-form-create-submenu');
            });
        }
    });
}

// abre modal de edit menu (alteração async)
function OpenModalEdit(IdItem, IdSubItem)
{
    $.ajax({
        url: '/Menu/EditSubMenuPartial?IdItem=' + IdItem + '&IdSubItem=' + IdSubItem,
        cache: false,
        success: function (html) {
            //console.log(html);
            $("#modal-submenu-edit").html(html);
            $("#modal-submenu-edit").modal('show');              

            // delegate On
            $("#modal-submenu-edit").on("click", "#btnSaveSubMenu", function () { 
                FormValidation('#modal-submenu-edit', '#smart-form-edit-submenu');
            });   
        }
    });
}

// abre modal de delete menu (alteração async)
function OpenModalDelete(IdItem, IdSubItem) {
    $.ajax({
        url: '/Menu/DeleteSubMenuPartial?IdItem=' + IdItem + '&IdSubItem=' + IdSubItem,
        cache: false,
        success: function (html) {
            //console.log(html);
            $("#modal-submenu-delete").html(html);
            $("#modal-submenu-delete").modal('show');

            // delegate On
            $("#modal-submenu-delete").on("click", "#btnDeleteSubMenu", function () {
                    $.ajax({
                        url: '/Menu/DeleteSubMenu?IdItem=' + IdItem + '&IdSubItem=' + IdSubItem,
                        type: 'post',
                        //data: $(form).serialize(),
                        success: function (message) {
                            //console.log(message);
                            $.SmartMessageBox({
                                title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
                                content: '<div style="margin:10px 0px 0px 5px;">' + message + '</div>',
                                buttons: '[Voltar]',
                            }, function (ButtonPressed) {
                                if (ButtonPressed === "Voltar") {
                                    $('#modal-submenu-delete').modal('hide');
                                    $(location).attr('href', window.location.href);
                                }
                            });
                        }
                    });
                return false;
            });
        }
    });
}
 
// validação dos forms (async)
function FormValidation(modal,form)
{   
    jQuery(form).validate({
        rules: {
            NOME: { required: true },
            CONTROLLER: { required: true },
            ACTION: { required: true },
            ICONE: { required: true },
            STATUS: { required: true }
        },
        messages: {
            NOME: { required: null },
            CONTROLLER: { required: null },
            ACTION: { required: null },
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
                    case 'CONTROLLER':
                        PrintAlert('Informe a <span style=\"color:#10e4ea;\">Controller</span> do menu.');
                        return (false);
                        break;
                    case 'ACTION':
                        PrintAlert('Informe a <span style=\"color:#10e4ea;\">Action</span> do menu.');
                        return (false);
                        break;
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
            $.ajax({
                url: $(form).attr('action'),
                type: 'post',
                data: $(form).serialize(),
                success: function (message) { 
                    //console.log(message);
                    $.SmartMessageBox({
                        title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
                        content: '<div style="margin:10px 0px 0px 5px;">' + message + '</div>',
                        buttons: '[Voltar]',
                    }, function (ButtonPressed) {
                        if (ButtonPressed === "Voltar") {
                            $(modal).modal('hide');     
                            $(location).attr('href', window.location.href);
                        }
                    });
                }
            }); 
        }
    });

}

// validação usada somente na edição do menu pai
$('#btnEditMenuMain').click(function () {
    FormValidationMenuMain('#smart-form-edit');
});

// função da validação chamada na funação acima
function FormValidationMenuMain(form) {
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

// function usada
function PrintAlert(alert) {
    $.SmartMessageBox({
        title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
        content: '<div style="margin:10px 0px 0px 5px;">' + alert + '</div>',
        buttons: '[Voltar]'
    });
}

