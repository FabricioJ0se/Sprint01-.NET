using System.ComponentModel.DataAnnotations;

namespace PortariaLight.Application.DTOs
{
    public class MoradorDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100, ErrorMessage = "Nome muito longo")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Apartamento é obrigatório")]
        public string Apartamento { get; set; } = string.Empty;

        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;

        // HATEOAS Links
        public List<LinkDTO> Links { get; set; } = new();

        public void GenerateLinks(string baseUrl)
        {
            Links.Clear();
            Links.Add(new LinkDTO($"{baseUrl}/api/moradores/{Id}", "self", "GET"));
            Links.Add(new LinkDTO($"{baseUrl}/api/moradores/{Id}", "update", "PUT"));
            Links.Add(new LinkDTO($"{baseUrl}/api/moradores/{Id}", "delete", "DELETE"));
            Links.Add(new LinkDTO($"{baseUrl}/api/moradores/{Id}/encomendas", "encomendas", "GET"));
        }
    }

    public class MoradorSearchDTO
    {
        public string? Nome { get; set; }
        public string? Apartamento { get; set; }
        public string? Email { get; set; }
        public bool? Ativo { get; set; } = true;
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Nome";
        public bool SortDescending { get; set; } = false;
    }
}