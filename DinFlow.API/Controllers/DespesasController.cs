using DinFlow.API.Entities;
using DinFlow.API.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DinFlow.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DespesasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DespesasController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna as categorias cadastradas.
        /// </summary>
        /// <returns>Detalhes da categoria.</returns>
        /// <response code="200">Categoria encontrada.</response>
        /// <response code="404">Categoria não encontrada.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var despesas = _context.Despesas.Where(r => !r.IsDeleted).ToList();
            return Ok(despesas);
        }
        /// <summary>
        /// Retorna os detalhes de uma categoria específica.
        /// </summary>
        /// <param name="id">ID da categoria.</param>
        /// <returns>Detalhes da categoria.</returns>
        /// <response code="200">Categoria encontrada.</response>
        /// <response code="404">Categoria não encontrada.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var despesa = _context.Despesas.SingleOrDefault(r => r.Id == id);
            if (despesa == null) return NotFound();
            return Ok(despesa);
        }
        /// <summary>
        /// Cadastra uma nova categoria.
        /// </summary>
        /// <param name="categoria">Dados da categoria.</param>
        /// <returns>Categoria criada com sucesso.</returns>
        /// <response code="201">A categoria foi criada com sucesso.</response>
        /// <response code="400">Dados inválidos foram enviados na requisição.</response>
        /// <response code="409">Já existe uma categoria com o mesmo nome.</response>
        [HttpPost]
        public IActionResult Post(Despesa despesa)
        {
            _context.Despesas.Add(despesa);
            return CreatedAtAction(nameof(GetById), new { id = despesa.Id }, despesa);
        }
        /// <summary>
        /// Atualiza uma categoria existente.
        /// </summary>
        /// <param name="id">ID da categoria a ser atualizada.</param>
        /// <param name="input">Objeto com os dados atualizados da categoria.</param>
        /// <response code="204">A categoria foi atualizada com sucesso.</response>
        /// <response code="404">Nenhuma categoria encontrada com o ID especificado.</response>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Despesa input)
        {
            var despesa = _context.Despesas.SingleOrDefault(r => r.Id == id);
            if (despesa == null) return NotFound();

            despesa.Update(input.Valor, input.Data, input.CategoriaId);
            return NoContent();
        }
        /// <summary>
        /// Exclui uma categoria específica pelo ID.
        /// </summary>
        /// <param name="id">ID da categoria a ser excluída.</param>
        /// <response code="204">A categoria foi marcada como excluída com sucesso.</response>
        /// <response code="404">Nenhuma categoria encontrada com o ID especificado.</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var despesa = _context.Despesas.SingleOrDefault(r => r.Id == id);
            if (despesa == null) return NotFound();

            despesa.Delete();
            return NoContent();
        }
    }
}