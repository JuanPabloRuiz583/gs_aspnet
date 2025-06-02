using gs.Data;
using gs.dtos;
using gs.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace gs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RotaSeguraController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RotaSeguraController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<RotaSeguraDto>> GetAll()
        {
            var rotas = _context.RotasSeguras
                .Select(r => new RotaSeguraDto
                {
                    Id = r.Id,
                    DistanciaKm = r.DistanciaKm,
                    Observacao = r.Observacao,
                    AbrigoId = r.AbrigoId,
                    AlertaId = r.AlertaId
                })
                .ToList();
            return Ok(rotas);
        }

        [HttpGet("{id}")]
        public ActionResult<RotaSeguraDto> Get(long id)
        {
            var rota = _context.RotasSeguras.Find(id);
            if (rota == null) return NotFound();

            return Ok(new RotaSeguraDto
            {
                Id = rota.Id,
                DistanciaKm = rota.DistanciaKm,
                Observacao = rota.Observacao,
                AbrigoId = rota.AbrigoId,
                AlertaId = rota.AlertaId
            });
        }

        [HttpPost]
        public ActionResult<RotaSeguraDto> Create([FromBody] RotaSeguraDto dto)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage)
                    .ToList();

                return BadRequest(new
                {
                    mensagem = "Erro de validação nos dados enviados.",
                    detalhes = errors
                });
            }

            // Verifica se já existe rota com o mesmo ID
            if (dto.Id != 0 && _context.RotasSeguras.Count(r => r.Id == dto.Id) > 0)
                return BadRequest(new
                {
                    mensagem = "Já existe uma rota segura com este ID."
                });

            var rota = new RotaSegura
            {
                DistanciaKm = dto.DistanciaKm,
                Observacao = dto.Observacao,
                AbrigoId = dto.AbrigoId,
                AlertaId = dto.AlertaId
            };

            try
            {
                _context.RotasSeguras.Add(rota);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(new
                {
                    mensagem = "Erro ao salvar a rota segura. Verifique se os dados informados são válidos e se os IDs de abrigo e alerta existem.",
                    detalhes = ex.InnerException?.Message ?? ex.Message
                });
            }

            dto.Id = rota.Id;
            return CreatedAtAction(nameof(Get), new { id = rota.Id }, dto);
        }


        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] RotaSeguraDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var rota = _context.RotasSeguras.Find(id);
            if (rota == null) return NotFound();

            if (dto.Id != 0 && dto.Id != id && _context.RotasSeguras.Count(r => r.Id == dto.Id) > 0)
                return BadRequest("Já existe uma rota segura com este ID.");

            rota.DistanciaKm = dto.DistanciaKm;
            rota.Observacao = dto.Observacao;
            rota.AbrigoId = dto.AbrigoId;
            rota.AlertaId = dto.AlertaId;

            _context.SaveChanges();

            // Retorna o objeto atualizado
            var rotaAtualizada = new RotaSeguraDto
            {
                Id = rota.Id,
                DistanciaKm = rota.DistanciaKm,
                Observacao = rota.Observacao,
                AbrigoId = rota.AbrigoId,
                AlertaId = rota.AlertaId
            };

            return Ok(rotaAtualizada);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var rota = _context.RotasSeguras.Find(id);
            if (rota == null) return NotFound();

            _context.RotasSeguras.Remove(rota);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
