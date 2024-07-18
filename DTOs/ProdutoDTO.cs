using projetoWebApi.Models;

namespace projetoWebApi.DTOs
{
    public class ProdutoDTO
    {       
        public int ProdutoId { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public string? ImagemUrl { get; set; }
        public decimal Preco { get; set; }
        public float Estoque { get; set; }
        public DateTime DataCadastro { get; set; }
        public int CategoriaId { get; set; }
    }
}
