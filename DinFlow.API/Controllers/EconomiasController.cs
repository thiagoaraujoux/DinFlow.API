using DinFlow.API.Entities;
using DinFlow.API.Persistence;
using Microsoft.AspNetCore.Mvc;

[Route("api/economias")]
[ApiController]
public class EconomiasController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public EconomiasController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var economias = _context.Economias.Where(e => !e.IsDeleted).ToList();
        return Ok(economias);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var economia = _context.Economias.SingleOrDefault(e => e.Id == id);
        if (economia == null) return NotFound();
        return Ok(economia);
    }

    [HttpPost]
    public IActionResult Post(Economia economia)
    {
        _context.Economias.Add(economia);
        return CreatedAtAction(nameof(GetById), new { id = economia.Id }, economia);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Economia input)
    {
        var economia = _context.Economias.SingleOrDefault(e => e.Id == id);
        if (economia == null) return NotFound();

        economia.Update(input.Valor, input.Data);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var economia = _context.Economias.SingleOrDefault(e => e.Id == id);
        if (economia == null) return NotFound();

        economia.Delete();
        return NoContent();
    }
}
