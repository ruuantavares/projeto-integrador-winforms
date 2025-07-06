using Model;
using Controller;
namespace View;

public class ViewTransacao : Form
{
    private readonly Usuario usuarioLogado;
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

        Size = new Size(400, 400);
        StartPosition = FormStartPosition.CenterScreen;

        LblDescricao = new Label
        {
            Text = "Descrição: ",
            Location = new Point(50, 50)
        };
        InpDescricao = new TextBox
        {
            Text = "",
            Location = new Point(150, 50),
            Size = new Size(200, 20)
        };
        LblCategoria = new Label
        {
            Text = "Categoria",
            Location = new Point(50, 180)
        };
        InpCategoria = new ComboBox
        {
            Location = new Point(150, 180),
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
            Location = new Point(50, 100)
        };
        InpValor = new TextBox
        {
            Text = "",
            Location = new Point(150, 100),
            Size = new Size(200, 20)
        };
        LblTipo = new Label
        {
            Text = "Tipo: ",
            Location = new Point(50, 150)
        };
        InpTipo = new ComboBox
        {
            Location = new Point(150, 150),
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
            Location = new Point(150, 250)
        };
        BtnAlterar.Click += Alterar;

        BtnDeletar = new Button
        {
            Text = "Deletar",
            Location = new Point(250, 250)
        };
        BtnDeletar.Click += Deletar;
        BtnRelatorio = new Button
        {
            Text = "Relatório",
            Location = new Point(150, 220)
        };
        BtnRelatorio.Click += (s, e) =>
        {
            var formRel = new ViewRelatorio(usuarioLogado);
            formRel.ShowDialog();
        };

        DgvTransacao = new DataGridView
        {
            Location = new Point(0, 300),
            Size = new Size(400, 150)
        };

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

        var novaTransacao = new Transacao(data, valor, descricao, tipo, usuarioLogado.Id, categoria);
        ControllerTransacao.Alterar(id, data, valor, descricao, categoria, tipo);
        Listar();
    }
    private void Deletar(object? sender, EventArgs e)
    {
        int id = ((Transacao)DgvTransacao.SelectedRows[0].DataBoundItem).Id;
        ControllerTransacao.Deletar(id);
        Listar();
    }
}