using System.ComponentModel.DataAnnotations;

namespace PortariaLight.Web.ViewModels
{
    public class EncomendaViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Descrição é obrigatória")]
        [StringLength(200, ErrorMessage = "Descrição muito longa")]
        public string Descricao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Morador é obrigatório")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione um morador válido")]
        public int MoradorId { get; set; }

        public DateTime DataEntrada { get; set; } = DateTime.Now;
        public bool Retirada { get; set; }

        // Para exibição
        public string? MoradorNome { get; set; }
        public string? MoradorApartamento { get; set; }

        // Lista de moradores para dropdown
        public List<MoradorOption> Moradores { get; set; } = new();
    }

    public class MoradorOption
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Apartamento { get; set; } = string.Empty;
    }

    public class EncomendaSearchViewModel
    {
        public string? Descricao { get; set; }
        public string? MoradorNome { get; set; }
        public bool? Retirada { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "DataEntrada";
        public bool SortDescending { get; set; } = true;
    }
}