using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using projetoWebApi.Context;
using projetoWebApi.Models;

namespace projetoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ProdutosController(AppDbContext context)
        {
            _appDbContext = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
            var produtos = _appDbContext.Produtos.Take(10).ToList();
            if (produtos is null)
                return NotFound("Produtos não encontrados");
            return produtos;
        }
        [HttpGet("{id:int}", Name = "ObterProduto")]//Rota nomeado
        public ActionResult<Produto> GetId(int id)
        {
            var produto = _appDbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto == null)
                return NotFound("Não foi encontrado o Produto");
            return produto;
        }
        [HttpPost]
        public ActionResult Post([FromBody] Produto produto)
        {
            if (produto == null)
                return BadRequest("Dados vazios");

            _appDbContext.Produtos.Add(produto);
            _appDbContext.SaveChanges();
            return new CreatedAtRouteResult("ObterProduto", //Utilização da Rota nomeado
                new { id = produto.ProdutoId }, produto);
        }
        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
                return BadRequest("Id não confere");
            _appDbContext.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _appDbContext.SaveChanges();
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            var produto = _appDbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
                return NotFound("Produto não encontrado");

            _appDbContext.Produtos.Remove(produto);
            _appDbContext.SaveChanges();

            return Ok(produto);
        }
    }
}
