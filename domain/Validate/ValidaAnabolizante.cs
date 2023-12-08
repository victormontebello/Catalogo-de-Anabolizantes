using domain.Entities;
using domain.Interface;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Validate
{
    public class ValidaAnabolizante : AbstractValidator<Anabolizante>
    {
        private IAnabolizanteRepositorio _repositorio;
        public ValidaAnabolizante(IAnabolizanteRepositorio repo) {
            _repositorio = repo;

            CascadeMode cascade = CascadeMode.Stop;

            RuleFor(bomba => bomba.Nome)
                .NotNull()
                .WithMessage("Bomba tem q ter nome")
                .NotEmpty()
                .WithMessage("Bomba tem q ter nome")
                .Length(3, 20)
                .WithMessage("O nome não pode ser grande como o Cbum");

            RuleFor(bomba => bomba.Composicao)
                .IsInEnum()
                .WithMessage("Bomba desse tipo ai so tem na gringa")
                .NotEmpty()
                .WithMessage("Quer bomba sem nada toma creatina");

            RuleFor(bomba => bomba.Preco)
                .NotNull()
                .WithMessage("Anabolizante sem preco nao da chefe")
                .NotEmpty()
                .WithMessage("Ta querendo me roubar?")
                .GreaterThan(0)
                .WithMessage("0 reais é sacanagem");
        }

    }
}
