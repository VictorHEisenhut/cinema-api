using AutoMapper;
using FilmesAPINET6.Data.Dtos.Filmes;
using FilmesAPINET6.Models;

namespace FilmesAPINET6.Profiles
{
    public class FilmeProfile : Profile
    {
        public FilmeProfile()
        {
            CreateMap<CreateFilmeDto, Filme>();
            CreateMap<UpdateFilmeDto, Filme>();
            CreateMap<Filme, UpdateFilmeDto>();
            CreateMap<Filme, ReadFilmeDto>()
                .ForMember(filmeDto => filmeDto.Sessoes, opt => opt.MapFrom(
                    filme => filme.Sessoes));

        }
    }
}
