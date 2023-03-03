using AutoMapper;
using FilmesAPINET6.Data.Dtos.Cinemas;
using FilmesAPINET6.Data.Dtos.Enderecos;
using FilmesAPINET6.Models;

namespace FilmesAPINET6.Profiles
{
    public class EnderecoProfile:Profile
    {
        public EnderecoProfile()
        {
            CreateMap<Endereco, ReadEnderecoDto>();
            CreateMap<ReadEnderecoDto, Endereco>();
            CreateMap<Endereco, CreateEnderecoDto>();
            CreateMap<CreateEnderecoDto, Endereco>();
            CreateMap<Endereco, UpdateEnderecoDto>();
            CreateMap<UpdateEnderecoDto, Endereco>();
        }
    }
}
