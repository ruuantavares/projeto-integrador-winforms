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
        Size = new Size(350, 300);
        StartPosition = FormStartPosition.CenterScreen;

        int lblWidth = 120;
        int inputWidth = 180;
        int startX = 20;
        int labelY = 20;
        int spacingY = 40;

        Label lblNome = new()
        {
            Text = "Nome:",
            Location = new Point(startX, labelY),
            Width = lblWidth
        };
        InpNome = new TextBox
        {
            Location = new Point(startX + lblWidth, labelY),
            Width = inputWidth
        };

        Label lblEmail = new()
        {
            Text = "Email:",
            Location = new Point(startX, labelY + spacingY),
            Width = lblWidth
        };
        InpEmail = new TextBox
        {
            Location = new Point(startX + lblWidth, labelY + spacingY),
            Width = inputWidth
        };


        Label lblSenha = new()
        {
            Text = "Senha:",
            Location = new Point(startX, labelY + spacingY * 2),
            Width = lblWidth
        };
        InpSenha = new TextBox
        {
            Location = new Point(startX + lblWidth, labelY + spacingY * 2),
            Width = inputWidth,
            PasswordChar = '*'
        };

        Label lblConfirmar = new()
        {
            Text = "Confirmar Senha:",
            Location = new Point(startX, labelY + spacingY * 3),
            Width = lblWidth
        };
        InpConfirmarSenha = new TextBox
        {
            Location = new Point(startX + lblWidth, labelY + spacingY * 3),
            Width = inputWidth,
            PasswordChar = '*'
        };

        BtnCadastrar = new Button
        {
            Text = "Cadastrar",
            Location = new Point(startX + lblWidth, labelY + spacingY * 4)
        };
        BtnCadastrar.Click += BtnCadastrar_Click;

        BtnVoltar = new Button
        {
            Text = "Voltar",
            Location = new Point(startX + lblWidth + 90, labelY + spacingY * 4)
        };
        BtnVoltar.Click += (s, e) =>
        {
            Hide();
            new ViewLogin().ShowDialog();
        };
        GroupBox groupBox = new GroupBox
        {
            Font = new Font("Segoe UI", 15, FontStyle.Italic),
            Size = new Size(350, 300),
            Location = new Point(120, 100),
            BackColor = Color.LightGray,

        };

        groupBox.Controls.Add(lblNome);
        groupBox.Controls.Add(InpNome);
        groupBox.Controls.Add(lblEmail);
        groupBox.Controls.Add(InpEmail);
        groupBox.Controls.Add(lblSenha);
        groupBox.Controls.Add(InpSenha);
        groupBox.Controls.Add(lblConfirmar);
        groupBox.Controls.Add(InpConfirmarSenha);
        Controls.Add(BtnCadastrar);
        Controls.Add(BtnVoltar);
    }

    private void BtnCadastrar_Click(object? sender, EventArgs e)
    {
        // Validações
        if (string.IsNullOrWhiteSpace(InpNome.Text) || string.IsNullOrWhiteSpace(InpEmail.Text) || string.IsNullOrWhiteSpace(InpSenha.Text))
        {
            MessageBox.Show("Preencha todos os campos.");
            return;
        }

        if (InpSenha.Text != InpConfirmarSenha.Text)
        {
            MessageBox.Show("As senhas não coincidem.");
            return;
        }

        // Criar novo usuário
        var novoUsuario = Usuario.CriarComSenhaSegura(InpNome.Text, InpEmail.Text, InpSenha.Text);

        RepositoryUsuario.Registrar(novoUsuario);
        MessageBox.Show("Usuário cadastrado com sucesso!");
        Hide();
        new ViewLogin().ShowDialog(); // Fecha a janela
    }
}
