namespace PortariaLight.Domain.Entities
{
    public class Morador
    {
        public int IdMorador { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
        public int IdApartamento { get; set; }
    }
}