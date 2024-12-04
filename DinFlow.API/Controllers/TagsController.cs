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

        [HttpGet]
        public IActionResult GetAll()
        {
            var tags = _context.Tags.Where(t => !t.IsDeleted).ToList();
            return Ok(tags);
        }

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

        [HttpPost]
        public IActionResult Post(Tag tag)
        {
            _context.Tags.Add(tag);
            return CreatedAtAction(nameof(GetById), new { id = tag.Id }, tag);
        }

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
