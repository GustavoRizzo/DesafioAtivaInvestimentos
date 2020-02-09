using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AtivaAPI.Data;
using AtivaAPI.Models;

namespace AtivaAPI.Controllers
{
    [Route("api/Operacoes")]
    [ApiController]
    public class OperacoesController : ControllerBase
    {
        private readonly AtivaAPIContext _context;

        public OperacoesController(AtivaAPIContext context)
        {
            _context = context;
        }

        //Json Format
        //{
        //    "tipoOperacao": "Tipo de Operação(Aplicação ou Resgate)" (enum)
        //    "idFundo": "Id do fundo" (guid)
        //    "cpfCliente": "Cpf do cliente" (string)
        //    "valorMovimentacao": "Valor da movimentação" (decimal)
        //    "dataMovimentacao": "Data da movimentação" (datetime)
        //}

        // GET: api/Operacoes (lista todas as operações feitas)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Operacao>>> GetOperacoes()
        {
            return await _context.Operacoes.ToListAsync();
        }

        // GET: api/Operacoes/5 (pega a operação específica pelo Id)
        [HttpGet("{id}")]
        public async Task<ActionResult<Operacao>> GetOperacao(Guid id)
        {
            var operacao = await _context.Operacoes.FindAsync(id);

            if (operacao == null)
            {
                return NotFound();
            }

            return operacao;
        }

        // PUT: api/Operacoes/5 (atualiza informações da operação)
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOperacao(Guid id, Operacao operacao)
        {
            if (id != operacao.Id)
            {
                return BadRequest();
            }

            _context.Entry(operacao).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OperacaoExists(id))
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

        // POST: api/Operacoes (faz uma nova operação)
        [HttpPost]
        public async Task<ActionResult<Operacao>> PostOperacao(Operacao operacao)
        {

            operacao.DataMovimentacao = DateTime.Now;
            _context.Operacoes.Add(operacao);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetOperacao), new { id = operacao.Id }, operacao);
        }

        // DELETE: api/Operacoes/5 (deleta uma operação)
        [HttpDelete("{id}")]
        public async Task<ActionResult<Operacao>> DeleteOperacao(Guid id)
        {
            var operacao = await _context.Operacoes.FindAsync(id);
            if (operacao == null)
            {
                return NotFound();
            }

            _context.Operacoes.Remove(operacao);
            await _context.SaveChangesAsync();

            return operacao;
        }

        private bool OperacaoExists(Guid id)
        {
            return _context.Operacoes.Any(e => e.Id == id);
        }
    }
}
