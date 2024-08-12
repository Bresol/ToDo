using Microsoft.EntityFrameworkCore;
using ToDo.Data;
using ToDo.Interfaces;
using ToDo.Models;

namespace ToDo.Service
{
    public class ServicoGerenciamentoAtividades<T> where T : class, IAtividade
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public ServicoGerenciamentoAtividades(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        }

        public async Task CriarListaAsync(string nomeLista)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AtividadesDbContext>();

            if (string.IsNullOrEmpty(nomeLista))
                throw new ArgumentException("Nome da lista não pode ser vazio.", nameof(nomeLista));

            var lista = new ListaAtividades<T> { Nome = nomeLista };
            context.Set<ListaAtividades<T>>().Add(lista);
            await context.SaveChangesAsync();
        }

        public async Task AdicionarAtividadeAsync(string nomeLista, T atividade)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AtividadesDbContext>();

            if (atividade == null)
                throw new ArgumentNullException(nameof(atividade));

            var lista = await context.Set<ListaAtividades<T>>()
                .FirstOrDefaultAsync(l => l.Nome == nomeLista);

            if (lista != null)
            {
                lista.AdicionarAtividade(atividade);
                context.Update(lista);
                await context.SaveChangesAsync();
            }
        }

        public async Task ConcluirAtividadeAsync(string nomeLista, int idAtividade)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AtividadesDbContext>();

            var lista = await context.Set<ListaAtividades<T>>()
                .Include(l => l.Atividades)
                .FirstOrDefaultAsync(l => l.Nome == nomeLista);

            var atividade = lista?.Atividades.FirstOrDefault(a => a.Id == idAtividade);
            if (atividade != null)
            {
                atividade.Concluir();
                context.Update(lista);
                await context.SaveChangesAsync();
            }
        }

        public async Task<List<ListaAtividades<T>>> ListarListasAsync()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AtividadesDbContext>();

            return await context.Set<ListaAtividades<T>>()
                .Include(l => l.Atividades)
                .ToListAsync();
        }

        public async Task RemoverAtividadeAsync(string nomeLista, int idAtividade)
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AtividadesDbContext>();

            var lista = await context.Set<ListaAtividades<T>>()
                .Include(l => l.Atividades)
                .FirstOrDefaultAsync(l => l.Nome == nomeLista);

            if (lista != null)
            {
                var atividade = lista.Atividades.FirstOrDefault(a => a.Id == idAtividade);
                if (atividade != null)
                {
                    lista.RemoverAtividade(atividade);
                    context.Update(lista);
                    await context.SaveChangesAsync();
                }
            }
        }
    }

}
