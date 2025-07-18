using Repository;

namespace Model;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; } // senha criptografado


    public static Usuario CriarComSenhaSegura(string nome, string email, string senha)
    {
        return new Usuario(nome, email, RepositoryUsuario.ComputeHash(senha));
    }

    public Usuario(string nome, string email, string senhaHash)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
    }
}