using gs.Data;
using gs.dtos;
using gs.Models;
using Microsoft.AspNetCore.Mvc;

namespace gs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AbrigoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AbrigoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AbrigoDto>> GetAll()
        {
            var abrigos = _context.Abrigos
                .Select(a => new AbrigoDto
                {
                    Id = a.Id,
                    Nome = a.Nome,
                    Endereco = a.Endereco,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    CapacidadeMaxima = a.CapacidadeMaxima,
                    CapacidadeAtual = a.CapacidadeAtual,
                    Ativo = a.Ativo
                })
                .ToList();
            return Ok(abrigos);
        }

        [HttpGet("{id}")]
        public ActionResult<AbrigoDto> Get(long id)
        {
            var abrigo = _context.Abrigos.Find(id);
            if (abrigo == null) return NotFound();

            return Ok(new AbrigoDto
            {
                Id = abrigo.Id,
                Nome = abrigo.Nome,
                Endereco = abrigo.Endereco,
                Latitude = abrigo.Latitude,
                Longitude = abrigo.Longitude,
                CapacidadeMaxima = abrigo.CapacidadeMaxima,
                CapacidadeAtual = abrigo.CapacidadeAtual,
                Ativo = abrigo.Ativo
            });
        }

        [HttpPost]
        public ActionResult<AbrigoDto> Create([FromBody] AbrigoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Validação de capacidade
            if (dto.CapacidadeAtual > dto.CapacidadeMaxima)
            {
                return BadRequest(new
                {
                    mensagem = "A capacidade atual não pode ser maior que a capacidade máxima."
                });
            }

            // Validação de latitude e longitude duplicados
            if (_context.Abrigos.Count(a => a.Latitude == dto.Latitude && a.Longitude == dto.Longitude) > 0)
            {
                return BadRequest(new
                {
                    mensagem = "Já existe um abrigo cadastrado com essa latitude e longitude."
                });
            }
            if (_context.Alertas.Count(a => a.Latitude == dto.Latitude && a.Longitude == dto.Longitude) > 0)
            {
                return BadRequest(new
                {
                    mensagem = "Já existe um alerta cadastrado com essa latitude e longitude."
                });
            }

            // Verifica se já existe abrigo com o mesmo ID
            if (dto.Id != 0 && _context.Abrigos.Count(a => a.Id == dto.Id) > 0)
                return BadRequest("Já existe um abrigo com este ID.");

            var abrigo = new Abrigo
            {
                Nome = dto.Nome,
                Endereco = dto.Endereco,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                CapacidadeMaxima = dto.CapacidadeMaxima,
                CapacidadeAtual = dto.CapacidadeAtual,
                Ativo = dto.Ativo
            };
            _context.Abrigos.Add(abrigo);
            _context.SaveChanges();

            dto.Id = abrigo.Id;
            return CreatedAtAction(nameof(Get), new { id = abrigo.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] AbrigoDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var abrigo = _context.Abrigos.Find(id);
            if (abrigo == null) return NotFound();

            // Validação de capacidade
            if (dto.CapacidadeAtual > dto.CapacidadeMaxima)
            {
                return BadRequest(new
                {
                    mensagem = "A capacidade atual não pode ser maior que a capacidade máxima."
                });
            }

            // Validação de latitude e longitude duplicados (exceto para o próprio registro)
            if (_context.Abrigos.Count(a => a.Latitude == dto.Latitude && a.Longitude == dto.Longitude && a.Id != id) > 0)
            {
                return BadRequest(new
                {
                    mensagem = "Já existe um abrigo cadastrado com essa latitude e longitude."
                });
            }
            if (_context.Alertas.Count(a => a.Latitude == dto.Latitude && a.Longitude == dto.Longitude) > 0)
            {
                return BadRequest(new
                {
                    mensagem = "Já existe um alerta cadastrado com essa latitude e longitude."
                });
            }

            // Se tentar atualizar para um ID já existente (diferente do atual)
            if (dto.Id != 0 && dto.Id != id && _context.Abrigos.Count(a => a.Id == dto.Id) > 0)
                return BadRequest("Já existe um abrigo com este ID.");

            abrigo.Nome = dto.Nome;
            abrigo.Endereco = dto.Endereco;
            abrigo.Latitude = dto.Latitude;
            abrigo.Longitude = dto.Longitude;
            abrigo.CapacidadeMaxima = dto.CapacidadeMaxima;
            abrigo.CapacidadeAtual = dto.CapacidadeAtual;
            abrigo.Ativo = dto.Ativo;

            _context.SaveChanges();

            var abrigoAtualizado = new AbrigoDto
            {
                Id = abrigo.Id,
                Nome = abrigo.Nome,
                Endereco = abrigo.Endereco,
                Latitude = abrigo.Latitude,
                Longitude = abrigo.Longitude,
                CapacidadeMaxima = abrigo.CapacidadeMaxima,
                CapacidadeAtual = abrigo.CapacidadeAtual,
                Ativo = abrigo.Ativo
            };

            return Ok(abrigoAtualizado);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var abrigo = _context.Abrigos.Find(id);
            if (abrigo == null) return NotFound();

            _context.Abrigos.Remove(abrigo);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
