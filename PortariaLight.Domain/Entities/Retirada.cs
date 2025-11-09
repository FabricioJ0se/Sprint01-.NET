namespace PortariaLight.Domain.Entities
{
    public class Retirada
    {
        public int IdRetirada { get; set; }
        public DateTime DataRetirada { get; set; }
        public int IdMorador { get; set; }
        public int IdPortaria { get; set; }
    }
}