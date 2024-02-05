using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pecasAPI.Data;


namespace pecasAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PecasController : ControllerBase
    {
        private readonly DataContext _context;
        public PecasController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pecas>>> GetPecas()
        {
            return Ok(await _context.Pecas.ToListAsync());
        }

        [HttpGet("(id)")]
        public async Task<ActionResult<Pecas>> GetPeca(int id)
        {
            var dbPeca = await _context.Pecas.FindAsync(id);

            if (dbPeca == null)
            {
                return NotFound();
            }

            return dbPeca;
        }

        [HttpPost]
        public async Task<ActionResult<List<Pecas>>> CriarPecas(Pecas peca)
        {
            _context.Pecas.Add(peca);
            await _context.SaveChangesAsync();

            return Ok(await _context.Pecas.ToListAsync());        
        }

        [HttpPut]
        public async Task<ActionResult<List<Pecas>>> AtualizarPeca(Pecas peca)
        {
            var dbPeca = await _context.Pecas.FindAsync(peca.Id);

            if(dbPeca == null)
            {
                return BadRequest("Peca Não encontrada!");
            }

            dbPeca.Descricao = peca.Descricao;
            dbPeca.Preco = peca.Preco;
               
            await _context.SaveChangesAsync();

            return Ok(await _context.Pecas.ToListAsync());

        }

        [HttpDelete("(id)")]
        public async Task<ActionResult<List<Pecas>>> DeletarPeca(int id)
        {
            var dbPeca = await _context.Pecas.FindAsync(id);

            if (dbPeca == null)
            {
                return BadRequest("Peca Não encontrada!");
            }

            _context.Pecas.Remove(dbPeca);
            await _context.SaveChangesAsync();

            return Ok(await _context.Pecas.ToListAsync());
        }
    }
}
