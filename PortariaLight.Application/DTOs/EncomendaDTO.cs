namespace PortariaLight.Application.DTOs
{
    public class EncomendaDTO
    {
        public int IdEncomenda { get; set; }
        public string Descricao { get; set; }
        public DateTime DataRecebida { get; set; }
        public string Status { get; set; }
        public int MoradorId { get; set; }
        public int? RetiradaId { get; set; }
    }
}
