using Model;
using Repository;

namespace View;

public class ViewEditarConta : Form
{
    private readonly Usuario usuario;
    private readonly Form telaAnterior;
    private readonly TextBox InpNome;
    private readonly TextBox InpEmail;
    private readonly TextBox InpSenha;
    private readonly Button BtnSalvar;
    private readonly Button BtnVoltar;

    public ViewEditarConta(Usuario usuarioLogado, Form origem)
    {
        usuario = usuarioLogado;
        telaAnterior = origem;

        Text = "Editar Conta";
        Size = new Size(700, 600);
        StartPosition = FormStartPosition.CenterScreen;

        var titulo = new Label
        {
            Text = "Editar Conta",
            Font = new Font("Segoe UI", 24, FontStyle.Bold),
            Location = new Point(30, 30),
            AutoSize = true
        };

        var groupBox = new GroupBox
        {
            Text = "Atualize suas informações",
            Font = new Font("Segoe UI", 14, FontStyle.Italic),
            Size = new Size(450, 250),
            Location = new Point(120, 100),
            BackColor = Color.LightGray,
        };

        var lblNome = new Label
        {
            Text = "Nome:",
            Location = new Point(30, 50),
            Font = new Font("Segoe UI", 12),
            AutoSize = true
        };
        InpNome = new TextBox
        {
            Location = new Point(140, 50),
            Size = new Size(250, 25),
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 11),
            Text = usuario.Nome
        };

        var lblEmail = new Label
        {
            Text = "Email:",
            Location = new Point(30, 100),
            Font = new Font("Segoe UI", 12),
            AutoSize = true
        };
        InpEmail = new TextBox
        {
            Location = new Point(140, 100),
            Size = new Size(250, 25),
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 11),
            Text = usuario.Email
        };

        var lblSenha = new Label
        {
            Text = "Nova Senha:",
            Location = new Point(30, 150),
            Font = new Font("Segoe UI", 12),
            AutoSize = true
        };
        InpSenha = new TextBox
        {
            Location = new Point(140, 150),
            Size = new Size(250, 25),
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 11),
            PasswordChar = '*'
        };

        BtnSalvar = new Button
        {
            Text = "Salvar",
            Location = new Point(170, 370),
            Size = new Size(100, 40),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnSalvar.Click += BtnSalvar_Click;

        BtnVoltar = new Button
        {
            Text = "Voltar",
            Location = new Point(280, 370),
            Size = new Size(100, 40),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnVoltar.Click += (s, e) =>
        {
            Hide();
            telaAnterior.Show();
        };

       
        groupBox.Controls.Add(lblNome);
        groupBox.Controls.Add(InpNome);
        groupBox.Controls.Add(lblEmail);
        groupBox.Controls.Add(InpEmail);
        groupBox.Controls.Add(lblSenha);
        groupBox.Controls.Add(InpSenha);

        
        Controls.Add(titulo);
        Controls.Add(groupBox);
        Controls.Add(BtnSalvar);
        Controls.Add(BtnVoltar);
    }

    private void BtnSalvar_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(InpNome.Text) || string.IsNullOrWhiteSpace(InpEmail.Text))
        {
            MessageBox.Show("Preencha nome e email.");
            return;
        }

        usuario.Nome = InpNome.Text;
        usuario.Email = InpEmail.Text;
        if (!string.IsNullOrWhiteSpace(InpSenha.Text))
        {
            usuario.SenhaHash = RepositoryUsuario.ComputeHash(InpSenha.Text);
        }

        RepositoryUsuario.Atualizar(usuario);
        MessageBox.Show("Dados atualizados com sucesso!");

        if (telaAnterior is ViewTransacao transacaoForm)
        {
            transacaoForm.AtualizarNomeUsuario(usuario.Nome);
        }
        new ViewTransacao(usuario).ShowDialog();
        Close();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        telaAnterior.Show();
        base.OnFormClosed(e);
    }
}
