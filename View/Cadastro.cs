using Model;
using Repository;

namespace View;

public class ViewCadastro : Form
{
    private readonly TextBox InpNome;
    private readonly TextBox InpEmail;
    private readonly TextBox InpSenha;
    private readonly TextBox InpConfirmarSenha;
    private readonly Button BtnCadastrar;
    private readonly Button BtnVoltar;

    public ViewCadastro()
    {
        Text = "Cadastro de Usuário";
        Size = new Size(700, 500);
        StartPosition = FormStartPosition.CenterScreen;

        var titulo = new Label
        {
            Text = "Crie sua Conta",
            Font = new Font("Segoe UI", 24, FontStyle.Bold),
            Location = new Point(30, 30),
            AutoSize = true
        };

        GroupBox groupBox = new GroupBox
        {
            Text = "Informações do Usuário",
            Font = new Font("Segoe UI", 15, FontStyle.Italic),
            Size = new Size(450, 280),
            Location = new Point(120, 100),
            BackColor = Color.LightGray,
        };

        var lblNome = new Label
        {
            Text = "Nome:",
            Location = new Point(30, 40),
            Font = new Font("Segoe UI", 12),
            AutoSize = true
        };
        InpNome = new TextBox
        {
            Location = new Point(170, 40),
            Size = new Size(220, 25),
            Font = new Font("Segoe UI", 11),
            BorderStyle = BorderStyle.None
        };

        var lblEmail = new Label
        {
            Text = "Email:",
            Location = new Point(30, 80),
            Font = new Font("Segoe UI", 12),
            AutoSize = true
        };
        InpEmail = new TextBox
        {
            Location = new Point(170, 80),
            Size = new Size(220, 25),
            Font = new Font("Segoe UI", 11),
            BorderStyle = BorderStyle.None
        };

        var lblSenha = new Label
        {
            Text = "Senha:",
            Location = new Point(30, 120),
            Font = new Font("Segoe UI", 12),
            AutoSize = true
        };
        InpSenha = new TextBox
        {
            Location = new Point(170, 120),
            Size = new Size(220, 25),
            Font = new Font("Segoe UI", 11),
            BorderStyle = BorderStyle.None,
            PasswordChar = '*'
        };

        var lblConfirmar = new Label
        {
            Text = "Confirmar Senha:",
            Location = new Point(30, 160),
            Font = new Font("Segoe UI", 12),
            AutoSize = true
        };
        InpConfirmarSenha = new TextBox
        {
            Location = new Point(170, 160),
            Size = new Size(220, 25),
            Font = new Font("Segoe UI", 11),
            BorderStyle = BorderStyle.None,
            PasswordChar = '*'
        };

        BtnCadastrar = new Button
        {
            Text = "Cadastrar",
            Location = new Point(170, 210),
            Size = new Size(100, 35),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnCadastrar.Click += BtnCadastrar_Click;

        BtnVoltar = new Button
        {
            Text = "Voltar",
            Location = new Point(290, 210),
            Size = new Size(100, 35),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnVoltar.Click += (s, e) =>
        {
            Hide();
            new ViewLogin().ShowDialog();
        };

        // Adiciona controles ao GroupBox
        groupBox.Controls.Add(lblNome);
        groupBox.Controls.Add(InpNome);
        groupBox.Controls.Add(lblEmail);
        groupBox.Controls.Add(InpEmail);
        groupBox.Controls.Add(lblSenha);
        groupBox.Controls.Add(InpSenha);
        groupBox.Controls.Add(lblConfirmar);
        groupBox.Controls.Add(InpConfirmarSenha);
        groupBox.Controls.Add(BtnCadastrar);
        groupBox.Controls.Add(BtnVoltar);

        // Adiciona ao Form
        Controls.Add(titulo);
        Controls.Add(groupBox);
    }

    private void BtnCadastrar_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InpNome.Text) ||
            string.IsNullOrWhiteSpace(InpEmail.Text) ||
            string.IsNullOrWhiteSpace(InpSenha.Text))
        {
            MessageBox.Show("Preencha todos os campos.");
            return;
        }

        if (InpSenha.Text != InpConfirmarSenha.Text)
        {
            MessageBox.Show("As senhas não coincidem.");
            return;
        }

        var usuario = Usuario.CriarComSenhaSegura(InpNome.Text, InpEmail.Text, InpSenha.Text);
        RepositoryUsuario.Registrar(usuario);
        MessageBox.Show("Cadastro realizado com sucesso!");

        Hide();
        new ViewLogin().ShowDialog();
    }
}
