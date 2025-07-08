using Controller;
using Model;
namespace View;

public class ViewRelatorio : Form
{
    private readonly Usuario usuarioLogado; //Guarda quem esta usando o sistema atualmente.Isso será passado ao abrir o relatório new ViewRelatorio(usuarioLogado)
    private readonly Form telaAnterior;
    private readonly DataGridView DgvTodos;
    private readonly Label LblEntrada;
    private readonly Label LblSaida;
    private readonly Label LblSaldo;
    private readonly Button BtnVoltar;

    public ViewRelatorio(Usuario usuario, Form telaPrincipal) //Construtor da janela,Recebe o usuário logado para filtrar os dados corretamente.
    {
        usuarioLogado = usuario; //Salva o usuário recebido no campo interno da classe.
        telaAnterior = telaPrincipal;

        Text = "Relatório Completo";//Define o título da janela, o tamanho e o centro da tela.
        Size = new Size(700, 500);
        StartPosition = FormStartPosition.CenterParent;

        DgvTodos = new DataGridView
        {
            Dock = DockStyle.Top,//gruda no topo
            Height = 320,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ////Cria e configura o DataGridView e faz as colunas se ajustarem ao tamanho da janela
        };
        LblEntrada = new Label
        {
            Text = "Entrada: R$ 0,00",
            Dock = DockStyle.Top,
            Height = 25
        };
        LblSaida = new Label
        {
            Text = "Saída: R$ 0,00",
            Dock = DockStyle.Top,
            Height = 25
        };
        LblSaldo = new Label
        {
            Text = "Saldo: R$ 0,00",
            Dock = DockStyle.Top,
            Height = 25
        };

        BtnVoltar = new Button //Adiciona um botão que fecha a janela
        {
            Text = "Voltar",
            Location = new Point(600, 400)
        };
        BtnVoltar.Click += (s, e) =>
        {
            telaAnterior.Show(); // Reexibe a tela de transações
            Close();        // Fecha a tela de relatório
        };


        Controls.Add(LblEntrada);
        Controls.Add(LblSaida);
        Controls.Add(LblSaldo);
        Controls.Add(DgvTodos);
        Controls.Add(BtnVoltar);

        Load += (s, e) => AtualizarRelatorio();//Quando a janela for carregada, essa linha executa a função AtualizarRelatorio()

    }
    private void AtualizarRelatorio() //Função que carrega todas as transações do usuário, organiza e exibe.
    {
        var transacoes = ControllerTransacao.Listar(usuarioLogado.Id)
        .OrderByDescending(Transacao => Transacao.Data)
        .ToList();

        DgvTodos.DataSource = transacoes; //Preenche o DataGridView com os dados carregados

        DgvTodos.Columns.Clear();
        DgvTodos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Descricao", HeaderText = "Descrição" });
        DgvTodos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Categoria", HeaderText = "Categoria" });
        DgvTodos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Valor", HeaderText = "Valor (R$)" });
        DgvTodos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Tipo", HeaderText = "Tipo de Transação" });
        DgvTodos.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Data", HeaderText = "Data", DefaultCellStyle = new DataGridViewCellStyle { Format = "dd/MM/yyyy" } }); // Exibe a data no formato brasileiro

        (var totalEntrada, var totalSaida, var saldo) = ControllerTransacao.ObterTotais(usuarioLogado.Id);

        LblEntrada.Text = $"Entrada: R$ {totalEntrada:N2}";
        LblSaida.Text = $"Saída: R$ {totalSaida:N2}";
        LblSaldo.Text = $"Saldo: R$ {saldo:N2}";
    }

}