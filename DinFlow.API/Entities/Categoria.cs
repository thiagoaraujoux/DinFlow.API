namespace DinFlow.API.Entities
{
    public class Categoria
    {
        public Categoria()
        {
            IsDeleted = false;
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public bool IsDeleted { get; set; }

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
