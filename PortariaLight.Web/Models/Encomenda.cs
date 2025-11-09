namespace PortariaLight.Web.Models
{
    public class Encomenda
    {
        public int IdEncomenda { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataRecebida { get; set; } = DateTime.Now;
        public string Status { get; set; } = string.Empty;
        public int IdMorador { get; set; }
        public int IdRetirada { get; set; }
        public int? IdPorteiro { get; set; }

        // Propriedades de navegação (opcionais)
        public Morador? Morador { get; set; }
        public Retirada? Retirada { get; set; }
        public Portaria? Porteiro { get; set; }
    }
}