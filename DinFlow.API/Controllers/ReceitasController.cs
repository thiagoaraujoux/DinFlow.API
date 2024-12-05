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
    /// <summary>
    /// Retorna as receitas cadastradas.
    /// </summary>
    /// <returns>Detalhes da receita.</returns>
    /// <response code="200">Receita encontrada.</response>
    /// <response code="404">Receita não encontrada.</response>
    [HttpGet]
    public IActionResult GetAll()
    {
        var receitas = _context.Receitas.Where(r => !r.IsDeleted).ToList();
        return Ok(receitas);
    }
    /// <summary>
    /// Retorna os detalhes de uma receita específica.
    /// </summary>
    /// <param name="id">ID da receita.</param>
    /// <returns>Detalhes da receita.</returns>
    /// <response code="200">Receita encontrada.</response>
    /// <response code="404">Receita não encontrada.</response>
    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var receita = _context.Receitas.SingleOrDefault(r => r.Id == id);
        if (receita == null) return NotFound();
        return Ok(receita);
    }
    /// <summary>
    /// Cadastra uma nova receita.
    /// </summary>
    /// <param name="receita">Dados da receita.</param>
    /// <returns>Receita criada com sucesso.</returns>
    /// <response code="201">A receita foi criada com sucesso.</response>
    /// <response code="400">Dados inválidos foram enviados na requisição.</response>
    /// <response code="409">Já existe uma receita com o mesmo nome.</response>
    [HttpPost]
    public IActionResult Post(Receita receita)
    {
        _context.Receitas.Add(receita);
        return CreatedAtAction(nameof(GetById), new { id = receita.Id }, receita);
    }
    /// <summary>
    /// Atualiza uma receita existente.
    /// </summary>
    /// <param name="id">ID da receita a ser atualizada.</param>
    /// <param name="input">Objeto com os dados atualizados da receita.</param>
    /// <response code="204">A receita foi atualizada com sucesso.</response>
    /// <response code="404">Nenhuma receita encontrada com o ID especificado.</response>
    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Receita input)
    {
        var receita = _context.Receitas.SingleOrDefault(r => r.Id == id);
        if (receita == null) return NotFound();

        receita.Update(input.Valor, input.Data, input.CategoriaId);
        return NoContent();
    }
    /// <summary>
    /// Exclui uma receita específica pelo ID.
    /// </summary>
    /// <param name="id">ID da receita a ser excluída.</param>
    /// <response code="204">A receita foi marcada como excluída com sucesso.</response>
    /// <response code="404">Nenhuma receita encontrada com o ID especificado.</response>
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var receita = _context.Receitas.SingleOrDefault(r => r.Id == id);
        if (receita == null) return NotFound();

        receita.Delete();
        return NoContent();
    }
}
