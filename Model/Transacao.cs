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
    public int Id { get; set; }
    public DateTime Data { get; set; }
    public decimal Valor { get; set; } //para evitar erros com pontos flutuantes, quando se trata de valores, é bom usar o decimal
    public string Categoria { get; set; }
    public string Descricao { get; set; }
    public TipoTransacao Tipo { get; set; }
    public int UsuarioId { get; set; } //armazena quem criou a transação.
    public Transacao(DateTime data, decimal valor, string descricao, TipoTransacao tipo, int usuarioId, string categoria = "")
    {
        Data = data;
        Valor = valor;
        Descricao = descricao;
        Tipo = tipo;
        UsuarioId = usuarioId;
        Categoria = categoria;
    }

}
