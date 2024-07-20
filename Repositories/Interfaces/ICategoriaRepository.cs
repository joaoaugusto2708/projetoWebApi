using projetoWebApi.Models;
using projetoWebApi.Pagination;

namespace projetoWebApi.Repositories.Interfaces
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        PagedList<Categoria> GetCategorias(CategoriasParameters categoriasParams);
        PagedList<Categoria> GetCategoriasFiltroNome(CategoriasFiltroNome categoriasParams);
    }
}
