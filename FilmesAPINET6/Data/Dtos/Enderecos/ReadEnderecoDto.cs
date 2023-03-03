using FilmesAPINET6.Data.Dtos.Cinemas;
using FilmesAPINET6.Models;

namespace FilmesAPINET6.Data.Dtos.Enderecos
{
    public class ReadEnderecoDto
    {
        public int ID { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
    }
}
