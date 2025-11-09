using System.ComponentModel.DataAnnotations;

namespace PortariaLight.Application.DTOs
{
    public class RetiradaDTO
    {
        public int Id { get; set; }

        [Required]
        public int MoradorId { get; set; } 

        [Required]
        public int PortariaId { get; set; } 

        [Required]
        public DateTime DataRetirada { get; set; } = DateTime.Now;

        [StringLength(100)]
        public string? RetiradoPor { get; set; }

        [StringLength(500)]
        public string? Observacoes { get; set; }

        // Dados para exibição
        public string EncomendaDescricao { get; set; } = string.Empty;
        public string MoradorNome { get; set; } = string.Empty;
        public string MoradorApartamento { get; set; } = string.Empty;

        // HATEOAS Links
        public List<LinkDTO> Links { get; set; } = new();

        public void GenerateLinks(string baseUrl)
        {
            Links.Clear();
            Links.Add(new LinkDTO($"{baseUrl}/api/retiradas/{Id}", "self", "GET"));
            Links.Add(new LinkDTO($"{baseUrl}/api/retiradas/{Id}", "update", "PUT"));
            Links.Add(new LinkDTO($"{baseUrl}/api/retiradas/{Id}", "delete", "DELETE"));
            Links.Add(new LinkDTO($"{baseUrl}/api/moradores/{MoradorId}", "morador", "GET"));
            Links.Add(new LinkDTO($"{baseUrl}/api/portarias/{PortariaId}", "portaria", "GET"));
        }
    }

    public class RetiradaSearchDTO
    {
        public string? RetiradoPor { get; set; }
        public string? MoradorNome { get; set; }
        public string? EncomendaDescricao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "DataRetirada";
        public bool SortDescending { get; set; } = true;
    }
}