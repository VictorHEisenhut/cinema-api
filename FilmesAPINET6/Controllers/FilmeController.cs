using AutoMapper;
using FilmesAPINET6.Data;
using FilmesAPINET6.Data.Dtos.Filmes;
using FilmesAPINET6.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPINET6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FilmeController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public FilmeController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona um filme ao banco de dados
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para a criação</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AddFilme([FromBody] CreateFilmeDto filmeDto)
        {
            Filme filme = _mapper.Map<Filme>(filmeDto);
            _context.Filmes.Add(filme);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetFilmeById), new {id = filme.ID}, filme);
        }

        /// <summary>
        /// Exibe todos o filmes do banco de dados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a exibição seja feita com sucesso</response>
        [HttpGet]
        public IEnumerable<ReadFilmeDto> GetFilmes([FromQuery]int skip = 0, [FromQuery] int take = 50, [FromQuery] string? nomeCinema = null)
        {
            if (nomeCinema == null)
            {
                return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).ToList());
            }
            
            return _mapper.Map<List<ReadFilmeDto>>(_context.Filmes.Skip(skip).Take(take).Where(filme => filme.Sessoes
            .Any(sessao => sessao.Cinema.Nome == nomeCinema)).ToList());

        }

        /// <summary>
        /// Exibe o filme correspondente ao seu ID
        /// </summary>
        /// <param name="id">ID único de um filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a exibição seja feita com sucesso</response>
        [HttpGet("{id}")]
        public IActionResult GetFilmeById(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);
            if (filme == null)
                return NotFound();
            var filmeDto = _mapper.Map<ReadFilmeDto>(filme);
            return Ok(filmeDto);
        }

        /// <summary>
        /// Atualiza todos os dados de um filme
        /// </summary>
        /// <param name="filmeDto">Objeto com os campos necessários para a atuaização</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        [HttpPut("{id}")]
        public IActionResult UpdateFilme(int id, [FromBody] UpdateFilmeDto filmeDto)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);
            if(filme == null) return NotFound();
            _mapper.Map(filmeDto, filme);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de um filme parcialmente
        /// </summary>
        /// <param name="patch">Parâmetro utilizado para a atualiação dos campos necessários</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        [HttpPatch("{id}")]
        public IActionResult UpdateFilmeParcial(int id, JsonPatchDocument<UpdateFilmeDto> patch)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);
            if (filme == null) return NotFound();

            var filmeToUpdate = _mapper.Map<UpdateFilmeDto>(filme);

            patch.ApplyTo(filmeToUpdate, ModelState);

            if (!TryValidateModel(filmeToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(filmeToUpdate, filme);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um filme do banco de dados
        /// </summary>
        /// <param name="id">ID único presente em todos os filmes</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a deleção seja feita com sucesso</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteFilme(int id)
        {
            var filme = _context.Filmes.FirstOrDefault(filme => filme.ID == id);
            if (filme == null) return NotFound();
            _context.Remove(filme);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
