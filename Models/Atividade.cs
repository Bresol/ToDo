using ToDo.Interfaces;

namespace ToDo.Models
{
    public class Atividade : IAtividade
    {
        public int Id { get; private set; }
        public string Nome { get; set; }
        public bool EstaConcluida { get; set; }

        public void Concluir()
        {
            EstaConcluida = true;
        }
    }

}
