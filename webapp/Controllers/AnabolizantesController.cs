using domain.Interface;
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
       



    }
}
