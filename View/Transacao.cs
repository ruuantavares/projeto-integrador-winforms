using Model;
using Controller;
namespace View;

public class ViewTransacao : Form
{
    private readonly Label LblDescricao;
    private readonly TextBox InpDescricao;
    private readonly Label LblValor;
    private readonly TextBox InpValor;
    private readonly Label LblTipo;
    private readonly ComboBox InpTipo;  //ComboBox para permitir selecionar as opções sem precisar digitar
    private readonly Button BtnRegistrar;
    private readonly Button BtnAlterar;
    private readonly Button BtnDeletar;
    private readonly DataGridView DgvTransacao;

    public ViewTransacao()
    {
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

        DgvTransacao = new DataGridView
        {
            Location = new Point(0, 300),
            Size = new Size(400, 150)
        };

        Controls.Add(LblDescricao);
        Controls.Add(InpDescricao);
        Controls.Add(LblValor);
        Controls.Add(InpValor);
        Controls.Add(LblTipo);
        Controls.Add(InpTipo);
        Controls.Add(BtnRegistrar);
        Controls.Add(BtnAlterar);
        Controls.Add(BtnDeletar);
        Controls.Add(DgvTransacao);

        Listar();
    }
    private void Listar()
    {
        List<Transacao> transacoes = ControllerTransacao.Listar();
        DgvTransacao.DataSource = transacoes;
        DgvTransacao.AutoGenerateColumns = false;
        DgvTransacao.Columns.Clear();

        DgvTransacao.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Descricao",
            HeaderText = "Descricao"
        });
        DgvTransacao.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Valor",
            HeaderText = "Valor"
        });
        DgvTransacao.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Tipo",
            HeaderText = "Tipo"
        });
        DgvTransacao.Columns.Add(new DataGridViewTextBoxColumn
        {
            DataPropertyName = "Data",
            HeaderText = "Data"
        });
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


        // Recupera valores dos inputs
        decimal valor = decimal.Parse(InpValor.Text);
        string descricao = InpDescricao.Text;
        TipoTransacao tipo = (TipoTransacao)InpTipo.SelectedItem;

        //Usa a data atual 
        DateTime data = DateTime.Now;
        //para usar as informações no datagridview depois
        Transacao novaTransacao = new Transacao(data, valor, descricao, tipo);
        ControllerTransacao.Registrar(data, valor, InpDescricao.Text, tipo);
        MessageBox.Show("Transação registrada com sucesso.");
        Listar();
    }
    private void Alterar(object? sender, EventArgs e)
    {
        int index = DgvTransacao.SelectedRows[0].Index;
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
        decimal valor = decimal.Parse(InpValor.Text);
        string descricao = InpDescricao.Text;
        TipoTransacao tipo = (TipoTransacao)InpTipo.SelectedItem;

        //Usa a data atual 
        DateTime data = DateTime.Now;

        Transacao novaTransacao = new Transacao(data, valor, descricao, tipo);
        ControllerTransacao.Alterar(index, data, valor, InpDescricao.Text, tipo);
        Listar();
    }
    private void Deletar(object? sender, EventArgs e)
    {
        int index = DgvTransacao.SelectedRows[0].Index;
        ControllerTransacao.Deletar(index);
        Listar();
    }
}