using System.Collections.ObjectModel;

namespace projetoWebApi.Models;

public class Categoria
{
    public Categoria()
    {
        Produtos = new Collection<Produto>();
    }
    public int CategoriaId { get; set; }
    public string? Nome { get; set; }
    public string? Imagem { get; set; }
    public ICollection<Produto>? Produtos { get; set; }
}

