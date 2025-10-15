using System.Collections.Generic;

namespace PortariaLight.Domain.Entities
{
    public class Morador
    {
        public int IdMorador { get; set; }
        public string Nome { get; set; }
        public int Apartamento { get; set; }
        public int Bloco { get; set; }
        public string Contato { get; set; }

        public ICollection<Retirada> Retiradas { get; set; }
        public ICollection<Encomenda> Encomendas { get; set; }
    }
}
