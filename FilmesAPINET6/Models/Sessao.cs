using System.ComponentModel.DataAnnotations;

namespace FilmesAPINET6.Models
{
    public class Sessao
    {
        public int? FilmeID { get; set; }
        public virtual Filme Filme { get; set; }
        public int? CinemaID { get; set; }
        public virtual Cinema Cinema { get; set; }
    }
}
