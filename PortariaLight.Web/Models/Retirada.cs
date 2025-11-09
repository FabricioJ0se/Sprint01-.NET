namespace PortariaLight.Web.Models
{
    public class Retirada
    {
        public int IdRetirada { get; set; }
        public DateTime DataRetirada { get; set; } = DateTime.Now;
        public string TokenRetirada { get; set; } = string.Empty;
        public int IdMorador { get; set; }
        public int IdPortaria { get; set; }

        // Propriedades de navegação (opcionais)
        public Morador? Morador { get; set; }
        public Portaria? Portaria { get; set; }
    }
}