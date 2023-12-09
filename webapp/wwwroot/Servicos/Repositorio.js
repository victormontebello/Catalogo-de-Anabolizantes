sap.ui.define([
], function (MessageBoxServico) {
    "use strict";

    var Repositorio = {
        UrlBase: "https://localhost:7208/api/Anabolizantes",
        headers: {
            'Content-Type': 'application/json',
        },
        fetchOptions: {
            headers: this.headers
        }
    };

    Repositorio.obterBombas = async function () {
        try {
            const resposta = await fetch(this.UrlBase);
            return resposta.json();
        }
        catch (erro) {
            MessageBoxServico.mostrarMessageBox(erro.message);
        };
    };

    Repositorio.obterBombaPorId = async function (id) {
        const urlAoObterPorId = `${this.UrlBase}/${id}`;
        try {
            const resposta = await fetch(urlAoObterPorId);
            return resposta.json();
        }
        catch (erro) {
            MessageBoxServico.mostrarMessageBox(erro.message);
        };
    };

    function construirNovoAnabolizante(modeloDeBomba) {
        return {
            nome: modeloDeBomba.nome,
            composicao: modeloDeBomba.composicao,
            vencimento: modeloDeBomba.vencimento,
            injetavel: modeloDeBomba.injetavel,
            preco: modeloDeBomba.preco,
        };
    }

    Repositorio.criarBomba = async function (modeloDeBomba) {
        var novaBomba = construirNovoAnabolizante(modeloDeBomba);
        try {
            const resposta = await fetch(this.UrlBase, {
                method: "POST",
                headers: this.headers,
                body: JSON.stringify(novaBomba),
            });
            return resposta.json();
        }
        catch (erro) {
            MessageBoxServico.mostrarMessageBox(erro.message);
        };
    };

    function construirAnabolizanteAtualizado(modeloDeBomba) {
        return {
            id: modeloDeBomba.id,
            nome: modeloDeBomba.nome,
            composicao: modeloDeBomba.composicao,
            vencimento: modeloDeBomba.vencimento,
            injetavel: modeloDeBomba.injetavel,
            preco: modeloDeBomba.preco,
        };
    }

    Repositorio.atualizarCliente = async function (modeloDeBomba) {
        const idDaBomba = modeloDeBomba.id;
        var bombaAtualizada = construirAnabolizanteAtualizado(modeloDeBomba);
        const urlAoAtualizar = `${this.UrlBase}/${idDaBomba}`;
        try {
            const resposta = await fetch(urlAoAtualizar, {
                method: "PUT",
                headers: this.headers,
                body: JSON.stringify(bombaAtualizada),
            });
            return resposta.json();
        }
        catch (erro) {
            MessageBoxServico.mostrarMessageBox(erro.message);
        };
    };

    Repositorio.removerBomba = async function (id) {
        const urlAoRemover = `${this.UrlBase}/${id}`;
        try {
            const resposta = await fetch(urlAoRemover, {
                method: "DELETE"
            });
            return resposta.json();
        }
        catch (erro) {
            MessageBoxServico.mostrarMessageBox(erro.message);
        };
    };

    return Repositorio;
});