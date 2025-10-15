using System.Collections.Generic;

namespace PortariaLight.Domain.Entities
{
    public class Encomenda
    {
        public int IdEncomenda { get; set; }
        public string Descricao { get; set; }
        public DateTime DataRecebida { get; set; }
        public string Status { get; set; }

        public int MoradorId { get; set; }
        public int RetiradaId { get; set; }

        public Morador Morador { get; set; }
        public Retirada Retirada { get; set; }
    }
}
