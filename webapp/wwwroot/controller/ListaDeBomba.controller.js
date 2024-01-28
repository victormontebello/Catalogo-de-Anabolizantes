sap.ui.define(
    [
        "sap/ui/core/mvc/Controller",
        "sap/ui/model/json/JSONModel",
        "../Servicos/Repositorio"
    ],
    function (Controller, JSONModel, Repositorio) {
        "use strict";
        const caminhoControladorDeListagem = "ui5.anabolizantes.controller.ListaDeBomba";
        return Controller.extend(caminhoControladorDeListagem, {
            onInit: function () {
                const paginaListagem = "listaDeBomba";
                this.getOwnerComponent().getRouter().getRoute(paginaListagem).attachMatched(this.aoCoincidirRota, this);
            },

            aoCoincidirRota: function () {
                this.carregarDadosBombasApi();
            },

            carregarDadosBombasApi: async function () {
                this._processarEvento(async () => {
                    var modeloDeAnabolizantes = new JSONModel();
                    const dados = await Repositorio.obterBombas();
                    modeloDeBomba.setData({ bombas: dados });
                    this.getView().setModel(modeloDeBomba);
                });
            },

            _processarEvento: async function (action) {
                const tipoDaPromessa = "catch";
                const tipoBuscado = "function";
                try {
                    BusyIndicator.show();
                    var promessa = action();
                    if (promessa && typeof promessa[tipoDaPromessa] === tipoBuscado) {
                        await promessa.catch(error => MessageBoxServico.mostrarMensagem(error.message));
                    }
                } catch (error) {
                    MessageBoxServico.mostrarMensagem(error.message);
                } finally {
                    BusyIndicator.hide();
                }
            },

            /*navegarParaPaginaDeCadastro: function () {
                this._processarEvento(() => {
                    const paginaDeCadastro = "cadastro";
                    this.aoNavegar(paginaDeCadastro);
                });
            },*/

            /*aoClicarNoCliente: function (evento) {
                this._processarEvento(() => {
                    const paginaDedetalhes = "detalhes";
                    const idCLiente = "id";
                    var idObtido = evento.getSource().getBindingContext().getProperty(idCLiente);
                    this.getOwnerComponent().getRouter().navTo(paginaDedetalhes, { id: idObtido });
                });
            }*/
        });
    }
);