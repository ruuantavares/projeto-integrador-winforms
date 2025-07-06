namespace Model;

public class Usuario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string SenhaHash { get; set; } // senha criptografado

    public Usuario(string nome, string email, string senhahash)
    {
        Nome = nome;
        Email = email;
        SenhaHash = senhahash;
    }
}