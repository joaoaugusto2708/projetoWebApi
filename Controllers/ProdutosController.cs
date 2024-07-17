using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public async Task<ActionResult<IEnumerable<Produto>>> Get()
        {
            var produtos =  await _appDbContext.Produtos.Take(10).ToListAsync();
            if (produtos is null)
                return NotFound("Produtos não encontrados");
            return produtos;
        }
        [HttpGet("{id:int}", Name = "ObterProduto")]//Rota nomeado
        public async Task<ActionResult<Produto>> GetId(int id)
        {
            var produto = await _appDbContext.Produtos.FirstOrDefaultAsync(p => p.ProdutoId == id);
            if (produto == null)
                return NotFound("Não foi encontrado o Produto");
            return produto;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Produto produto)
        {
            if (produto == null)
                return BadRequest("Dados vazios");

            await _appDbContext.Produtos.AddAsync(produto);
            await _appDbContext.SaveChangesAsync();
            return new CreatedAtRouteResult("ObterProduto", //Utilização da Rota nomeado
                new { id = produto.ProdutoId }, produto);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, Produto produto)
        {
            if(id != produto.ProdutoId)
                return BadRequest("Id não confere");
            _appDbContext.Entry(produto).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await _appDbContext.SaveChangesAsync();
            return Ok(produto);
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var produto = _appDbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
            if (produto is null)
                return NotFound("Produto não encontrado");

            _appDbContext.Produtos.Remove(produto);
            await _appDbContext.SaveChangesAsync();

            return Ok(produto);
        }
    }
}
