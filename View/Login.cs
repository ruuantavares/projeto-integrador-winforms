using Repository;
using Model;
using System.Windows.Forms;
namespace View;

public class ViewLogin : Form
{
    private readonly TextBox InpEmail;
    private readonly TextBox InpSenha;
    private readonly Button BtnEntrar;

    public ViewLogin()
    {
        Text = "Login";
        Size = new Size(300, 200);
        StartPosition = FormStartPosition.CenterScreen;

        Label lblEmail = new()
        {
            Text = "Email:",
            Location = new Point(30, 30)
        };
        InpEmail = new TextBox
        {
            Location = new Point(100, 30),
            Width = 150
        };
        Label lblSenha = new()
        {
            Text = "Senha:",
            Location = new Point(30, 70)
        };
        InpSenha = new TextBox
        {
            Location = new Point(100, 70),
            Width = 150,
            PasswordChar = '*' //esconde a senha
        };
        BtnEntrar = new Button
        {
            Text = "Entrar",
            Location = new Point(100, 110)
        };

        BtnEntrar.Click += BtnEntrar_Click;

        Controls.Add(lblEmail);
        Controls.Add(InpEmail);
        Controls.Add(lblSenha);
        Controls.Add(InpSenha);
        Controls.Add(BtnEntrar);
    }

    private void BtnEntrar_Click(object? sender, EventArgs e)
    {   //evita tentar logar com campos em branco
        if (string.IsNullOrWhiteSpace(InpEmail.Text) || string.IsNullOrWhiteSpace(InpSenha.Text))
        {
            MessageBox.Show("Preencha todos os campos.");
            return;
        }

        var usuario = RepositoryUsuario.Login(InpEmail.Text, InpSenha.Text);
        if (usuario != null)
        {
            Hide(); //oculta tela de login
            new ViewTransacao(usuario).ShowDialog(); //abre a tela principal com o usuario logado
            Close(); //aplicação termina ao fechar a tela principal
        }
        else
        {
            MessageBox.Show("Email ou senha inválidos.");
        }

        InpSenha.KeyDown += (s, e) =>
    {
        if (e.KeyCode == Keys.Enter)//Entrar com o botao enter
            BtnEntrar.PerformClick();
    };

    }

}