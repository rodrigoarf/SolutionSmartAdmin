runAllForms();

$('#btnSaveCedente').click(function () {
    FormValidation('#smart-form-edit');
});

function FormValidation(form)
{
    $(form).validate({
        rules: {
            NOME: { required: true },
            ENDERECO: { required: true },
            NUMERO: { required: true },
            BAIRRO: { required: true },
            CIDADE: { required: true },
            UF: { required: true },
            CEP: { required: true },
            DTH_NASCIMENTO: { required: true },
            COD_BANCO: { required: true },
            NUM_AGENCIA: { required: true },
            NUM_CONTA_CORRENTE: { required: true },
            CPF_CNPJ: { required: true },
            INSTRUCAO_BOLETO: { required: true }
        },
        messages: {
            NOME: { required: null },
            ENDERECO: { required: null },
            NUMERO: { required: null },
            BAIRRO: { required: null },
            CIDADE: { required: null },
            UF: { required: null },
            CEP: { required: null },
            DTH_NASCIMENTO: { required: null },
            COD_BANCO: { required: null },
            NUM_AGENCIA: { required: null },
            NUM_CONTA_CORRENTE: { required: null },
            CPF_CNPJ: { required: null },
            INSTRUCAO_BOLETO: { required: null }
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
                        PrintAlert('Informe o <span style="color:#10e4ea;">Nome</span> completo do cedente.');
                        return (false);
                        break;
                    case 'ENDERECO':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Endereço</span> do cedente.');
                        return (false);
                        break;
                    case 'NUMERO':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Número</span> do cedente.');
                        return (false);
                        break;
                    case 'BAIRRO':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Bairro</span>. do cedente.');
                        return (false);
                        break;
                    case 'CIDADE':
                        PrintAlert('Informe a <span style="color:#10e4ea;">Cidade</span> do cedente.');
                        return (false);
                        break;
                    case 'UF':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Estado</span> do cedente.');
                        return (false);
                        break;
                    case 'CEP':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Cep</span> do cedente.');
                        return (false);
                        break;
                    case 'DTH_NASCIMENTO':
                        PrintAlert('Informe a <span style="color:#10e4ea;">Data de Nascimento</span> do cedente.');
                        return (false);
                        break;  
                    case 'COD_BANCO':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Banco</span> do cedente para deposito.');
                        return (false);
                        break;
                    case 'NUM_AGENCIA':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Número da agência</span> para depósito.');
                        return (false);
                        break;
                    case 'NUM_CONTA_CORRENTE':
                        PrintAlert('Informe o <span style="color:#10e4ea;">Número da conta corrente</span> para depósito.');
                        return (false);
                        break;
                    case 'CPF_CNPJ':
                        PrintAlert('Informe o <span style="color:#10e4ea;">CPF</span> ou <span style="color:#10e4ea;">CNPJ</span> do cedente.');
                        return (false);
                        break;
                    case 'INSTRUCAO_BOLETO':
                        PrintAlert('Informe as <span style="color:#10e4ea;">Instruções</span> referente ao boleto.');
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