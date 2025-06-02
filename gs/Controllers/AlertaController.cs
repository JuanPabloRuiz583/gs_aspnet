using gs.Data;
using gs.dtos;
using gs.Models;
using Microsoft.AspNetCore.Mvc;

namespace gs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AlertaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlertaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<AlertaDto>> GetAll()
        {
            var alertas = _context.Alertas
                .Select(a => new AlertaDto
                {
                    Id = a.Id,
                    Descricao = a.Descricao,
                    DataHora = a.DataHora,
                    Latitude = a.Latitude,
                    Longitude = a.Longitude,
                    TipoEvento = a.TipoEvento,
                    UsuarioId = a.UsuarioId
                })
                .ToList();
            return Ok(alertas);
        }

        [HttpGet("{id}")]
        public ActionResult<AlertaDto> Get(long id)
        {
            var alerta = _context.Alertas.Find(id);
            if (alerta == null) return NotFound();

            return Ok(new AlertaDto
            {
                Id = alerta.Id,
                Descricao = alerta.Descricao,
                DataHora = alerta.DataHora,
                Latitude = alerta.Latitude,
                Longitude = alerta.Longitude,
                TipoEvento = alerta.TipoEvento,
                UsuarioId = alerta.UsuarioId
            });
        }

        [HttpPost]
        public ActionResult<AlertaDto> Create([FromBody] AlertaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (_context.Usuarios.Count(u => u.Id == dto.UsuarioId) == 0)
                return BadRequest("Usuário informado não existe.");

            // Validação de latitude e longitude duplicados
            if (_context.Alertas.Count(a => a.Latitude == dto.Latitude && a.Longitude == dto.Longitude) > 0)
            {
                return BadRequest(new
                {
                    mensagem = "Já existe um alerta cadastrado com essa latitude e longitude."
                });
            }
            if (_context.Abrigos.Count(a => a.Latitude == dto.Latitude && a.Longitude == dto.Longitude) > 0)
            {
                return BadRequest(new
                {
                    mensagem = "Já existe um abrigo cadastrado com essa latitude e longitude."
                });
            }

            var alerta = new Alerta
            {
                Descricao = dto.Descricao,
                DataHora = dto.DataHora,
                Latitude = dto.Latitude,
                Longitude = dto.Longitude,
                TipoEvento = dto.TipoEvento,
                UsuarioId = dto.UsuarioId
            };

            try
            {
                _context.Alertas.Add(alerta);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Não foi possível criar o alerta. Verifique os dados informados. Detalhes: " + ex.Message);
            }

            dto.Id = alerta.Id;
            return CreatedAtAction(nameof(Get), new { id = alerta.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] AlertaDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var alerta = _context.Alertas.Find(id);
            if (alerta == null) return NotFound();

            if (_context.Usuarios.Count(u => u.Id == dto.UsuarioId) == 0)
                return BadRequest("Usuário informado não existe.");

            // Validação de latitude e longitude duplicados (exceto para o próprio alerta)
            if (_context.Alertas.Count(a => a.Latitude == dto.Latitude && a.Longitude == dto.Longitude && a.Id != id) > 0)
            {
                return BadRequest(new
                {
                    mensagem = "Já existe um alerta cadastrado com essa latitude e longitude."
                });
            }
            if (_context.Abrigos.Count(a => a.Latitude == dto.Latitude && a.Longitude == dto.Longitude) > 0)
            {
                return BadRequest(new
                {
                    mensagem = "Já existe um abrigo cadastrado com essa latitude e longitude."
                });
            }

            alerta.Descricao = dto.Descricao;
            alerta.DataHora = dto.DataHora;
            alerta.Latitude = dto.Latitude;
            alerta.Longitude = dto.Longitude;
            alerta.TipoEvento = dto.TipoEvento;
            alerta.UsuarioId = dto.UsuarioId;

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity("Não foi possível atualizar o alerta. Verifique os dados informados. Detalhes: " + ex.Message);
            }

            var alertaAtualizado = new AlertaDto
            {
                Id = alerta.Id,
                Descricao = alerta.Descricao,
                DataHora = alerta.DataHora,
                Latitude = alerta.Latitude,
                Longitude = alerta.Longitude,
                TipoEvento = alerta.TipoEvento,
                UsuarioId = alerta.UsuarioId
            };

            return Ok(alertaAtualizado);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var alerta = _context.Alertas.Find(id);
            if (alerta == null) return NotFound();

            _context.Alertas.Remove(alerta);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
