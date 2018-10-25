function p_AEPSAD_loading(vMensaje) {
    $.alert({
        icon: 'fa fa-spinner fa-spin',
        title: 'Intranet - AEPSAD',
        content: vMensaje
    });
}

function p_AEPSAD_save_ok(vMensaje) {

    $.confirm({
        icon: 'fas fa-check',
        title: 'Intranet - AEPSAD',
        content: vMensaje,
        type: 'blue',
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'Cerrar',
                btnClass: 'btn-blue',
                action: function () {
                }
            }
        }
    });
}

function p_AEPSAD_error(vMensaje) {

    $.confirm({
        icon: 'fas fa-times',
        title: 'Intranet - AEPSAD',
        content: vMensaje,
        type: 'red',
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'Cerrar',
                btnClass: 'btn-red',
                action: function () {
                }
            }
        }
    });
}

function p_AEPSAD_delete_confirm(vUrlDelete, vMensaje) {

    console.log("p_AEPSAD_delete_confirm");

    $.confirm({
        icon: 'fas fa-check',
        title: 'Intranet - AEPSAD',
        content: vMensaje,
        type: 'blue',
        typeAnimated: true,
        buttons: {
            tryAgain: {
                text: 'Borrar',
                btnClass: 'btn-red',
                action: function () {

                    var fullUrl = window.location.origin + vUrlDelete;

                    $.ajax({
                        url: fullUrl,
                        datatype: "text",
                        type: "get",
                        contentType: "application/json",
                        async: true,
                        success: function (data) {
                            p_AEPSAD_save_ok("El elemento se ha eliminado correctamente");    
                            window.location.href = location.href;
                        },
                        error: function (data) {
                            p_AEPSAD_error(data);
                        }
                    });

                }
            },
            cancel: {
                text: 'Cancelar',
                btnClass: 'btn-blue',
                action: function () {

                }
            }

        }
    });

}