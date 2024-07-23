using projetoWebApi.Context;
using projetoWebApi.Models;
using projetoWebApi.Pagination;
using projetoWebApi.Repositories.Interfaces;
using X.PagedList;

namespace projetoWebApi.Repositories
{
    public class ProdutoRepository : Repository<Produto>, IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context) : base(context) { }

        //public IEnumerable<Produto> GetProdutosAsync(ProdutosParameters produtoParams)
        //{
        //    return GetAllAsync()
        //        .OrderBy(p => p.Nome)
        //        .Skip((produtoParams.PageNumber - 1) * produtoParams.PageSize)
        //        .Take(produtoParams.PageSize)
        //        .ToList();
        //}

        public async Task<IPagedList<Produto>> GetProdutosAsync(ProdutosParameters produtoParams)
        {
            var produtos = await GetAllAsync();
            var produtosOrdenados = produtos.OrderBy(p => p.ProdutoId).AsQueryable();
            //var resultado = PagedList<Produto>.ToPagedList(produtosOrdenados, produtoParams.PageNumber, produtoParams.PageSize);
            var resultado = await produtosOrdenados.ToPagedListAsync(produtoParams.PageNumber, produtoParams.PageSize);
            return resultado;
        }

        public async Task<IPagedList<Produto>> GetProdutosFiltroPrecoAsync(ProdutosFiltroPreco produtosFiltroparams)
        {
            var produtos = await GetAllAsync();

            if(produtosFiltroparams.Preco.HasValue && !string.IsNullOrEmpty(produtosFiltroparams.PrecoCriterio))
            {
                if(produtosFiltroparams.PrecoCriterio.Equals("maior", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(p => p.Preco > produtosFiltroparams.Preco.Value).OrderBy(p => p.Preco);
                else if (produtosFiltroparams.PrecoCriterio.Equals("menor", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(p => p.Preco < produtosFiltroparams.Preco.Value).OrderBy(p => p.Preco);
                else if (produtosFiltroparams.PrecoCriterio.Equals("igual", StringComparison.OrdinalIgnoreCase))
                    produtos = produtos.Where(p => p.Preco == produtosFiltroparams.Preco.Value).OrderBy(p => p.Preco);
            }
            //var produtosFiltrado = PagedList<Produto>.ToPagedList(produtos.AsQueryable(), produtosFiltroparams.PageNumber, produtosFiltroparams.PageSize);
            var produtosFiltrados = await produtos.ToPagedListAsync(produtosFiltroparams.PageNumber, produtosFiltroparams.PageSize);
            return produtosFiltrados;
        }

        public async Task<IEnumerable<Produto>> GetProdutosPorCategoriaAsync(int id)
        {
            var produtos = await GetAllAsync();
            return produtos.Where(c => c.CategoriaId == id);
        }
    }
}
