using System.ComponentModel.DataAnnotations;

namespace FilmesAPINET6.Models
{
    public class Endereco
    {
        [Key]
        [Required]
        public int ID { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public virtual Cinema Cinema { get; set; }

    }
}
