using System.ComponentModel.DataAnnotations;

namespace PortariaLight.Application.DTOs
{
    public class PortariaDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome muito longo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Localização é obrigatória")]
        [StringLength(200, ErrorMessage = "Localização muito longa")]
        public string Localizacao { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; } = string.Empty;

        [EmailAddress(ErrorMessage = "Email inválido")]
        public string? Email { get; set; }

        public string? Responsavel { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;

        // Estatísticas
        public int TotalEncomendasHoje { get; set; }
        public int EncomendasPendentes { get; set; }
        public int TotalMoradores { get; set; }

        // HATEOAS Links
        public List<LinkDTO> Links { get; set; } = new();

        public void GenerateLinks(string baseUrl)
        {
            Links.Clear();
            Links.Add(new LinkDTO($"{baseUrl}/api/portarias/{Id}", "self", "GET"));
            Links.Add(new LinkDTO($"{baseUrl}/api/portarias/{Id}", "update", "PUT"));
            Links.Add(new LinkDTO($"{baseUrl}/api/portarias/{Id}", "delete", "DELETE"));
            Links.Add(new LinkDTO($"{baseUrl}/api/portarias/{Id}/estatisticas", "estatisticas", "GET"));
        }
    }

    public class PortariaSearchDTO
    {
        public string? Nome { get; set; }
        public string? Localizacao { get; set; }
        public string? Responsavel { get; set; }
        public bool? Ativo { get; set; } = true;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Nome";
        public bool SortDescending { get; set; } = false;
    }

    public class EstatisticasPortariaDTO
    {
        public int PortariaId { get; set; }
        public string PortariaNome { get; set; } = string.Empty;
        public int TotalEncomendasHoje { get; set; }
        public int EncomendasPendentes { get; set; }
        public int TotalMoradores { get; set; }
        public int RetiradasHoje { get; set; }
        public DateTime DataConsulta { get; set; } = DateTime.Now;
    }
}