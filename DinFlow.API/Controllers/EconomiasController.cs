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
    /// <summary>
    /// Retorna as Economias cadastradas.
    /// </summary>
    /// <returns>Detalhes das Economias.</returns>
    /// <response code="200">Economia encontrada.</response>
    /// <response code="404">Economia não encontrada.</response>
    [HttpGet]
    public IActionResult GetAll()
    {
        var economias = _context.Economias.Where(e => !e.IsDeleted).ToList();
        return Ok(economias);
    }
    /// <summary>
    /// Retorna os detalhes de uma Economias específica.
    /// </summary>
    /// <param name="id">ID da Economias.</param>
    /// <returns>Detalhes da Economias.</returns>
    /// <response code="200">Economia encontrada.</response>
    /// <response code="404">Economia não encontrada.</response>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var economia = _context.Economias.SingleOrDefault(e => e.Id == id);
        if (economia == null) return NotFound();
        return Ok(economia);
    }
    /// <summary>
    /// Cadastra uma nova economia.
    /// </summary>
    /// <param name="economia">Dados da economia.</param>
    /// <returns>Receita criada com sucesso.</returns>
    /// <response code="201">A economia foi criada com sucesso.</response>
    /// <response code="400">Dados inválidos foram enviados na requisição.</response>
    /// <response code="409">Já existe uma economia com o mesmo nome.</response>
    [HttpPost]
    public IActionResult Post(Economia economia)
    {
        _context.Economias.Add(economia);
        return CreatedAtAction(nameof(GetById), new { id = economia.Id }, economia);
    }
    /// <summary>
    /// Atualiza uma economia existente.
    /// </summary>
    /// <param name="id">ID da economia a ser atualizada.</param>
    /// <param name="input">Objeto com os dados atualizados da economia.</param>
    /// <response code="204">A economia foi atualizada com sucesso.</response>
    /// <response code="404">Nenhuma economia encontrada com o ID especificado.</response>
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Economia input)
    {
        var economia = _context.Economias.SingleOrDefault(e => e.Id == id);
        if (economia == null) return NotFound();

        economia.Update(input.Valor, input.Data);
        return NoContent();
    }
    /// <summary>
    /// Exclui uma economia específica pelo ID.
    /// </summary>
    /// <param name="id">ID da economia a ser excluída.</param>
    /// <response code="204">A economia foi marcada como excluída com sucesso.</response>
    /// <response code="404">Nenhuma economia encontrada com o ID especificado.</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var economia = _context.Economias.SingleOrDefault(e => e.Id == id);
        if (economia == null) return NotFound();

        economia.Delete();
        return NoContent();
    }
}
