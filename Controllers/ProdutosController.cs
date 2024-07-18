using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using projetoWebApi.DTOs;
using projetoWebApi.Models;
using projetoWebApi.Repositories.Interfaces;

namespace projetoWebApi.Controllers;

[Route("[controller]")]
[ApiController]
public class ProdutosController : ControllerBase
{
    private readonly IUnitOfWork _uof;
    private readonly IMapper _mapper;
    public ProdutosController(IUnitOfWork uof, IMapper mapper)
    {
        _uof = uof;
        _mapper = mapper;
    }

    [HttpGet("produtos/{id}")]
    public ActionResult<IEnumerable<ProdutoDTO>> GetProdutosCategoria(int id)
    {
        var produtos = _uof.ProdutoRepository.GetProdutosPorCategoria(id);
        if (produtos is null)
            return NotFound();
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);

        return Ok(produtosDto);
    }

    [HttpGet]
    public ActionResult<IEnumerable<ProdutoDTO>> Get()
    {
        var produtos = _uof.ProdutoRepository.GetAll();
        if (produtos is null)
        {
            return NotFound();
        }
        var produtosDto = _mapper.Map<IEnumerable<ProdutoDTO>>(produtos);
        return Ok(produtosDto);
    }

    [HttpGet("{id}", Name = "ObterProduto")]
    public ActionResult<ProdutoDTO> Get(int id)
    {
        var produto = _uof.ProdutoRepository.Get(p => p.ProdutoId == id);
        if (produto is null)
        {
            return NotFound("Produto não encontrado...");
        }
        var produtoDto = _mapper.Map<ProdutoDTO>(produto);
        return Ok(produtoDto);
    }

    [HttpPost]
    public ActionResult<ProdutoDTO> Post(ProdutoDTO produtoDto)
    {
        if (produtoDto is null)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);
        var novoProduto  = _uof.ProdutoRepository.Create(produto);
        _uof.Commit();
        var novoProdutoDto = _mapper.Map<ProdutoDTO>(novoProduto);
        return new CreatedAtRouteResult("ObterProduto",
            new { id = novoProduto.ProdutoId }, novoProdutoDto);
    }
    [HttpPatch("{id}/UpdatePartial")]
    public ActionResult<ProdutoDtoUpdateResponse> Patch(int id, JsonPatchDocument<ProdutoDTOUpdateRequest> patchProdutoDto)
    {
        if (patchProdutoDto is null || id <= 0)
            return BadRequest();
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);
        if (produto is null)
            return NotFound();
        var produtoUpdateRequest = _mapper.Map<ProdutoDTOUpdateRequest>(produto);
        patchProdutoDto.ApplyTo(produtoUpdateRequest, ModelState);
        if (!ModelState.IsValid || TryValidateModel(produtoUpdateRequest))
            return BadRequest(ModelState);

        _mapper.Map(produtoUpdateRequest, produto);
        _uof.ProdutoRepository.Update(produto);
        _uof.Commit();
        return Ok(_mapper.Map<ProdutoDtoUpdateResponse>(produto));
    
    }

    [HttpPut("{id:int}")]
    public ActionResult<ProdutoDTO> Put(int id, ProdutoDTO produtoDto)
    {
        if (id != produtoDto.ProdutoId)
            return BadRequest();

        var produto = _mapper.Map<Produto>(produtoDto);
        var produtoAtualizado = _uof.ProdutoRepository.Update(produto);
        _uof.Commit();
        var novoProdutoDto = _mapper.Map<ProdutoDTO>(produtoAtualizado);
        return Ok(novoProdutoDto);
        
    }

    [HttpDelete("{id:int}")]
    public ActionResult<ProdutoDTO> Delete(int id)
    {
        var produto = _uof.ProdutoRepository.Get(c => c.ProdutoId == id);
        if (produto is null)
            return NotFound("Produto não encontrado");
        var produtoDeletado = _uof.ProdutoRepository.Delete(produto);
        _uof.Commit();
        var produtoDeletadoDto = _mapper.Map<ProdutoDTO>(produtoDeletado);
        return Ok(produtoDeletadoDto);
    }
}