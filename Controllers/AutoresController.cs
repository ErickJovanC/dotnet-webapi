using Microsoft.AspNetCore.Mvc;

namespace WebApiAutores.Controllers
{
    [ApiController] // Realiza validaciones de forma automatica'
    [Route("api/autores")]  // Ruta para acceder al metodo del controlador
    public class AutoresController: ControllerBase
    {
        [HttpGet] // Establece el método de acceso
        public ActionResult<List<Autor>> Get()
        {
            return new List<Autor>() {
                new Autor() {Id = 1, Nombre = "Juan"},
                new Autor() {Id = 2, Nombre = "Pedro"},
                new Autor() {Id = 3, Nombre = "Erick"},
            };
        }
    }
}