using AutoMapper;
using FilmesAPINET6.Data;
using FilmesAPINET6.Data.Dtos.Sessao;
using FilmesAPINET6.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPINET6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SessaoController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public SessaoController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Adiciona uma sessão ao banco de dados
        /// </summary>
        /// <param name="sessaoDto">Objeto com os campos necessários para a criação</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AddSessao(CreateSessaoDto sessaoDto)
        {
            Sessao sessao = _mapper.Map<Sessao>(sessaoDto);
            _context.Sessoes.Add(sessao);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetSessaoById), new { filmeID = sessao.FilmeID, cinemaID = sessao.CinemaID }, sessao);
        }

        /// <summary>
        /// Exibe todos as sessões do banco de dados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a exibição seja feita com sucesso</response>
        [HttpGet]
        public IEnumerable<ReadSessaoDto> GetSessoes()
        {
            return _mapper.Map<List<ReadSessaoDto>>(_context.Sessoes.ToList());
        }

        /// <summary>
        /// Exibe a sessão correspondente aos id's dos filmes e dos cinemas
        /// </summary>
        /// <param name="filmeID">ID único de um filme</param>
        /// <param name="cinemaID">ID único de um cinema</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a exibição seja feita com sucesso</response>
        [HttpGet("{filmeID}/{cinemaID}")]
        public IActionResult GetSessaoById(int filmeID, int cinemaID)
        {
            var sessao = _context.Sessoes.FirstOrDefault(s => s.FilmeID == filmeID && s.CinemaID == cinemaID);
            if (sessao == null) return NotFound();
            var sessaoDto = _mapper.Map<ReadSessaoDto>(sessao);
            return Ok(sessaoDto);
        }

        /// <summary>
        /// Deleta uma sessão do banco de dados
        /// </summary>
        /// <param name="filmeID">ID único de um filme</param>
        /// <param name="cinemaID">ID único de um cinema</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a deleção seja feita com sucesso</response>
        [HttpDelete("{filmeId}/{cinemaID}")]
        public IActionResult DeleteSessao(int filmeID, int cinemaID)
        {
            var sessao = _context.Sessoes.FirstOrDefault(s => s.FilmeID == filmeID && s.CinemaID == cinemaID);
            if (sessao == null) return NotFound();
            _context.Sessoes.Remove(sessao);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
