using System;
using System.Collections.Generic;

namespace PortariaLight.Domain.Entities
{
    public class Retirada
    {
        public int IdRetirada { get; set; }
        public DateTime DataRetirada { get; set; }
        public string TokenRetirada { get; set; }

        public int MoradorId { get; set; }
        public int PortariaId { get; set; }

        public Morador Morador { get; set; }
        public Portaria Portaria { get; set; }
        public ICollection<Encomenda> Encomendas { get; set; }
    }
}
