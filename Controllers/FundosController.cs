using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtivaAPI.Models;
using AtivaAPI.Data;

namespace AtivaAPI.Controllers
{
    [Route("api/Fundos")]
    [ApiController]
    public class FundosController : ControllerBase
    {
        private readonly AtivaAPIContext _context;

        public FundosController(AtivaAPIContext context)
        {
            _context = context;

            //como o banco de dados é in memory, criar 5 fundos ao banco de dados para funcionar como lista inicial
            if (!_context.Fundos.Any())
            {
                _context.Fundos.Add(new Fundo { Id = Guid.NewGuid(), Nome = "ALASKA BLACK FIC FIA II BDR - NÍVEL I", CNPJ = "26648868000196", InvestimentoMinimo = 1000M });
                _context.Fundos.Add(new Fundo { Id = Guid.NewGuid(), Nome = "BRADESCO ASSET FIC FIA SMALL CAPS", CNPJ = "34054867000141", InvestimentoMinimo = 5000M });
                _context.Fundos.Add(new Fundo { Id = Guid.NewGuid(), Nome = "GTI DINOMA BRASIL FIA", CNPJ = "09143435000160", InvestimentoMinimo = 1000M });
                _context.Fundos.Add(new Fundo { Id = Guid.NewGuid(), Nome = "OCCAM FIC FIA", CNPJ = "12332239000148", InvestimentoMinimo = 10000M });
                _context.Fundos.Add(new Fundo { Id = Guid.NewGuid(), Nome = "VENTURE VALUE FIA", CNPJ = "11447072000106", InvestimentoMinimo = 5000M });
                
                _context.SaveChanges();
            }
        }

        //Json Format
        //{
        //  "nome": "Nome do Fundo" (string)
        //  "cnpj": "CNPJ do Fundo (string)
        //  "investimentoMinimo": "Investimento Minimo" (decimal)
        //}

        // GET: api/Fundos (Lista todos os fundos)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Fundo>>> GetFundos()
        {
            return await _context.Fundos.ToListAsync();
        }

        // GET: api/Fundos/5 (mostra o fundo específico por Id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Fundo>> GetFundo(Guid id)
        {
            var fundo = await _context.Fundos.FindAsync(id);

            if (fundo == null)
            {
                return NotFound();
            }

            return fundo;
        }

        // PUT: api/Fundos/5 (atualiza informações do fundo específico por Id)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFundo(Guid id, Fundo fundo)
        {
            if (id != fundo.Id)
            {
                return BadRequest();
            }

            _context.Entry(fundo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FundoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Fundos (Adiciona um fundo novo na lista de fundos)
        [HttpPost]
        public async Task<ActionResult<Fundo>> PostFundo(Fundo fundo)
        {
            _context.Fundos.Add(fundo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFundo), new { id = fundo.Id }, fundo);
        }

        // DELETE: api/Fundos/5 (deleta o fundo específico por Id)
        [HttpDelete("{id}")]
        public async Task<ActionResult<Fundo>> DeleteFundo(Guid id)
        {
            var fundo = await _context.Fundos.FindAsync(id);
            if (fundo == null)
            {
                return NotFound();
            }

            _context.Fundos.Remove(fundo);
            await _context.SaveChangesAsync();

            return fundo;
        }

        private bool FundoExists(Guid id)
        {
            return _context.Fundos.Any(e => e.Id == id);
        }
    }
}
