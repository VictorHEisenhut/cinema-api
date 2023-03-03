using AutoMapper;
using FilmesAPINET6.Data.Dtos.Sessao;
using FilmesAPINET6.Models;

namespace FilmesAPINET6.Profiles
{
    public class SessaoProfile : Profile
    {
        public SessaoProfile()
        {
            CreateMap<CreateSessaoDto, Sessao>();
            CreateMap<Sessao, ReadSessaoDto>();
        }
    }
}
