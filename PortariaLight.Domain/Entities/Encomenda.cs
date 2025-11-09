namespace PortariaLight.Domain.Entities
{
    public class Encomenda
    {
        public int IdEncomenda { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public DateTime DataRecebimento { get; set; }
        public int IdMorador { get; set; }
        public int IdRetirada { get; set; }
    }
}