using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace FilmesAPINET6.Models
{
    public class Filme
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage ="O titulo do filme é obrigatório")]
        [MaxLength(70, ErrorMessage ="Máximo de caracteres excedidos")]
        public string Titulo { get; set; }
        [Required(ErrorMessage = "O gênero do filme é obrigatório")]
        [MaxLength(70, ErrorMessage = "Máximo de caracteres excedidos")]
        public string Genero { get; set; }
        [Required]
        [Range(70, 500, ErrorMessage ="Duração deve ser entre 70 e 500 minutos")]
        public int Duracao { get; set; }
        public virtual ICollection<Sessao> Sessoes { get; set; }
    }
}
