using DinFlow.API.Entities;
using DinFlow.API.Persistence;
using Microsoft.AspNetCore.Mvc;

[Route("api/categorias")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CategoriasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var categorias = _context.Categorias.Where(c => !c.IsDeleted).ToList();
        return Ok(categorias);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var categoria = _context.Categorias.SingleOrDefault(c => c.Id == id);
        if (categoria == null) return NotFound();
        return Ok(categoria);
    }

    [HttpPost]
    public IActionResult Post(Categoria categoria)
    {
        _context.Categorias.Add(categoria);
        return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Categoria input)
    {
        var categoria = _context.Categorias.SingleOrDefault(c => c.Id == id);
        if (categoria == null) return NotFound();

        categoria.Update(input.Nome);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var categoria = _context.Categorias.SingleOrDefault(c => c.Id == id);
        if (categoria == null) return NotFound();

        categoria.Delete();
        return NoContent();
    }
}
