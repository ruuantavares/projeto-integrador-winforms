namespace Repository;

using MySql.Data.MySqlClient;
public static class Conexao
{   
    // se usa const quando o valor nunca vai mudar, é acessado apenas para leitura
    private const string ConnectionString = "server=localhost;database=pi_controle_financeiro;user id=root;password=''";
    public static MySqlConnection Abrir()
    {
        var conexao = new MySqlConnection(ConnectionString);
        conexao.Open();
        return conexao;
    }
}