namespace Repository;

using Model;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;
public static class RepositoryUsuario
{
    public static void Registrar(Usuario usuario)
    {
        using var conexao = Conexao.Abrir();
        string queryInsert = "INSERT INTO usuarios(nome, email, senhahash) VALUES (@Nome, @Email, @SenhaHash)";
        using var command = new MySqlCommand(queryInsert, conexao);
        command.Parameters.AddWithValue("@Nome", usuario.Nome);
        command.Parameters.AddWithValue("@Email", usuario.Email);
        command.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
        command.ExecuteNonQuery();
        usuario.Id = Convert.ToInt32(command.LastInsertedId);
    }
    public static Usuario? Login(string email, string senhaPlain) //Recebe email e senhaPlain (texto puro)
    {
        string hash = ComputeHash(senhaPlain); //transforma senha em hash.
        using var conexao = Conexao.Abrir();
        string queryUpdate = "SELECT id, nome, email, senhaHash FROM usuarios WHERE email = @Email"; //busca usuario com o email
        using var command = new MySqlCommand(queryUpdate, conexao);
        command.Parameters.AddWithValue("@Email", email);

        using var reader = command.ExecuteReader(); //Compara hash salva e hash gerado.
        if (!reader.Read())//Se diferente retorna null > login falhou.
        {
            return null;
        }

        string storedHash = reader.GetString("senhaHash");
        if (storedHash != hash)
        {
            return null;
        }//Se igual: retorna o Usuario com Id.

        return new Usuario(
            nome: reader.GetString("nome"),
            email: reader.GetString("email"),
            senhaHash: storedHash)
        {
            Id = reader.GetInt32("id")
        };
    }
    public static string ComputeHash(string input)//Gera hash SHA-256 de forma segura.


    {
        using SHA256 sha = SHA256.Create();
        Byte[] bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
        StringBuilder stringBuilder = new();
        foreach (byte b in bytes) stringBuilder.Append(b.ToString("x2")); //Converte bytes em string hexadecimal (x2).
        return stringBuilder.ToString();
    }
    public static void Atualizar(Usuario usuario)
    {
        using var conexao = Conexao.Abrir();
        string query = "UPDATE usuarios SET nome = @Nome, email = @Email, senhaHash = @SenhaHash WHERE id = @Id";
        using var command = new MySqlCommand(query, conexao);
        command.Parameters.AddWithValue("@Nome", usuario.Nome);
        command.Parameters.AddWithValue("@Email", usuario.Email);
        command.Parameters.AddWithValue("@SenhaHash", usuario.SenhaHash);
        command.Parameters.AddWithValue("@Id", usuario.Id);
        command.ExecuteNonQuery();
    }

}