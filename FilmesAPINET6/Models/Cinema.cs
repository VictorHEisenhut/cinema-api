using System.ComponentModel.DataAnnotations;

namespace FilmesAPINET6.Models
{
    public class Cinema
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required(ErrorMessage ="Campo nome é obrigatório")]
        public string Nome { get; set; }
        public int EnderecoId { get; set; }
        public virtual Endereco Endereco { get; set; }
        public virtual ICollection<Sessao> Sessoes { get; set; }
    }
}
