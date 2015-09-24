function OpenModalPermission(Id) {
    $.ajax({
        url: '/Usuario/PermissionMenuPartial/' + Id,
        cache: false,
        success: function (html) {
            //console.log(html);
            $("#modal-usuario-menu").html(html);
            $("#modal-usuario-menu").modal('show');
        }
    });
}
