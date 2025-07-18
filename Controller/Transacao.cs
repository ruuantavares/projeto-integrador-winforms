using Model;
using Repository;

namespace Controller;

public class ControllerTransacao
{
    public static void Registrar(Transacao transacao)
    {
        RepositoryTransacao.Criar(transacao);
    }
    public static decimal CalcularTotalEntrada(int usuarioId)
    {
        return RepositoryTransacao.Listar(usuarioId).Where(transacao => transacao.Tipo == TipoTransacao.Entrada).Sum(transacao => transacao.Valor);
    }
    public static decimal CalcularTotalSaida(int usuarioId)
    {
        return RepositoryTransacao.Listar(usuarioId).Where(transacao => transacao.Tipo == TipoTransacao.Saida).Sum(transacao => transacao.Valor);
    }
    public static decimal CalcularSaldo(int usuarioId)
    {
        return CalcularTotalEntrada(usuarioId) - CalcularTotalSaida(usuarioId);
    }
    public static List<Transacao> Listar(int usuarioId)
    {
        return RepositoryTransacao.Listar(usuarioId).Where(transacao => transacao.UsuarioId == usuarioId).ToList();
    }
    public static void Alterar(int id, DateTime data, decimal valor, string descricao, string categoria, TipoTransacao tipo)
    {
        RepositoryTransacao.Alterar(id: id, data: data, valor: valor, descricao: descricao, categoria: categoria, tipo: tipo);
    }
    public static void Deletar(int index)
    {
        RepositoryTransacao.Deletar(index);
    }
    public static (decimal entrada, decimal saida, decimal saldo) ObterTotais(int usuarioId)
    {
        decimal entrada = CalcularTotalEntrada(usuarioId);
        decimal saida = CalcularTotalSaida(usuarioId);
        decimal saldo = entrada - saida;
        return (entrada, saida, saldo);
    }
}
