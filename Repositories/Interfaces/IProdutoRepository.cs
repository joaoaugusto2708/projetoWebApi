using projetoWebApi.Models;
using projetoWebApi.Pagination;

namespace projetoWebApi.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //IEnumerable<Produto> GetProdutosAsync(ProdutosParameters produtoParams);
        Task<PagedList<Produto>> GetProdutosAsync(ProdutosParameters produtoParams);
        Task<PagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroparams);
        Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
    }
}
