namespace Repository;

using Model;
public class RepositoryTransacao
{
    static List<Transacao> transacoes = [];

    public static void Criar(Transacao transacao)
    {
        transacoes.Add(transacao);
    }
    public static List<Transacao> Listar()
    {
        return transacoes;
    }
    public static void Alterar(int index, DateTime data, decimal valor, string descricao, TipoTransacao tipo)
    {
        transacoes[index].Data = data;
        transacoes[index].Valor = valor;
        transacoes[index].Descricao = descricao;
        transacoes[index].Tipo = tipo;
    }
    public static void Deletar(int index)
    {
        transacoes.RemoveAt(index);
    }
}
