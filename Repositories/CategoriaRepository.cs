using projetoWebApi.Context;
using projetoWebApi.Models;

namespace projetoWebApi.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly AppDbContext _context;

        public CategoriaRepository(AppDbContext context)
        {
            _context = context;
        }

        public Categoria Create(Categoria categoria)
        {
            if(categoria is null)
                throw new ArgumentNullException(nameof(categoria));
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public Categoria Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));
            _context.Categorias.Remove(categoria);
            _context.SaveChanges();
            return categoria;
        }

        public Categoria GetCategoria(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria is null)
                throw new InvalidOperationException("Nenhuma Categoria Encontrada");
            return categoria;
        }

        public IEnumerable<Categoria> GetCategorias()
        {
           var categorias = _context.Categorias.ToList();
            if(categorias is null)
                throw new InvalidOperationException("Categorias é null");
            return categorias;
        }

        public Categoria Update(Categoria categoria)
        {
            if (categoria is null)
                throw new ArgumentNullException(nameof(categoria));
            _context.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
            return categoria;
        }
    }
}
