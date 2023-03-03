using System.ComponentModel.DataAnnotations;

namespace FilmesAPINET6.Data.Dtos.Cinemas
{
    public class UpdateCinemaDto
    {
        [Required(ErrorMessage = "Campo nome é obrigatório")]
        public string Nome { get; set; }
    }
}
