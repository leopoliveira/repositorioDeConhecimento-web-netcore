using AutoMapper;
using RepositorioDeConhecimento.Models.Application.DTO;
using RepositorioDeConhecimento.Models.Domain.Entities;

namespace RepositorioDeConhecimento.Infrastructure.Helpers.Automapper
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Autor, AutorDTO>()
                .ForMember(des => des.FotoId, src => src.MapFrom(src => src.Id))
                .ForMember(des => des.Foto, src => src.MapFrom(src => src.Foto))
                .ReverseMap();

            CreateMap<Categoria, CategoriaDTO>()
                .ForMember(des => des.IconeId, src => src.MapFrom(src => src.Id))
                .ForMember(des => des.Icone, src => src.MapFrom(src => src.Icone))
                .ReverseMap();

            CreateMap<Conhecimento, ConhecimentoDTO>()
                .ForMember(des => des.AutorId, src => src.MapFrom(src => src.Id))
                .ForMember(des => des.Autor, src => src.MapFrom(src => src.Autor))
                .ForMember(des => des.CategoriaId, src => src.MapFrom(src => src.Id))
                .ForMember(des => des.Categoria, src => src.MapFrom(src => src.Categoria))
                .ReverseMap();

            CreateMap<Imagem, ImagemDTO>()
                .ForMember(des => des.ConhecimentoId, src => src.MapFrom(src => src.Id))
                .ForMember(des => des.Conhecimento, src => src.MapFrom(src => src.Conhecimento))
                .ReverseMap();

            CreateMap<Tag, TagDTO>()
                .ForMember(des => des.ConhecimentoId, src => src.MapFrom(src => src.Id))
                .ForMember(des => des.Conhecimento, src => src.MapFrom(src => src.Conhecimento))
                .ReverseMap();
        }
    }
}
