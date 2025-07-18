namespace Repository;

using Model;
using MySql.Data.MySqlClient;

public class RepositoryTransacao
{
    public static void Criar(Transacao transacao)
    {
        using var conexao = Conexao.Abrir();

        string queryInsert = "INSERT INTO transacoes(data, valor,categoria, descricao, tipo, usuarioId) VALUES (@Data, @Valor,@Categoria, @Descricao, @Tipo, @UsuarioId)";
        using var command = new MySqlCommand(queryInsert, conexao);

        try
        {   //adiciona os parametros com os valores da transação
            command.Parameters.AddWithValue("@Data", transacao.Data);
            command.Parameters.AddWithValue("@Valor", transacao.Valor);
            command.Parameters.AddWithValue("@Descricao", transacao.Descricao);
            command.Parameters.AddWithValue("@Tipo", transacao.Tipo.ToString());
            command.Parameters.AddWithValue("@Categoria", transacao.Categoria);
            command.Parameters.AddWithValue("@UsuarioId", transacao.UsuarioId);
            //executa o comando no banco
            int rowAffected = command.ExecuteNonQuery();
            if (rowAffected > 0)
            {
                //recupera o id gerado automaticamente
                transacao.Id = Convert.ToInt32(command.LastInsertedId);
            }
        }
        catch (Exception)
        {
            MessageBox.Show("Erro ao inserir transação.");
        }
    }
    public static List<Transacao> Listar(int usuarioId)
    {
        var lista = new List<Transacao>();

        using var conexao = Conexao.Abrir(); // Abre a conexão com o banco de dados usando sua classe Conexao.cs e o using var garante que a conexão será fechada automaticamente ao final do escopo
        string query = "SELECT * FROM transacoes WHERE usuarioId = @UserId";
        using var command = new MySqlCommand(query, conexao);
        command.Parameters.AddWithValue("@UserId", usuarioId);

        using var reader = command.ExecuteReader(); //Executa o comando SQL (SELECT) e retorna um MySqlDataReader, esse objeto permite ler os resultados linha por linha, também com using var para liberar recursos ao fim
        while (reader.Read()) //Lê uma linha por vez da tabela transacoes.O loop vai rodar até não ter mais linhas
        {
            var transacao = new Transacao(
            Convert.ToDateTime(reader["data"]),  //reader["coluna"] pega o valor da coluna da linha atual
            Convert.ToDecimal(reader["valor"]),
            reader["descricao"].ToString(),
            Enum.Parse<TipoTransacao>(reader["tipo"].ToString()), //converte string para o enum correspondente
            Convert.ToInt32(reader["usuarioId"]),
            reader["categoria"].ToString()
        );
            transacao.Id = Convert.ToInt32(reader["id"]);
            lista.Add(transacao); //Adiciona o objeto recém-criado à lista transacoes
        }

        return lista; //Após o loop terminar (todas as linhas lidas), retorna a lista com todas as transações lidas do banco
    }
    public static void Alterar(int id, DateTime data, decimal valor, string descricao, string categoria, TipoTransacao tipo)
    {
        using var conexao = Conexao.Abrir();
        string queryUpdate = "UPDATE transacoes SET data=@Data, valor=@Valor, descricao=@Descricao, categoria=@Categoria, tipo=@Tipo WHERE id=@Id";
        using var command = new MySqlCommand(queryUpdate, conexao);
        try
        {
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@Data", data);
            command.Parameters.AddWithValue("@Valor", valor);
            command.Parameters.AddWithValue("@Descricao", descricao);
            command.Parameters.AddWithValue("@Categoria", categoria);
            command.Parameters.AddWithValue("@Tipo", tipo.ToString());

            command.ExecuteNonQuery();
        }
        catch (Exception)
        {
            MessageBox.Show("Erro ao alterar transação.");
        }
    }
    public static void Deletar(int id)
    {
        using var conexao = Conexao.Abrir();
        string queryDelete = "DELETE FROM transacoes WHERE id = @Id";
        using var command = new MySqlCommand(queryDelete, conexao);
        try
        {
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
        }
        catch (Exception)
        {
            MessageBox.Show("Erro ao deletar transação.");
        }
    }
    
}
