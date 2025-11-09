namespace PortariaLight.Domain.Entities
{
    public class Apartamento
    {
        public int IdApartamento { get; set; }
        public string Numero { get; set; } = string.Empty;
        public string Bloco { get; set; } = string.Empty;
    }
}