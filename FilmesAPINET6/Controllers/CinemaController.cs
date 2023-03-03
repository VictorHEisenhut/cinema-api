using AutoMapper;
using FilmesAPINET6.Data;
using FilmesAPINET6.Data.Dtos.Cinemas;
using FilmesAPINET6.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FilmesAPINET6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CinemaController : ControllerBase
    {
        private FilmeContext _context;
        private IMapper _mapper;

        public CinemaController(FilmeContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Exibe todos o cinemas do banco de dados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a exibição seja feita com sucesso</response>
        [HttpGet]
        public IEnumerable<ReadCinemaDto> GetCinemas([FromQuery] int? enderecoId = null)
        {
            if (enderecoId == null)
            {
                return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.ToList());
            }

            return _mapper.Map<List<ReadCinemaDto>>(_context.Cinemas.FromSqlRaw($"SELECT ID, Nome, EnderecoId FROM Cinemas WHERE Cinemas.EnderecoId = {enderecoId}").ToList());

        }

        /// <summary>
        /// Exibe o cinema correspondente ao seu ID
        /// </summary>
        /// <param name="id">ID único de um cinema</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a exibição seja feita com sucesso</response>
        [HttpGet("{id}")]
        public IActionResult GetCinemaById(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(c => c.ID == id);
            if (cinema == null) return NotFound();
            var cinemaDto = _mapper.Map<ReadCinemaDto>(cinema);
            return Ok(cinemaDto);
        }

        /// <summary>
        /// Adiciona um cinema ao banco de dados
        /// </summary>
        /// <param name="cinemaDto">Objeto com os campos necessários para a criação</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AddCinema([FromBody] CreateCinemaDto cinemaDto)
        {
            Cinema cinema = _mapper.Map<Cinema>(cinemaDto);
            _context.Cinemas.Add(cinema);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetCinemaById), new { id = cinema.ID }, cinemaDto);
        }

        /// <summary>
        /// Atualiza todos os dados de um cinema
        /// </summary>
        /// <param name="cinemaDto">Objeto com os campos necessários para a atuaização</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        [HttpPut("{id}")]
        public IActionResult UpdateCinema(int id, [FromBody] UpdateCinemaDto cinemaDto)
        {
            var cinema = _context.Cinemas.FirstOrDefault(c => c.ID == id);
            if(cinema == null) return NotFound();
            _mapper.Map(cinemaDto, cinema);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de um cinema parcialmente
        /// </summary>
        /// <param name="patch">Parâmetro utilizado para a atualiação dos campos necessários</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        [HttpPatch("{id}")]
        public IActionResult UpdateCinemaParcial(int id, JsonPatchDocument<UpdateCinemaDto> patch)
        {
            var cinema = _context.Cinemas.FirstOrDefault(c => c.ID == id);
            if (cinema == null) return NotFound();
            
            var cinemaToUpdate = _mapper.Map<UpdateCinemaDto>(cinema);

            patch.ApplyTo(cinemaToUpdate, ModelState);

            if (!TryValidateModel(cinemaToUpdate))
            {
                return ValidationProblem(ModelState);
            }
            _mapper.Map(cinemaToUpdate, cinema);
            _context.SaveChanges();
            return NoContent();

        }

        /// <summary>
        /// Deleta um cinema do banco de dados
        /// </summary>
        /// <param name="id">ID único presente em todos os cinemas</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a deleção seja feita com sucesso</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteCinema(int id)
        {
            var cinema = _context.Cinemas.FirstOrDefault(c => c.ID == id);
            if(cinema == null) return NotFound();
            _context.Cinemas.Remove(cinema);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
