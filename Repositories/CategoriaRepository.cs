using projetoWebApi.Context;
using projetoWebApi.Models;
using projetoWebApi.Pagination;
using projetoWebApi.Repositories.Interfaces;

namespace projetoWebApi.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {   
        }

        public PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams)
        {
            var categorias = GetAll().OrderBy(c => c.CategoriaId).AsQueryable();
            var categoriasOrdenados = PagedList<Categoria>.ToPagedList(categorias, categoriasParams.PageNumber, categoriasParams.PageSize);
            return categoriasOrdenados;
        }
    }
}
