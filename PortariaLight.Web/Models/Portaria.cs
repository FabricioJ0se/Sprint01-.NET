namespace PortariaLight.Web.Models
{
    public class Portaria
    {
        public int IdPortaria { get; set; }
        public string NomePorteiro { get; set; } = string.Empty;
        public string Turno { get; set; } = string.Empty;
        public string Contato { get; set; } = string.Empty;
        public DateTime DataRegistro { get; set; } = DateTime.Now;
    }
}