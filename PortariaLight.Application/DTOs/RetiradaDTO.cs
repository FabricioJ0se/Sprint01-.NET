namespace PortariaLight.Application.DTOs
{
    public class RetiradaDTO
    {
        public int IdRetirada { get; set; }
        public DateTime DataRetirada { get; set; }
        public string TokenRetirada { get; set; }
        public int MoradorId { get; set; }
        public int PortariaId { get; set; }
    }
}
