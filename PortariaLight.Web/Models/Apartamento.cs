namespace PortariaLight.Web.Models
{
    public class Apartamento
    {
        public int IdApartamento { get; set; }
        public int? Torre { get; set; }
        public string Bloco { get; set; } = string.Empty;
        public string Numero { get; set; } = string.Empty;
    }
}