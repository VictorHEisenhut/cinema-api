using System.ComponentModel.DataAnnotations;

namespace FilmesAPINET6.Data.Dtos.Cinemas
{
    public class CreateCinemaDto
    {
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
    }
}
