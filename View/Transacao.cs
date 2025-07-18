using Model;
using Controller;
namespace View;

public class ViewTransacao : Form
{
    private Usuario usuarioLogado;
    private Label LblBoasVindas;
    private LinkLabel LnkEditarConta;
    private LinkLabel LnkLogout;
    private Label LblDescricao;
    private TextBox InpDescricao;
    private Label LblCategoria;
    private ComboBox InpCategoria;
    private Label LblValor;
    private TextBox InpValor;
    private Label LblTipo;
    private ComboBox InpTipo;  //ComboBox para permitir selecionar as opções sem precisar digitar
    private Button BtnRegistrar;
    private Button BtnAlterar;
    private Button BtnDeletar;
    private Button BtnRelatorio;
    private DataGridView DgvTransacao;

    private int? transacaoEditandoId = null;

    public ViewTransacao(Usuario usuario)
    {
        // WindowState = FormWindowState.Maximized;

        Initialize(usuario, null, false);
    }
    public ViewTransacao(Usuario usuario, Transacao existente, bool modoEditar = false)
    {
        Initialize(usuario, existente, modoEditar);
    }
    public void Initialize(Usuario usuario, Transacao? existente, bool modoEditar)
    {
        usuarioLogado = usuario;

        Size = new Size(700, 700);
        StartPosition = FormStartPosition.CenterScreen;
        KeyPreview = true; // Faz o form receber teclas mesmo se o foco estiver em outro controle


        LblBoasVindas = new Label
        {   //Cria um Label com a mensagem de boas-vindas usando o nome do usuário logado
            Text = $"Bem-vindo, {usuarioLogado.Nome}!",
            Location = new Point(30, 30),
            AutoSize = true, //faz o label ajustar sua largura ao texto.
            Font = new Font("Segoe UI", 30, FontStyle.Bold), //Usa uma fonte maior e em negrito.
        };

        LnkEditarConta = new LinkLabel
        {   //Um link chamado "Editar Conta", que será clicável.
            Text = "Editar Conta",
            Location = new Point(450, 50),
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.Black,
        };
        LnkEditarConta.Click += (s, e) =>
        {
            Hide();
            new ViewEditarConta(usuarioLogado, this).ShowDialog();//Passa o usuarioLogado para pré-preencher os dados, e this para poder voltar à tela depois.
            
        };

        LnkLogout = new LinkLabel
        {   // um link de sair(logout)
            Text = "Sair",
            Location = new Point(600, 50),
            AutoSize = true,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Color.Black,
        };
        LnkLogout.Click += (s, e) =>
        {
            Hide();
            new ViewLogin().ShowDialog(); // volta para tela de login
        };

        LblDescricao = new Label
        {
            Text = "Descrição: ",
            Location = new Point(30, 100),
            Font = new Font("Segoe UI", 12)
        };
        InpDescricao = new TextBox
        {
            Text = "",
            Location = new Point(150, 100),
            Size = new Size(200, 20),
            BorderStyle = BorderStyle.None

        };
        LblCategoria = new Label
        {
            Text = "Categoria",
            Location = new Point(30, 250),
            Font = new Font("Segoe UI", 12)
        };
        InpCategoria = new ComboBox
        {
            Location = new Point(150, 250),
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
            Location = new Point(30, 150),
            Font = new Font("Segoe UI", 12)
        };
        InpValor = new TextBox
        {
            Text = "",
            Location = new Point(150, 150),
            Size = new Size(200, 20),
            BorderStyle = BorderStyle.None
        };
        LblTipo = new Label
        {
            Text = "Tipo: ",
            Size = new Size(50, 50),
            Location = new Point(30, 200),
            Font = new Font("Segoe UI", 12)
        };
        InpTipo = new ComboBox
        {

            Location = new Point(150, 200),
            Size = new Size(200, 20),
            DropDownStyle = ComboBoxStyle.DropDownList // Bloqueia para escolher apenas opçoes

        };
        GroupBox groupBox = new GroupBox
        {
            Text = "Nova Transação",
            Font = new Font("Segoe UI", 15, FontStyle.Italic),
            Size = new Size(450, 350),
            Location = new Point(120, 100),
            BackColor = Color.LightGray,

        };
        InpTipo.Items.Add(TipoTransacao.Entrada); // aqui sugere o tipo da transação, se é entrada ou saida
        InpTipo.Items.Add(TipoTransacao.Saida);

        BtnRegistrar = new Button
        {
            Text = "Registrar",
            Location = new Point(120, 500),
            Size = new Size(100, 40),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),

        };
        BtnRegistrar.Click += Registrar;

