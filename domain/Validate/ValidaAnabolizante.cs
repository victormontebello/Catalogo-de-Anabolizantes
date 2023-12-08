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
                .y



        }

    }
}
