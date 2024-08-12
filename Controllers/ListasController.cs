using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Models;
using ToDo.Service;

namespace ToDo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ListasController : ControllerBase
    {
        private readonly ServicoGerenciamentoAtividades<Atividade> _servico;

        public ListasController(ServicoGerenciamentoAtividades<Atividade> servico)
        {
            _servico = servico;
        }

        [HttpPost]
        public async Task<IActionResult> CriarLista([FromBody] string nomeLista)
        {
            await _servico.CriarListaAsync(nomeLista);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> ListarListas()
        {
            var listas = await _servico.ListarListasAsync();
            return Ok(listas);
        }

        [HttpPost("{nomeLista}/atividades")]
        public async Task<IActionResult> AdicionarAtividade(string nomeLista, [FromBody] Atividade atividade)
        {
            await _servico.AdicionarAtividadeAsync(nomeLista, atividade);
            return Ok();
        }

        [HttpPost("{nomeLista}/atividades/{idAtividade}/concluir")]
        public async Task<IActionResult> ConcluirAtividade(string nomeLista, int idAtividade)
        {
            await _servico.ConcluirAtividadeAsync(nomeLista, idAtividade);
            return Ok();
        }

        [HttpDelete("{nomeLista}/atividade/{id}")]
        public async Task<IActionResult> RemoverAtividade(string nomeLista, int id)
        {
            await _servico.RemoverAtividadeAsync(nomeLista, id);
            return NoContent();
        }
    }

}
