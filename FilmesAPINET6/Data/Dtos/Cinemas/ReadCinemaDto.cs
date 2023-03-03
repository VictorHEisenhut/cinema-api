using FilmesAPINET6.Data.Dtos.Enderecos;
using FilmesAPINET6.Data.Dtos.Sessao;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPINET6.Data.Dtos.Cinemas
{
    public class ReadCinemaDto
    {
        public int ID { get; set; }
        public string Nome { get; set; }
        public ReadEnderecoDto Endereco { get; set; }
        public ICollection<ReadSessaoDto> Sessoes { get; set; }
    }
}
