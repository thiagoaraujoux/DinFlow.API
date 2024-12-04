namespace DinFlow.API.Entities
{
    public class Economia
    {
        public Economia()
        {
            IsDeleted = false;
        }

        public Guid Id { get; set; }
        public decimal Valor { get; set; }
        public DateTime Data { get; set; }
        public bool IsDeleted { get; set; }

        public void Update(decimal valor, DateTime data)
        {
            Valor = valor;
            Data = data;
        }

        public void Delete()
        {
            IsDeleted = true;
        }
    }
}
