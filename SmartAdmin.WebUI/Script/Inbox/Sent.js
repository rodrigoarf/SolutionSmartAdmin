

$("#btnDropMessage").click(function (e) {

    var checkeds = $('#inbox-table input[type=checkbox]:checked').map(function () { return $(this).val(); }).toArray();

    if (checkeds.length > 0) {
        $.SmartMessageBox({
            title: "Atenção!",
            content: "Deseja realmente enviar para <span style='color:#10e4ea;'>lixeira</span> o(s) " + checkeds.length + " item(s) <span style='color:#10e4ea;'>selecionado(s)</span> ?",
            buttons: '[Sim][Não]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Sim") {
                var Form = $('#smart-form-messages');
                Form.attr("action", "/Inbox/Delete");
                Form.submit();
            }
            if (ButtonPressed === "Não") {
                $("#inbox-table input[type=checkbox]:checked").closest("tr").removeClass('highlight');
                $("#inbox-table input[type=checkbox]:checked").attr('checked', false);
                return (false);
            }
        });
        e.preventDefault();
    } else {
        $.SmartMessageBox({
            title: "Aviso!",
            content: "Você deve selecionar um ou mais <span style='color:#10e4ea;'>iten(s)<span> para executar esta ação!",
            buttons: '[Voltar]'
        });
    }  
});

$("#btnDropMessageSents").click(function (e) {

    var checkeds = $('#inbox-table input[type=checkbox]:checked').map(function () { return $(this).val(); }).toArray();

    if (checkeds.length > 0) {
        $.SmartMessageBox({
            title: "Atenção!",
            content: "Esta pasta não permite adicionar <span style='color:#10e4ea;'>itens a lixeira</span>, somente <span style='color:#10e4ea;'>apagar definitivamente</span>.<br/> Deseja realmente apagar o(s) " + checkeds.length + " item(s) selecionado(s), definitivamente ?",
            buttons: '[Sim][Não]'
        }, function (ButtonPressed) {
            if (ButtonPressed === "Sim") {
                var Form = $('#smart-form-messages');
                Form.attr("action", "/Inbox/DeleteDefinitive");
                Form.submit();
            }
            if (ButtonPressed === "Não") {
                $("#inbox-table input[type=checkbox]:checked").closest("tr").removeClass('highlight');
                $("#inbox-table input[type=checkbox]:checked").attr('checked', false);
                return (false);
            }
        });
        e.preventDefault();
    } else {
        $.SmartMessageBox({
            title: "Aviso!",
            content: "Você deve selecionar um ou mais <span style='color:#10e4ea;'>iten(s)</span> para executar esta ação!",
            buttons: '[Voltar]'
        });
    }
});