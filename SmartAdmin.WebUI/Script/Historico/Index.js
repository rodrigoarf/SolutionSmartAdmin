runAllForms()

var pagefunction = function () {
    $(".datemask").mask("99/99/9999");
};

pagefunction();


$("#btnBuscarHistorico").click(function () {   
    var dataInicial = $("#DATAINICIAL").val();  
    var dataFinal = $("#DATAFINAL").val();

    if ((dataInicial == '') || (dataFinal == '')) {
        PrintAlert("Você deve preencher o campo Data Inicial e Data Final!");
        return (false);
    } else {
        if (dataInicial > dataFinal) {
            PrintAlert("A Data Inicial deve ser menor que a Data Final!");
            return (false);
        } else
        {
            // executa a busca! 
        }
    } 
});

function PrintAlert(alert) {
    $.SmartMessageBox({
        title: '<i class="fa fa-lg fa-fw fa-exclamation-triangle"></i> Aviso!',
        content: '<div style="margin:10px 0px 0px 5px;">' + alert + '</div>',
        buttons: '[Voltar]'
    });
}