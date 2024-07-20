using projetoWebApi.Context;
using projetoWebApi.Models;
using projetoWebApi.Pagination;
using projetoWebApi.Repositories.Interfaces;

namespace projetoWebApi.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context) : base(context) { }

        //public IEnumerable<Produto> GetProdutos(ProdutosParameters produtoParams)
        //{
        //    return GetAll()
        //        .OrderBy(p => p.Nome)
        //        .Skip((produtoParams.PageNumber - 1) * produtoParams.PageSize)
        //        .Take(produtoParams.PageSize)
        //        .ToList();
        //}

        public PagedList<Produto> GetProdutos(ProdutosParameters produtoParams)
        {
            var produtos = GetAll().OrderBy(p => p.ProdutoId).AsQueryable();
            var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtoParams.PageNumber, produtoParams.PageSize);
            return produtosOrdenados;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAll().Where(c => c.CategoriaId == id);
        }
    }
}
