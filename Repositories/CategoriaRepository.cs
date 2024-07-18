using projetoWebApi.Context;
using projetoWebApi.Models;
using projetoWebApi.Repositories.Interfaces;

namespace projetoWebApi.Repositories
{
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext context) : base(context)
        {   
        }
    }
}
