using System.ComponentModel.DataAnnotations;

namespace FilmesAPINET6.Data.Dtos.Filmes
{
    public class UpdateFilmeDto
    {
        [Required(ErrorMessage = "O titulo do filme é obrigatório")]
        [StringLength(70, ErrorMessage = "Máximo de caracteres excedidos")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O gênero do filme é obrigatório")]
        [StringLength(70, ErrorMessage = "Máximo de caracteres excedidos")]
        public string Genero { get; set; }
        [Required]
        [Range(70, 500, ErrorMessage = "Duração deve ser entre 70 e 500 minutos")]
        public int Duracao { get; set; }
    }
}
