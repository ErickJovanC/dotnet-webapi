using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers;
[ApiController] // Realiza validaciones de forma automatica'
[Route("api/autores")]  // Ruta para acceder al metodo del controlador
public class AutoresController: ControllerBase
{
    private readonly ApplicationDbContext context;
    private readonly ILogger<AutoresController> logger;

    public AutoresController(ApplicationDbContext context, ILogger<AutoresController> logger)
    {
        this.context = context;
        this.logger = logger;
    }

    // Obtener los regisstros de la DB
    [HttpGet]
    public async Task<ActionResult<List<Autor>>> Get()
    {
        return await context.Autores.Include(XmlConfigurationExtensions => XmlConfigurationExtensions.Libros).ToListAsync();
    }

    // Enviar Registros a la DB
    [HttpPost]
    public async Task<ActionResult> Post(Autor autor)
    {
        context.Add(autor);
        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut("{id:int}")] // api/autores/1
    public async Task<ActionResult> Put(Autor autor, int id)
    {
        // En caso de inconsitencia del Id
        if (autor.Id != id )
        {
            return BadRequest("El id del autor no coincide con el id de la URL");
        }

        // Valida la existencia del elemento en la DB
        var existe = await context.Autores.AnyAsync(x => x.Id == id);

        if (!existe) {
            return NotFound();
        }

        context.Update(autor); // Establece el tipo acción
        await context.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete("{id:int}")] //api/autores/2
    public async Task<ActionResult> Delete(int id)
    {
        var existe = await context.Autores.AnyAsync(x => x.Id == id);

        if (!existe) {
            return NotFound();
        }

        //Crear una instancia y la carga para enviarla a Entity
        context.Remove(new Autor() {Id = id});
        await context.SaveChangesAsync();

        return Ok();
    }
}