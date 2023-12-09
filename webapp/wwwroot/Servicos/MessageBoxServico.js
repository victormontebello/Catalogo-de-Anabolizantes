sap.ui.define([
    "sap/m/MessageBox"
], function (MessageBox) {
    "use strict";

    var MessageBoxServico = {
        mostrarMessageBox: function (mensagem, callback, title, actions) {
            MessageBox.confirm(mensagem, {
                title: title || "Confirmação",
                actions: actions || [MessageBox.Action.YES, MessageBox.Action.NO],
                onClose: function (decisao) {
                    if (decisao === MessageBox.Action.YES) {
                        callback(true);
                    } else {
                        callback(false);
                    }
                }
            });
        },

        confirmar: function (mensagem, funcaoCallback, id) {
            return MessageBox.confirm(mensagem, {
                actions: [MessageBox.Action.YES, MessageBox.Action.NO],
                onClose: (acao) => {
                    if (acao === MessageBox.Action.YES) {
                        return funcaoCallback.apply(this, id)
                    }
                    return
                }
            })
        },

        mostrarMensagem: function (mensagem, tipo) {
            const erro = "error";
            const sucesso = "sucesso";
            switch (tipo) {
                case erro:
                    return MessageBox.error(mensagem);
                case sucesso:
                    return MessageBox.success(mensagem);
                default:
                    return MessageBox.information(mensagem);
            }
        }
    };

    MessageBoxServico.mostrarMensagemDeSucessoo = function (mensagem, delay) {
        setTimeout(function () {
            MessageBoxServico.mostrarMensagem(mensagem);
        }, delay);
    };

    return MessageBoxServico;

});