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
        content: vMensaje ,
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

function p_AEPSAD_delete_confirm(vURL,vMensaje) {
    
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
                    location.href = vURL;
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