using Repository;

namespace Model;

    //enum é uma forma de representar um conjunto fixo de valores nomeados
    //
    public enum TipoTransacao
{
    Entrada,
    Saida
}
public class Transacao
{
    public  DateTime Data { get; set; }
    public decimal Valor { get; set; } //para evitar erros com pontos flutuantes, quando se trata de valores, é bom usar o decimal
    public string Descricao { get; set; }
    public TipoTransacao Tipo{ get; set; }
    public Transacao(DateTime data, decimal valor, string descricao, TipoTransacao tipo)
    {
        Data = data;
        Valor = valor;
        Descricao = descricao;
        Tipo = tipo;
        //RepositoryTransacao.Criar(this);
    }

}
