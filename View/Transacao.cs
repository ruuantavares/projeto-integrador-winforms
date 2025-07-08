using Model;
using Controller;
namespace View;

public class ViewTransacao : Form
{
    private readonly Usuario usuarioLogado;
    private readonly Label LblBoasVindas;
    private readonly LinkLabel LnkEditarConta;
    private readonly LinkLabel LnkLogout;
    private readonly Label LblDescricao;
    private readonly TextBox InpDescricao;
    private readonly Label LblCategoria;
    private readonly ComboBox InpCategoria;
    private readonly Label LblValor;
    private readonly TextBox InpValor;
    private readonly Label LblTipo;
    private readonly ComboBox InpTipo;  //ComboBox para permitir selecionar as opções sem precisar digitar
    private readonly Button BtnRegistrar;
    private readonly Button BtnAlterar;
    private readonly Button BtnDeletar;
    private readonly Button BtnRelatorio;
    private readonly DataGridView DgvTransacao;

    public ViewTransacao(Usuario usuario)
    {
        usuarioLogado = usuario;

        Size = new Size(460, 420);
        StartPosition = FormStartPosition.CenterScreen;

        LblBoasVindas = new Label
        {   //Cria um Label com a mensagem de boas-vindas usando o nome do usuário logado
            Text = $"Bem-vindo, {usuarioLogado.Nome}!",
            Location = new Point(20, 20),
            AutoSize = true, //faz o label ajustar sua largura ao texto.
            Font = new Font("Segoe UI", 10, FontStyle.Bold) //Usa uma fonte maior e em negrito.
        };

        LnkEditarConta = new LinkLabel
        {   //Um link chamado "Editar Conta", que será clicável.
            Text = "Editar Conta",
            Location = new Point(250, 20),
            AutoSize = true
        };
        LnkEditarConta.Click += (s, e) =>
        {   //Quando clicado, esconde a tela atual com Hide() e abre a nova tela ViewEditarConta.
            Hide();
            new ViewEditarConta(usuarioLogado, this).ShowDialog();//Passa o usuarioLogado para pré-preencher os dados, e this para poder voltar à tela depois.
        };

        LnkLogout = new LinkLabel
        {   // um link de sair(logout)
            Text = "Sair",
            Location = new Point(350, 20),
            AutoSize = true
        };
        LnkLogout.Click += (s, e) =>
        {
            Hide();
            new ViewLogin().ShowDialog(); // volta para tela de login
            Close();
        };


        LblDescricao = new Label
        {
            Text = "Descrição: ",
            Location = new Point(50, 110)
        };
        InpDescricao = new TextBox
        {
            Text = "",
            Location = new Point(150, 110),
            Size = new Size(200, 20)
        };
        LblCategoria = new Label
        {
            Text = "Categoria",
            Location = new Point(50, 170)
        };
        InpCategoria = new ComboBox
        {
            Location = new Point(150, 170),
            Size = new Size(200, 20),
            DropDownStyle = ComboBoxStyle.DropDownList
        };
        InpCategoria.Items.AddRange(new object[]{
            "Alimentação",
            "Transporte",
            "Lazer",
            "Salário",
            "Saúde",
            "Outros"
        });
        LblValor = new Label
        {
            Text = "Valor: ",
            Location = new Point(50, 140)
        };
        InpValor = new TextBox
        {
            Text = "",
            Location = new Point(150, 140),
            Size = new Size(200, 20)
        };
        LblTipo = new Label
        {
            Text = "Tipo: ",
            Location = new Point(50, 200)
        };
        InpTipo = new ComboBox
        {
            Location = new Point(150, 200),
            Size = new Size(200, 20),
            DropDownStyle = ComboBoxStyle.DropDownList // Bloqueia para escolher apenas opçoes
        };
        InpTipo.Items.Add(TipoTransacao.Entrada); // aqui sugere o tipo da transação, se é entrada ou saida
        InpTipo.Items.Add(TipoTransacao.Saida);

        BtnRegistrar = new Button
        {
            Text = "Registrar",
            Location = new Point(50, 250)
        };
        BtnRegistrar.Click += Registrar;

        BtnAlterar = new Button
        {
            Text = "Alterar",
            Location = new Point(135, 250)
        };
        BtnAlterar.Click += Alterar;

        BtnDeletar = new Button
        {
            Text = "Deletar",
            Location = new Point(220, 250)
        };
        BtnDeletar.Click += Deletar;
        BtnRelatorio = new Button
        {
            Text = "Relatório",
            Location = new Point(305, 250)
        };
        BtnRelatorio.Click += (s, e) =>
        {
            Hide();
            var formRel = new ViewRelatorio(usuarioLogado, this).ShowDialog();

        };

        DgvTransacao = new DataGridView
        {
            Location = new Point(0, 300),
            Size = new Size(450, 150)
        };

        Controls.Add(LblBoasVindas);
        Controls.Add(LnkEditarConta);
        Controls.Add(LnkLogout);
        Controls.Add(LblDescricao);
        Controls.Add(InpDescricao);
        Controls.Add(LblCategoria);
        Controls.Add(InpCategoria);
        Controls.Add(LblValor);
        Controls.Add(InpValor);
        Controls.Add(LblTipo);
        Controls.Add(InpTipo);
        Controls.Add(BtnRegistrar);
        Controls.Add(BtnAlterar);
        Controls.Add(BtnDeletar);
        Controls.Add(BtnRelatorio);
        Controls.Add(DgvTransacao);

        Listar();
    }
    private void Listar()
    {
        var transacoes = ControllerTransacao.Listar(usuarioLogado.Id);

        //pega apenas as 2  ultimas transaçoes
        var ultimas = transacoes.OrderByDescending(transacao => transacao.Data)//ordena da transação mais recente para a mais antiga.
        .Take(2) //seleciona somente as duas mais recentes.
        .ToList(); //converte em List<Transacao> para poder ser ligada à DataGridView.
        DgvTransacao.DataSource = ultimas;

        DgvTransacao.AutoGenerateColumns = false;
        DgvTransacao.Columns.Clear();

        DgvTransacao.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descricao", HeaderText = "Descrição" });
        DgvTransacao.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Valor", HeaderText = "Valor" });
        DgvTransacao.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Tipo", HeaderText = "Tipo" });
        DgvTransacao.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Data", HeaderText = "Data" });
    }
    private void Registrar(object? sender, EventArgs e)
    {

        if (InpValor.Text.Length < 1)
        {
            MessageBox.Show("Preencha o campo Valor.");
            return;
        }
        else if (InpTipo.SelectedItem == null) //SelectedItem pega a escolha do usuario no tipo de transacao
        {
            MessageBox.Show("Selecione o tipo de transação.");
            return;
        }
        if (InpCategoria.SelectedItem == null)
        {
            MessageBox.Show("Selecione uma categoria.");
            return;
        }


        // Recupera valores dos inputs
        string descricao = InpDescricao.Text;
        string categoria = InpCategoria.SelectedItem?.ToString() ?? "Outros";
        decimal valor = decimal.Parse(InpValor.Text);
        TipoTransacao tipo = (TipoTransacao)InpTipo.SelectedItem;

        //Usa a data atual 
        DateTime data = DateTime.Now;
        //para usar as informações no datagridview depois
        Transacao novaTransacao = new Transacao(data, valor, descricao, tipo, usuarioLogado.Id, categoria);
        ControllerTransacao.Registrar(novaTransacao);
        MessageBox.Show("Transação registrada com sucesso.");

        // Limpa os campos após cadastro
        InpDescricao.Text = "";
        InpValor.Text = "";
        InpCategoria.SelectedIndex = -1;
        InpTipo.SelectedIndex = -1;

        Listar();
    }
    private void Alterar(object? sender, EventArgs e)
    {
        int id = ((Transacao)DgvTransacao.SelectedRows[0].DataBoundItem).Id;
        if (InpValor.Text.Length < 1)
        {
            MessageBox.Show("Preencha o campo Valor.");
            return;
        }
        else if (InpTipo.SelectedItem == null)
        {
            MessageBox.Show("Selecione o tipo de transação.");
            return;
        }


        // Recupera valores dos inputs
        string categoria = InpCategoria.SelectedItem?.ToString() ?? "Outros";
        decimal valor = decimal.Parse(InpValor.Text);
        string descricao = InpDescricao.Text;
        TipoTransacao tipo = (TipoTransacao)InpTipo.SelectedItem;

        //Usa a data atual 
        DateTime data = DateTime.Now;

        Transacao novaTransacao = new(data, valor, descricao, tipo, usuarioLogado.Id, categoria);
        ControllerTransacao.Alterar(id, data, valor, descricao, categoria, tipo);

        InpDescricao.Text = "";
        InpValor.Text = "";
        InpCategoria.SelectedIndex = -1;
        InpTipo.SelectedIndex = -1;

        Listar();
    }
    private void Deletar(object? sender, EventArgs e)
    {
        int id = ((Transacao)DgvTransacao.SelectedRows[0].DataBoundItem).Id;
        ControllerTransacao.Deletar(id);
        Listar();
    }
}