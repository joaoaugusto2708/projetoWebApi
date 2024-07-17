using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using projetoWebApi.Context;
using projetoWebApi.Filters;
using projetoWebApi.Models;

namespace projetoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ILogger _logger;
        public CategoriasController(AppDbContext context, ILogger logger)
        {
            _appDbContext = context;
            _logger = logger;
        }
        [HttpGet]
        [ServiceFilter(typeof(ApiLoggingFilter))]
        public ActionResult<IEnumerable<Categoria>> Get()
        {
            try
            {
                _logger.LogInformation($"===== Metodo Get Categorias Produto {Thread.CurrentThread.ToString}=======");
                var categoria = _appDbContext.Categorias.AsNoTracking().ToList();
                if (categoria is null)
                    return NotFound("Categorias não encontrados");
                return categoria;
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema a realizar sua operação: " + ex.Message);
            }
            
        }
        [HttpGet("{id:int}", Name = "ObterCategoria")]//Rota nomeado
        public ActionResult<Categoria> GetId(int id)
        {
            try
            {
                var categoria = _appDbContext.Categorias.FirstOrDefault(c => c.CategoriaId == id);
                if (categoria == null)
                    return NotFound("Não foi encontrado a Categoria");
                return categoria;
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema a realizar sua operação: " + ex.Message);
            }
            
        }
        [HttpGet("produtos")]
        public ActionResult<IEnumerable<Categoria>> GetCategoriaProdutos(int id)
        {
            try
            {
                return _appDbContext.Categorias.Include(p => p.Produtos).Where(c => c.CategoriaId <= 5).ToList();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema a realizar sua operação: " + ex.Message);
            }
            
        }
        [HttpPost]
        public ActionResult Post([FromBody] Categoria categoria)
        {
            try
            {
                if (categoria == null)
                    return BadRequest("Dados vazios");

                _appDbContext.Categorias.Add(categoria);
                _appDbContext.SaveChanges();
                return new CreatedAtRouteResult("ObterCategoria", //Utilização da Rota nomeado
                    new { id = categoria.CategoriaId }, categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema a realizar sua operação: " + ex.Message);
            }
            
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Categoria categoria)
        {
            try
            {
                if (id != categoria.CategoriaId)
                    return BadRequest("Id não confere");
                _appDbContext.Entry(categoria).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                _appDbContext.SaveChanges();
                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema a realizar sua operação: " + ex.Message);
            }
            
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var categoria = _appDbContext.Categorias.FirstOrDefault(p => p.CategoriaId == id);
                if (categoria is null)
                    return NotFound("Produto não encontrado");

                _appDbContext.Categorias.Remove(categoria);
                _appDbContext.SaveChanges();

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Ocorreu um problema a realizar sua operação: " + ex.Message);
            }
            
        }
    }
}
