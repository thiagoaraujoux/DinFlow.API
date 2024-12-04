using DinFlow.API.Entities;

namespace DinFlow.API.Persistence
{
    public class ApplicationDbContext
    {
        public List<Tag> Tags { get; set; }
        public List<Receita> Receitas { get; set; }
        public List<Despesa> Despesas { get; set; }
        public List<Categoria> Categorias { get; set; }
        public List<Economia> Economias { get; set; }

        public ApplicationDbContext()
        {
            Tags = new List<Tag>();
            Receitas = new List<Receita>();
            Despesas = new List<Despesa>();
            Categorias = new List<Categoria>();
            Economias = new List<Economia>();


        }
    }
}
