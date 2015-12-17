$("#btnDropTrashMessage").click(function (e) {

    var checkeds = $('#inbox-table input[type=checkbox]').map(function () { return $(this).val(); }).toArray();

    if (checkeds.length > 0) {
        $.SmartMessageBox({
            title: "Aviso!",
            content: "Deseja apagar <span style='color:#10e4ea;'>definitivamente</span> o(s) item(s) selecionado(s).",
            buttons: '[Sim][Não]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Sim") {
                var Form = $('#smart-form-messages');
                Form.attr("action", "/Inbox/DeleteDefinitive");
                Form.submit();
            }
            if (ButtonPressed === "Não") {
                var checksboxs = $("#inbox-table input[type=checkbox]:checked");
                checksboxs.closest("tr").removeClass('highlight');
                checksboxs.attr('checked', false);
                return (false);
            }
        });
        e.preventDefault();
    }
    else
    {
        $.SmartMessageBox({
            title: "Aviso!",
            content: "Não existem <span style='color:#10e4ea;'>iten(s)</span> para executar esta ação!",
            buttons: '[Voltar]'
        });
    }
});