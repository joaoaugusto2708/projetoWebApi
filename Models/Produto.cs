using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace projetoWebApi.Models;

[Table("Produto")]
public class Produto
{
    [Key]
    public int ProdutoId { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome deve ter no maximo 80 caracteres")]
    public string? Nome { get; set; }
    [Required]
    [StringLength(300)]
    public string? Descricao { get; set; }
    [Required]
    [StringLength(300)]
    public string? ImagemUrl { get; set; }
    [Required]
    [Column(TypeName="decimal(10,2)")]
    [Range(1, 1000, ErrorMessage = "O preço deve estar entre {1} e {2}")]
    public decimal Preco { get; set; }
    public float Estoque { get; set; }
    public DateTime DataCadastro { get; set; }
    public int CategoriaId { get; set; }
    //[JsonIgnore] Igonora na serialização e nas deserialização
    [JsonIgnore]
    public Categoria? Categoria { get; set; }
}
