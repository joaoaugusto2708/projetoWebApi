using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoWebApi.Context;
using projetoWebApi.Models;

namespace projetoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public CategoriasController(AppDbContext context)
        {
            _appDbContext = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            var categoria = _appDbContext.Categorias.AsNoTracking().ToList();
            if (categoria is null)
                return NotFound("Categorias não encontrados");
            return categoria;
        }
        [HttpGet("{id:int}", Name = "ObterCategoria")]//Rota nomeado
        public ActionResult<Categoria> GetId(int id)
        {
            var categoria = _appDbContext.Categorias.FirstOrDefault(c => c.CategoriaId == id);
            if (categoria == null)
                return NotFound("Não foi encontrado a Categoria");
            return categoria;
        }
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos(int id)
        {
            return _appDbContext.Categorias.Include(p => p.Produtos).Where(c=> c.CategoriaId <= 5).ToList();
        }
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            if (categoria == null)
                return BadRequest("Dados vazios");

            _appDbContext.Categorias.Add(categoria);
            _appDbContext.SaveChanges();
            return new CreatedAtRouteResult("ObterCategoria", //Utilização da Rota nomeado
                new { id = categoria.CategoriaId }, categoria);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            if (id != categoria.CategoriaId)
                return BadRequest("Id não confere");
            _appDbContext.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appDbContext.SaveChanges();
            return Ok(categoria);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var categoria = _appDbContext.Categorias.FirstOrDefault(p => p.CategoriaId == id);
            if (categoria is null)
                return NotFound("Produto não encontrado");

            _appDbContext.Categorias.Remove(categoria);
            _appDbContext.SaveChanges();

            return Ok(categoria);
        }
    }
}
