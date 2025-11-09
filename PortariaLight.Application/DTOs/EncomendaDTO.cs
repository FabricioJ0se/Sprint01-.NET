using System.ComponentModel.DataAnnotations;

namespace PortariaLight.Application.DTOs
{
    public class EncomendaDTO
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Descricao { get; set; } = string.Empty;

        public DateTime DataEntrada { get; set; }
        public bool Retirada { get; set; }

        public string MoradorNome { get; set; } = string.Empty;
        public string MoradorApartamento { get; set; } = string.Empty;
        public int MoradorId { get; set; }

        // HATEOAS Links
        public List<LinkDTO> Links { get; set; } = new();

        public void GenerateLinks(string baseUrl)
        {
            Links.Clear();
            Links.Add(new LinkDTO($"{baseUrl}/api/encomendas/{Id}", "self", "GET"));
            Links.Add(new LinkDTO($"{baseUrl}/api/encomendas/{Id}", "update", "PUT"));
            Links.Add(new LinkDTO($"{baseUrl}/api/encomendas/{Id}", "delete", "DELETE"));

            if (!Retirada)
            {
                Links.Add(new LinkDTO($"{baseUrl}/api/encomendas/{Id}/retirar", "retirar", "POST"));
            }
        }
    }

    public class LinkDTO
    {
        public string Href { get; set; } = string.Empty;
        public string Rel { get; set; } = string.Empty;
        public string Method { get; set; } = string.Empty;

        public LinkDTO() { }

        public LinkDTO(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }

    public class PagedResultDTO<T>
    {
        public List<T> Items { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
        public List<LinkDTO> Links { get; set; } = new();
    }

    public class EncomendaSearchDTO
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