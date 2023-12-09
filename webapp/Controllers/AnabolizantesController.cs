using domain.Entities;
using domain.Interface;
using FluentMigrator.Builders.Alter.Column;
using Microsoft.AspNetCore.Mvc;

namespace webapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnabolizantesController : ControllerBase
    {
        private IAnabolizanteRepositorio _repositorio;

        public AnabolizantesController(IAnabolizanteRepositorio repositorio)
        {
            _repositorio = repositorio;
        }

        [HttpGet]
        public IActionResult ObterTodos()
        {
            var anabolizantes = _repositorio.GetAll();
            return Ok(anabolizantes);
        }

        [HttpGet("{id}")]
        public IActionResult ObterPeloId([FromRoute] int id)
        {
            if (id == 0) { return BadRequest(); }
            var bomba = _repositorio.GetById(id);

            if (bomba == null) { return NotFound("Essa bomba ai nao existe"); }

            return Ok(bomba);
        }

        [HttpPost]
        public IActionResult Criar([FromBody] Anabolizante novaBomba)
        {
            if (novaBomba == null) { return BadRequest(); }
            _repositorio.Insert(novaBomba);
            return Created($"novaBomba/{novaBomba}", novaBomba);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar([FromRoute] int id, [FromBody] Anabolizante al)
        {
            if (id == 0) { return NotFound(); }
            if (al == null) { return BadRequest(); }

            al.Id = id;
            _repositorio.Update(al);

            return Ok(al);
        }

        [HttpDelete("{id}")]
        public IActionResult Remover([FromRoute]int id)
        {
            if (id == 0) { return NoContent(); }
            var bomba = _repositorio.GetById(id);

            if (bomba == null) { return NotFound($"Bomba com esse codigo ai nao existe"); }
            _repositorio.Delete(id);

            return Ok();

        }
    }
}
