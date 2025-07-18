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

        StartPosition = FormStartPosition.CenterScreen;
        Text = "Editar Conta";
        Size = new Size(700, 500);

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

        BtnSalvar = new Button
        {
            Text = "Salvar",
            Location = new Point(100, 150)
        };
        BtnSalvar.Click += BtnSalvar_Click;
        if (telaAnterior is ViewTransacao telaTransacao)
        {
            telaTransacao.AtualizarNomeUsuario(usuarioLogado.Nome);
        }
        BtnVoltar = new Button
        {
            Text = "Voltar",
            Location = new Point(190, 150)
        };
        BtnVoltar.Click += (s, e) =>
        {
            Hide();
            new ViewTransacao(usuarioLogado).ShowDialog();
            
        };

        Controls.Add(lblNome);
        Controls.Add(InpNome);
        Controls.Add(lblEmail);
        Controls.Add(InpEmail);
        Controls.Add(lblSenha);
        Controls.Add(InpSenha);
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
        MessageBox.Show("Dados atualizados!");
        new ViewTransacao(usuario).ShowDialog();
        Close();
    }

    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        telaAnterior.Show();
        base.OnFormClosed(e);
    }
}
