using ToDo.Interfaces;

namespace ToDo.Models
{
    public class ListaAtividades<T> where T : IAtividade
    {
        public int Id { get; private set; }
        public string Nome { get; set; } = string.Empty;
        public List<T> Atividades { get; set; } = new List<T>();

        public ListaAtividades()
        {
            Atividades = new List<T>();
        }

        public void AdicionarAtividade(T atividade)
        {
            Atividades.Add(atividade);
        }

        public void RemoverAtividade(T atividade)
        {
            Atividades.Remove(atividade);
        }

        public IEnumerable<T> ListarAtividades()
        {
            return Atividades;
        }
    }

}
