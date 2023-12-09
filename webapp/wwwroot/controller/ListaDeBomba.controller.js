sap.ui.define(
    [
        "./BaseController.controller",
        "sap/ui/model/json/JSONModel",
        "sap/ui/model/Filter",
        "sap/ui/model/FilterOperator",
        "../Servicos/Repositorio"
    ],
    function (BaseController, JSONModel, Filter, FilterOperator, Repositorio) {
        "use strict";
        const caminhoControladorDeListagem = "ui5.anabolizantes.ListaDeBomba";
        return BaseController.extend(caminhoControladorDeListagem, {
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

            filtrarCliente: function (evento) {
                this._processarEvento(() => {
                    const idTabelaCliente = "idTabelaCliente";
                    const consulta = "query";
                    const items = "items";
                    const nomeCliente = "nome";

                    var arrayFiltro = [];
                    var valorDigitadoUsuario = evento.getParameter(consulta);
                    if (valorDigitadoUsuario) {
                        arrayFiltro.push(new Filter(nomeCliente, FilterOperator.Contains, valorDigitadoUsuario));
                    }
                    var obterIdTabela = this.byId(idTabelaCliente);
                    var bindingClienteTabela = obterIdTabela.getBinding(items);
                    bindingClienteTabela.filter(arrayFiltro);
                })
            },

            navegarParaPaginaDeCadastro: function () {
                this._processarEvento(() => {
                    const paginaDeCadastro = "cadastro";
                    this.aoNavegar(paginaDeCadastro);
                });
            },

            aoClicarNoCliente: function (evento) {
                this._processarEvento(() => {
                    const paginaDedetalhes = "detalhes";
                    const idCLiente = "id";
                    var idObtido = evento.getSource().getBindingContext().getProperty(idCLiente);
                    this.getOwnerComponent().getRouter().navTo(paginaDedetalhes, { id: idObtido });
                });
            }
        });
    }
);