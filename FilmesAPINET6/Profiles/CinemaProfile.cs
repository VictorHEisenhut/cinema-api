using AutoMapper;
using FilmesAPINET6.Data.Dtos.Cinemas;
using FilmesAPINET6.Models;

namespace FilmesAPINET6.Profiles
{
    public class CinemaProfile : Profile
    {
        public CinemaProfile()
        {
            CreateMap<Cinema, ReadCinemaDto>()
                .ForMember(cinemaDto => cinemaDto.Endereco, opt => opt.MapFrom(
                    cinema => cinema.Endereco))
                .ForMember(cinemaDto => cinemaDto.Sessoes, opt => opt.MapFrom(
                    cinema => cinema.Sessoes));
            CreateMap<ReadCinemaDto, Cinema>();
            CreateMap<Cinema, CreateCinemaDto>();
            CreateMap<CreateCinemaDto, Cinema>();
            CreateMap<Cinema, UpdateCinemaDto>();
            CreateMap<UpdateCinemaDto, Cinema>();
        }
    }
}
