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
        //    return GetAllAsync()
        //        .OrderBy(p => p.Nome)
        //        .Skip((produtoParams.PageNumber - 1) * produtoParams.PageSize)
        //        .Take(produtoParams.PageSize)
        //        .ToList();
        //}

        public PagedList<Produto> GetProdutos(ProdutosParameters produtoParams)
        {
            var produtos = GetAllAsync().OrderBy(p => p.ProdutoId).AsQueryable();
            var produtosOrdenados = PagedList<Produto>.ToPagedList(produtos, produtoParams.PageNumber, produtoParams.PageSize);
            return produtosOrdenados;
        }

        public PagedList<Produto> GetProdutosFiltroPreco(ProdutosFiltroPreco produtosFiltroparams)
        {
            var produtos = GetAllAsync().AsQueryable();

            if(produtosFiltroparams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroparams.PrecoCriterio))
            {
                if(produtosFiltroparams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(p => p.Preco > produtosFiltroparams.Preco.Value).OrderBy(p => p.Preco);
                else if (produtosFiltroparams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(p => p.Preco < produtosFiltroparams.Preco.Value).OrderBy(p => p.Preco);
                else if (produtosFiltroparams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(p => p.Preco == produtosFiltroparams.Preco.Value).OrderBy(p => p.Preco);
            }
            var produtosFiltrado = PagedList<Produto>.ToPagedList(produtos, produtosFiltroparams.PageNumber, produtosFiltroparams.PageSize);
            return produtosFiltrado;
        }

        public IEnumerable<Produto> GetProdutosPorCategoria(int id)
        {
            return GetAllAsync().Where(c => c.CategoriaId == id);
        }
    }
}
