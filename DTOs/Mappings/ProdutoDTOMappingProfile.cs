using AutoMapper;
using projetoWebApi.Models;

namespace projetoWebApi.DTOs.Mappings
{
    public class ProdutoDTOMappingProfile : Profile
    {
        public ProdutoDTOMappingProfile()
        {
            CreateMap<Produto, ProdutoDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Produto, ProdutoDTOUpdateRequest>().ReverseMap();
            CreateMap<Produto, ProdutoDtoUpdateResponse>().ReverseMap();
        }
    }
}
