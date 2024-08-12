namespace ToDo.Interfaces
{
    public interface IAtividade
    {
        int Id { get; }
        string Nome { get; set; }
        bool EstaConcluida { get; set; }
        void Concluir();
    }

}