        BtnAlterar = new Button
        {
            Text = "Alterar",
            Location = new Point(230, 500),
            Size = new Size(100, 40),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnAlterar.Click += Alterar;

        BtnDeletar = new Button
        {
            Text = "Deletar",
            Location = new Point(350, 500),
            Size = new Size(100, 40),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnDeletar.Click += Deletar;
        BtnRelatorio = new Button
        {
            Text = "Relatório",
            Location = new Point(470, 500),
            Size = new Size(100, 40),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnRelatorio.Click += (s, e) =>
        {
            Hide();
            var formRel = new ViewRelatorio(usuarioLogado, this).ShowDialog();

        };

        DgvTransacao = new DataGridView
        {
            Location = new Point(20, 550),
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 10),
            Dock = DockStyle.Bottom,//gruda no topo
            Height = 100,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ////Cria e configura o DataGridView e faz as colunas se ajustarem ao tamanho da janela
        };
        groupBox.Controls.Add(LblDescricao);
        groupBox.Controls.Add(InpDescricao);
        groupBox.Controls.Add(LblCategoria);
        groupBox.Controls.Add(InpCategoria);
        groupBox.Controls.Add(LblValor);
        groupBox.Controls.Add(InpValor);
        groupBox.Controls.Add(LblTipo);
        groupBox.Controls.Add(InpTipo);
        Controls.Add(BtnRegistrar);
        Controls.Add(BtnAlterar);
        Controls.Add(BtnDeletar);
        Controls.Add(BtnRelatorio);
        Controls.Add(DgvTransacao);
        Controls.Add(LblBoasVindas);
        Controls.Add(LnkLogout);
        Controls.Add(LnkEditarConta);
        Controls.Add(groupBox);


        if (modoEditar && existente != null)
        {
            // Preenche os campos com os dados da transação existente
            InpDescricao.Text = existente.Descricao;
            InpValor.Text = existente.Valor.ToString("N2");
            InpCategoria.SelectedItem = existente.Categoria;
            InpTipo.SelectedItem = existente.Tipo;

            transacaoEditandoId = existente.Id;
        }
        Listar();
    }

    public void AtualizarTransacoes()
    {
        Listar();
    }
    private void Listar()
    {
        var transacoes = ControllerTransacao.Listar(usuarioLogado.Id);

        //pega apenas as 5  ultimas transaçoes
        var ultimas = transacoes.OrderByDescending(transacao => transacao.Data)//ordena da transação mais recente para a mais antiga.
        .Take(4) //seleciona somente as duas mais recentes.
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
        if (transacaoEditandoId == null)
        {
            MessageBox.Show("Nenhuma transação foi selecionada para edição.");
            return;
        }

        if (InpValor.Text.Length < 1)
        {
            MessageBox.Show("Preencha o campo Valor.");
            return;
        }
        if (InpTipo.SelectedItem == null)
        {
            MessageBox.Show("Selecione o tipo de transação.");
            return;
        }

        string categoria = InpCategoria.SelectedItem?.ToString() ?? "Outros";
        decimal valor = decimal.Parse(InpValor.Text);
        string descricao = InpDescricao.Text;
        TipoTransacao tipo = (TipoTransacao)InpTipo.SelectedItem;
        DateTime data = DateTime.Now;

        ControllerTransacao.Alterar(transacaoEditandoId.Value, data, valor, descricao, categoria, tipo);
        MessageBox.Show("Transação alterada com sucesso!");

        // limpa tudo
        InpDescricao.Text = "";
        InpValor.Text = "";
        InpCategoria.SelectedIndex = -1;
        InpTipo.SelectedIndex = -1;
        transacaoEditandoId = null;

        Listar();
    }
    private void Deletar(object? sender, EventArgs e)
    {
        int id = ((Transacao)DgvTransacao.SelectedRows[0].DataBoundItem).Id;
        ControllerTransacao.Deletar(id);
        Listar();
    }
    public void AtualizarNomeUsuario(string novoNome)
    {
        usuarioLogado.Nome = novoNome;
        LblBoasVindas.Text = $"Bem-vindo, {usuarioLogado.Nome}!";
        Location = new Point(30, 30);
        AutoSize = true;
        Font = new Font("Segoe UI", 30, FontStyle.Bold);
    }
}