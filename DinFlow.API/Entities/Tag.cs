namespace DinFlow.API.Entities
{
    public class Tag
    {
        public Tag()
        {
            Receitas = new List<Guid>();
            Despesas = new List<Guid>();
            IsDeleted = false;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool IsDeleted { get; set; }

        public List<Guid> Receitas { get; set; } // IDs das receitas relacionadas
        public List<Guid> Despesas { get; set; } // IDs das despesas relacionadas

        public void Update(string nome)
        {
            Nome = nome;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
