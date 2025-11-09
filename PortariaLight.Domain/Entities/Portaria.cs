namespace PortariaLight.Domain.Entities
{
    public class Portaria
    {
        public int IdPortaria { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
    }
}