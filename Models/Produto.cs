using projetoWebApi.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace projetoWebApi.Models;

[Table("Produto")]
public class Produto : IValidatableObject
{
    [Key]
    public int ProdutoId { get; set; }
    [Required(ErrorMessage = "O nome é obrigatório")]
    [StringLength(80, ErrorMessage = "O nome deve ter no maximo 80 caracteres")]
    [PrimeiraLetraMaiuscula]
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

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(this.Nome))
        {
            var primeiraLetra = this.Nome[0].ToString();
            if (primeiraLetra != primeiraLetra.ToUpper())
            {
                //Yield é um iterador que irá fazer a ação para cada atributo
                yield return new 
                    ValidationResult("A primeira letra do produto deve ser maiúscula", 
                    new[] 
                    { nameof(this.Nome) }
                    );
            }
            if(this.Estoque <= 0)
            {
                yield return new
                    ValidationResult("O estoque deve ser maior que zero",
                    new[] 
                    { nameof(this.Estoque)}
                    );

            }
        }
    }
}
