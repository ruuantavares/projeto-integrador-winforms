using Repository;
namespace View;

public class ViewLogin : Form
{
    private readonly TextBox InpEmail;
    private readonly TextBox InpSenha;
    private readonly Button BtnEntrar;
    private readonly Button BtnRegistrar;

    public ViewLogin()
    {
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(420, 300);
        BackColor = Color.WhiteSmoke;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        KeyPreview = true; // Faz o form receber teclas mesmo se o foco estiver em outro controle


        Label Lbllogo = new()
        {
            Text = "Login",
            Font = new Font("Segoe UI", 25, FontStyle.Bold),
            Size = new Size(300, 55),
            Location = new Point(150, 5)
        };

        Label lblEmail = new()
        {
            Text = "Email:",
            Location = new Point(30, 80),
            Font = new Font("Segoe UI", 10),
            Size = new Size(50,15)

        };
        InpEmail = new TextBox
        {
            Location = new Point(150, 80),
            Size = new Size(200, 10),
            Font = new Font("Segoe UI", 10),
            BorderStyle = BorderStyle.FixedSingle
        };
        Label lblSenha = new()
        {
            Text = "Senha:",
            Location = new Point(30, 120),
            Font = new Font("Segoe UI", 10),
            Size = new Size(50,15)
            
        };
        InpSenha = new TextBox
        {
            Location = new Point(150, 120),
            Size = new Size(200, 10),
            Font = new Font("Segoe UI", 10),
            BorderStyle = BorderStyle.FixedSingle,
            PasswordChar = '*' //esconde a senha
        };
        BtnEntrar = new Button
        {
            Text = "Entrar",
            Location = new Point(100, 180),
            Size = new Size(100, 50),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10)
        };

        BtnEntrar.Click += BtnEntrar_Click;

        BtnRegistrar = new Button
        {
            Text = "Registrar",
            Location = new Point(250, 180),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Size = new Size(100, 50),
            Font = new Font("Segoe UI", 10),
        };

        BtnRegistrar.Click += (s, e) =>
        {
            Hide(); // esconde a tela de login
            new ViewCadastro().ShowDialog(); //entra na tela de cadastro.
        };
        Controls.Add(Lbllogo);
        Controls.Add(lblEmail);
        Controls.Add(InpEmail);
        Controls.Add(lblSenha);
        Controls.Add(InpSenha);
        Controls.Add(BtnEntrar);
        Controls.Add(BtnRegistrar);
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