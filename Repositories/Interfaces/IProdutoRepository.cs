using projetoWebApi.Models;
using projetoWebApi.Pagination;

namespace projetoWebApi.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //IEnumerable<Produto> GetProdutos(ProdutosParameters produtoParams);
        PagedList<Produto> GetProdutos(ProdutosParameters produtoParams);
        PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroparams);
        IEnumerable<Produto> GetProdutosPorCategoria(int id);
    }
}
