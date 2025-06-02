using gs.Data;
using gs.dtos;
using gs.Models;
using Microsoft.AspNetCore.Mvc;

namespace gs.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuarioController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuarioController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<UsuarioDto>> GetAll()
        {
            var usuarios = _context.Usuarios
                .Select(u => new UsuarioDto
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email,
                    Senha = u.Senha
                })
                .ToList();
            return Ok(usuarios);
        }

        [HttpGet("{id}")]
        public ActionResult<UsuarioDto> Get(long id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            return Ok(new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha
            });
        }

        [HttpPost]
        public ActionResult<UsuarioDto> Create([FromBody] UsuarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            // Verifica se já existe usuário com o mesmo e-mail
            if (_context.Usuarios.Count(u => u.Email == dto.Email) > 0)
                return BadRequest("Já existe um usuário com este e-mail.");


            var usuario = new Usuario
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Senha = dto.Senha
            };
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();

            dto.Id = usuario.Id;
            return CreatedAtAction(nameof(Get), new { id = usuario.Id }, dto);
        }

        [HttpPut("{id}")]
        public IActionResult Update(long id, [FromBody] UsuarioDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            // Verifica se o novo e-mail já pertence a outro usuário
            if (_context.Usuarios.Count(u => u.Email == dto.Email && u.Id != id) > 0)
                return BadRequest("Já existe um usuário com este e-mail.");

            usuario.Nome = dto.Nome;
            usuario.Email = dto.Email;
            usuario.Senha = dto.Senha;

            _context.SaveChanges();

            var usuarioAtualizado = new UsuarioDto
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Senha = usuario.Senha
            };

            return Ok(usuarioAtualizado);
        }


        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null) return NotFound();

            _context.Usuarios.Remove(usuario);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
