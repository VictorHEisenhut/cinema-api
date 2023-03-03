using AutoMapper;
using FilmesAPINET6.Data;
using FilmesAPINET6.Data.Dtos.Enderecos;
using FilmesAPINET6.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration.Json;

namespace FilmesAPINET6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnderecoController : ControllerBase
    {
        private IMapper _mapper;
        private FilmeContext _context;

        public EnderecoController(IMapper mapper, FilmeContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        /// <summary>
        /// Adiciona um endereço ao banco de dados
        /// </summary>
        /// <param name="enderecoDto">Objeto com os campos necessários para a criação</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Caso a inserção seja feita com sucesso</response>
        [HttpPost]
        public IActionResult AddEndereco([FromBody]CreateEnderecoDto enderecoDto)
        {
            Endereco endereco = _mapper.Map<Endereco>(enderecoDto);
            _context.Enderecos.Add(endereco);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEnderecoById), new { ID = endereco.ID }, endereco);
        }

        /// <summary>
        /// Exibe todos o endereços do banco de dados
        /// </summary>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a exibição seja feita com sucesso</response>

        [HttpGet]
        public IEnumerable<ReadEnderecoDto> GetEnderecos()
        {
            return _mapper.Map<List<ReadEnderecoDto>>(_context.Enderecos.ToList());
        }

        /// <summary>
        /// Exibe o endereço correspondente ao seu ID
        /// </summary>
        /// <param name="id">ID único de um endereço</param>
        /// <returns>IActionResult</returns>
        /// <response code="200">Caso a exibição seja feita com sucesso</response>
        [HttpGet("{id}")]
        public IActionResult GetEnderecoById(int id)
        {
            Endereco endereco = _context.Enderecos.FirstOrDefault(e => e.ID == id);
            if (endereco == null) return NotFound();
            ReadEnderecoDto enderecoDto = _mapper.Map<ReadEnderecoDto>(endereco);
            return Ok(enderecoDto);
        }

        /// <summary>
        /// Atualiza todos os dados de um endereço
        /// </summary>
        /// <param name="enderecoDto">Objeto com os campos necessários para a atuaização</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        [HttpPut("{id}")]
        public IActionResult UpdateEndereco(int id, [FromBody] UpdateEnderecoDto enderecoDto)
        {
            var endereco = _context.Enderecos.FirstOrDefault(e => e.ID == id);
            if (endereco == null) return NotFound();
            _mapper.Map(enderecoDto, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Atualiza os dados de um endereço parcialmente
        /// </summary>
        /// <param name="patch">Parâmetro utilizado para a atualiação dos campos necessários</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a atualização seja feita com sucesso</response>
        [HttpPatch("{id}")]
        public IActionResult UpdateEnderecoParcial(int id, JsonPatchDocument<UpdateEnderecoDto> patch)
        {
            var endereco = _context.Enderecos.FirstOrDefault(e => e.ID == id);
            if (endereco == null) return NotFound();
            var enderecoToUpdate = _mapper.Map<UpdateEnderecoDto>(endereco);

            patch.ApplyTo(enderecoToUpdate, ModelState);
            
            if (!TryValidateModel(enderecoToUpdate))
            {
                return ValidationProblem(ModelState);
            }

            _mapper.Map(enderecoToUpdate, endereco);
            _context.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um endereço do banco de dados
        /// </summary>
        /// <param name="id">ID único presente em todos os endereços</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Caso a deleção seja feita com sucesso</response>
        [HttpDelete("{id}")]
        public IActionResult DeleteEndereco(int id)
        {
            var endereco = _context.Enderecos.FirstOrDefault(e => e.ID == id);
            if(endereco == null) return NotFound();
            _context.Enderecos.Remove(endereco);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
