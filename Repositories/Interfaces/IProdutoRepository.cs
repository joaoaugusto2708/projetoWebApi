using projetoWebApi.Models;
using projetoWebApi.Pagination;
using X.PagedList;

namespace projetoWebApi.Repositories.Interfaces
{
    public interface IProdutoRepository : IRepository<Produto>
    {
        //IEnumerable<Produto> GetProdutosAsync(ProdutosParameters produtoParams);
        Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtoParams);
        Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroparams);
        Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id);
    }
}
