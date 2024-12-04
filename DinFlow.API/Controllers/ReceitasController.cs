using DinFlow.API.Entities;
using DinFlow.API.Persistence;
using Microsoft.AspNetCore.Mvc;

[Route("api/receitas")]
[ApiController]
public class ReceitasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public ReceitasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var receitas = _context.Receitas.Where(r => !r.IsDeleted).ToList();
        return Ok(receitas);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var receita = _context.Receitas.SingleOrDefault(r => r.Id == id);
        if (receita == null) return NotFound();
        return Ok(receita);
    }

    [HttpPost]
    public IActionResult Post(Receita receita)
    {
        _context.Receitas.Add(receita);
        return CreatedAtAction(nameof(GetById), new { id = receita.Id }, receita);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Receita input)
    {
        var receita = _context.Receitas.SingleOrDefault(r => r.Id == id);
        if (receita == null) return NotFound();

        receita.Update(input.Valor, input.Data, input.CategoriaId);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var receita = _context.Receitas.SingleOrDefault(r => r.Id == id);
        if (receita == null) return NotFound();

        receita.Delete();
        return NoContent();
    }
}
