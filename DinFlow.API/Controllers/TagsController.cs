using DinFlow.API.Entities;
using DinFlow.API.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace DinFlow.API.Controllers
{
    [Route("api/tags")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TagsController(ApplicationDbContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Retorna as despesas cadastradas.
        /// </summary>
        /// <returns>Detalhes da despesa.</returns>
        /// <response code="200">despesa encontrada.</response>
        /// <response code="404">despesa não encontrada.</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var tags = _context.Tags.Where(t => !t.IsDeleted).ToList();
            return Ok(tags);
        }
        /// <summary>
        /// Retorna os detalhes de uma despesa específica.
        /// </summary>
        /// <param name="id">ID da despesa.</param>
        /// <returns>Detalhes da despesa.</returns>
        /// <response code="200">despesa encontrada.</response>
        /// <response code="404">despesa não encontrada.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var tag = _context.Tags.SingleOrDefault(t => t.Id == id);
            if (tag == null)
            {
                return NotFound();
            }
            return Ok(tag);
        }
        /// <summary>
        /// Cadastra uma nova despesa.
        /// </summary>
        /// <param name="despesa">Dados da despesa.</param>
        /// <returns>despesa criada com sucesso.</returns>
        /// <response code="201">A despesa foi criada com sucesso.</response>
        /// <response code="400">Dados inválidos foram enviados na requisição.</response>
        /// <response code="409">Já existe uma despesa com o mesmo nome.</response>
        [HttpPost]
        public IActionResult Post(Tag tag)
        {
            _context.Tags.Add(tag);
            return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
        }
        /// <summary>
        /// Atualiza uma despesa existente.
        /// </summary>
        /// <param name="id">ID da despesa a ser atualizada.</param>
        /// <param name="input">Objeto com os dados atualizados da despesa.</param>
        /// <response code="204">A despesa foi atualizada com sucesso.</response>
        /// <response code="404">Nenhuma despesa encontrada com o ID especificado.</response>
        [HttpPut("{id}")]
        public IActionResult Update(Guid id, Tag input)
        {
            var tag = _context.Tags.SingleOrDefault(t => t.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            tag.Update(input.Nome);
            return NoContent(); // Como está em memória, não há problema de atualização direta.
        }
         /// <summary>
        /// Exclui uma despesa específica pelo ID.
        /// </summary>
        /// <param name="id">ID da despesa a ser excluída.</param>
        /// <response code="204">A despesa foi marcada como excluída com sucesso.</response>
        /// <response code="404">Nenhuma despesa encontrada com o ID especificado.</response>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var tag = _context.Tags.SingleOrDefault(t => t.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            tag.Delete();
            return NoContent();
        }
        /// <summary>
        /// Associa uma receita existente a uma tag específica.
        /// </summary>
        /// <param name="id">ID da tag que será associada à receita.</param>
        /// <param name="receitaId">ID da receita que será associada à tag.</param>
        /// <response code="204">Receita associada à tag com sucesso.</response>
        /// <response code="404">A tag ou a receita especificada não foi encontrada.</response>
        /// <response code="400">Dados inválidos ou inconsistentes foram enviados na requisição.</response>
        [HttpPost("{id}/receitas")]
        public IActionResult AddReceita(Guid id, Guid receitaId)
        {
            var tag = _context.Tags.SingleOrDefault(t => t.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            if (!tag.Receitas.Contains(receitaId))
            {
                tag.Receitas.Add(receitaId);
            }

            return NoContent();
        }
        /// <summary>
        /// Associa uma despesa existente a uma tag específica.
        /// </summary>
        /// <param name="id">ID da tag que será associada à despesa.</param>
        /// <param name="despesaId">ID da despesa que será associada à tag.</param>
        /// <response code="204">Despesa associada à tag com sucesso.</response>
        /// <response code="404">A tag ou a despesa especificada não foi encontrada.</response>
        /// <response code="400">Dados inválidos ou inconsistentes foram enviados na requisição.</response>
        [HttpPost("{id}/despesas")]
        public IActionResult AddDespesa(Guid id, Guid despesaId)
        {
            var tag = _context.Tags.SingleOrDefault(t => t.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            if (!tag.Despesas.Contains(despesaId))
            {
                tag.Despesas.Add(despesaId);
            }

            return NoContent();
        }
    }
}
