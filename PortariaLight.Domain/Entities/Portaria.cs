using System;
using System.Collections.Generic;

namespace PortariaLight.Domain.Entities
{
    public class Portaria
    {
        public int IdPortaria { get; set; }
        public string NomePorteiro { get; set; }
        public string Turno { get; set; }
        public string Contato { get; set; }
        public DateTime DataRegistro { get; set; }

        public ICollection<Retirada> Retiradas { get; set; }
    }
}
