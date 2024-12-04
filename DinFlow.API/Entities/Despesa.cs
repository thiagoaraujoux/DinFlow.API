namespace DinFlow.API.Entities
{
    public class Despesa
    {
        public Despesa()
        {
            Tags = new List<Guid>();
            IsDeleted = false;
        }

        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public Guid CategoriaId { get; set; }
        public List<Guid> Tags { get; set; } // IDs das tags relacionadas
        public bool IsDeleted { get; set; }

        public void Update(decimal valor, DateTime data, Guid categoriaId)
        {
            Valor = valor;
            Data = data;
            CategoriaId = categoriaId;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
