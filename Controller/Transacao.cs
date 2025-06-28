using Model;
using Repository;

namespace Controller;

public class ControllerTransacao
{
    public static void Criar(DateTime data, decimal valor, string descricao, TipoTransacao tipo)
    {
        new Transacao(data, valor, descricao, tipo);
    }
    public static List<Transacao> Listar()
    {
        return RepositoryTransacao.Listar();
    }
    public static void Alterar(int index, DateTime data, decimal valor, string descricao, TipoTransacao tipo)
    {
        RepositoryTransacao.Alterar(index, data, valor, descricao, tipo);
    }
    public static void Deletar(int index)
    {
        RepositoryTransacao.Deletar(index);
    }
}
