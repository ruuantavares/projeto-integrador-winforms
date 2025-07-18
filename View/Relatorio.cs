using Controller;
using Model;
namespace View;

public class ViewRelatorio : Form
{
    private readonly Usuario usuarioLogado; //Guarda quem esta usando o sistema atualmente.Isso será passado ao abrir o relatório new ViewRelatorio(usuarioLogado)
    private readonly Form telaAnterior;
    private readonly ComboBox CmbCategoria, CmbTipo;
    private readonly DateTimePicker DtpDataInicio, DtpDataFim;
    private readonly TextBox InpBusca;
    private readonly Label LblEntrada, LblSaida, LblSaldo, LblDescricao,LblCategoria, LblTipo, LblDataInicio, LblDataFim;
    private readonly Button BtnPesquisar, BtnAlterar, BtnDeletar, BtnVoltar;
    private readonly DataGridView DgvTodos;


    public ViewRelatorio(Usuario usuario, Form telaPrincipal) //Construtor da janela,Recebe o usuário logado para filtrar os dados corretamente.
    {
        usuarioLogado = usuario; //Salva o usuário recebido no campo interno da classe.
        telaAnterior = telaPrincipal;

        Text = "Relatório Completo";//Define o título da janela, o tamanho e o centro da tela.
        Size = new Size(850, 500);
        StartPosition = FormStartPosition.CenterParent;
        // WindowState = FormWindowState.Maximized;

        DgvTodos = new DataGridView
        {
            Dock = DockStyle.Top,//gruda no topo
            Height = 320,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill ////Cria e configura o DataGridView e faz as colunas se ajustarem ao tamanho da janela
        };
        LblSaida = new Label
        {
            Text = "Saída: R$ 0,00",
            Dock = DockStyle.Top,
            Height = 15,
            Font = new Font("Segoe UI", 10, FontStyle.Italic)
        };
        LblEntrada = new Label
        {
            Text = "Entrada: R$ 0,00",
            Dock = DockStyle.Top,
            Height = 15,
            Font = new Font("Segoe UI", 10, FontStyle.Italic),
        };
        LblSaldo = new Label
        {
            Text = "Saldo: R$ 0,00",
            Dock = DockStyle.Top,
            Font = new Font("Segoe UI", 10, FontStyle.Italic),
            Height = 15
        };

        BtnVoltar = new Button //Adiciona um botão que fecha a janela
        {
            Text = "Voltar",
            Location = new Point(480, 410),
            Size = new Size(100, 30),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnVoltar.Click += (s, e) =>
        {
            if (telaAnterior is ViewTransacao transacaoForm)
            {
                transacaoForm.AtualizarTransacoes(); // Novo método que você criará
            }

            telaAnterior.Show();
            Close();
        };
        LblCategoria = new Label
        {
            Text = "Categoria :",
            Location = new Point(255, 365),
            Width = 100
        };
        CmbCategoria = new ComboBox
        {
            Location = new Point(255, 380),
            Size = new Size(80, 30),
            Width = 120
        };
        CmbCategoria.Items.AddRange(new object[] { "Todas", "Alimentação", "Transporte", "Lazer", "Salário", "Saúde", "Outros" });
        CmbCategoria.SelectedIndex = 0;

         LblTipo = new Label
        {
            Text = "Tipo :",
            Location = new Point(378, 365),
            Width = 100
        };
        CmbTipo = new ComboBox
        {
            Location = new Point(378, 380),
            Size = new Size(80, 30),
            Width = 100
        };
        CmbTipo.Items.AddRange(new object[] { "Todos", TipoTransacao.Entrada, TipoTransacao.Saida });
        CmbTipo.SelectedIndex = 0;

         LblDataInicio = new Label
        {
            Text = "Data Inicio :",
            Location = new Point(480, 365),
            Width = 100
        };
        DtpDataInicio = new DateTimePicker
        {
            Location = new Point(480, 380),
            Size = new Size(80, 30),
            Width = 120
        };
         LblDataFim = new Label
        {
            Text = "Data Final :",
            Location = new Point(602, 365),
            Width = 100
        };
        DtpDataFim = new DateTimePicker
        {
            Location = new Point(602, 380),
            Size = new Size(80, 30),
            Width = 120,

        };
        LblDescricao = new Label
        {
            Text = "Descrição:",
            Location = new Point(130, 365),
            Size = new Size(80, 30),
            Width = 100
        };
        InpBusca = new TextBox
        {
            Text = "",
            Location = new Point(130, 380),
            Width = 120,
            BorderStyle = BorderStyle.FixedSingle
        };
        BtnPesquisar = new Button
        {
            Text = "🔍",
            Location = new Point(730, 378),
            Size = new Size(80, 30),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 12),
        };
        BtnPesquisar.Click += (s, e) => Filtrar();
        BtnAlterar = new Button
        {
            Text = "Alterar",
            Location = new Point(260, 410),
            Width = 100,
            Size = new Size(100, 30),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnAlterar.Click += (s, e) => AlterarSelecionado();
        BtnDeletar = new Button
        {
            Text = "Excluir",
            Location = new Point(370, 410),
            Width = 100,
            Size = new Size(100, 30),
            FlatStyle = FlatStyle.Flat,
            ForeColor = Color.White,
            BackColor = Color.FromArgb(80, 80, 80),
            Font = new Font("Segoe UI", 10),
        };
        BtnDeletar.Click += (s, e) => DeletarSelecionado();


        Controls.Add(LblEntrada);
        Controls.Add(LblSaida);
        Controls.Add(LblSaldo);
        Controls.Add(DgvTodos);
        Controls.Add(BtnVoltar);
        Controls.Add(CmbCategoria);
        Controls.Add(CmbTipo);
        Controls.Add(DtpDataInicio);
        Controls.Add(DtpDataFim);
        Controls.Add(BtnPesquisar);
        Controls.Add(BtnAlterar);
        Controls.Add(BtnDeletar);
        Controls.Add(InpBusca);
        Controls.Add(LblDescricao);
        Controls.Add(LblCategoria);
        Controls.Add(LblTipo);
        Controls.Add(LblDataInicio);
        Controls.Add(LblDataFim);

        Load += (s, e) => AtualizarRelatorio();//Quando a janela for carregada, essa linha executa a função AtualizarRelatorio()

    }
    private void AtualizarRelatorio() //Função que carrega todas as transações do usuário, organiza e exibe.
    {
        var transacoes = ControllerTransacao.Listar(usuarioLogado.Id)
        .OrderByDescending(Transacao => Transacao.Data)
        .ToList();

        DgvTodos.AutoGenerateColumns = false;
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
    private void Filtrar()
    {
        var transacoes = ControllerTransacao.Listar(usuarioLogado.Id).AsQueryable();

        if (CmbCategoria.SelectedItem?.ToString() != "Todas")
        {
            transacoes = transacoes.Where(t => t.Categoria == CmbCategoria.SelectedItem.ToString());
        }

        if (CmbTipo.SelectedItem?.ToString() != "Todos")
        {
            transacoes = transacoes.Where(t => t.Tipo.ToString() == CmbTipo.SelectedItem.ToString());
        }

        transacoes = transacoes.Where(t =>
            t.Data.Date >= DtpDataInicio.Value.Date &&
            t.Data.Date <= DtpDataFim.Value.Date
        );

        if (!string.IsNullOrWhiteSpace(InpBusca.Text))
        {
            transacoes = transacoes.Where(t => t.Descricao.Contains(InpBusca.Text, StringComparison.OrdinalIgnoreCase));
        }

        var lista = transacoes.ToList();
        DgvTodos.DataSource = lista;
        DgvTodos.AutoGenerateColumns = false;

        var entrada = lista.Where(t => t.Tipo == TipoTransacao.Entrada).Sum(t => t.Valor);
        var saida = lista.Where(t => t.Tipo == TipoTransacao.Saida).Sum(t => t.Valor);
        var saldo = entrada - saida;

        LblEntrada.Text = $"Entrada: R$ {entrada:N2}";
        LblSaida.Text = $"Saída: R$ {saida:N2}";
        LblSaldo.Text = $"Saldo: R$ {saldo:N2}";
    }
    private void AlterarSelecionado()
    {
        if (DgvTodos.SelectedRows.Count == 0)
        {
            return;
        }
        var transacao = (Transacao)DgvTodos.SelectedRows[0].DataBoundItem;
        // Abre um form modal (ViewTransacao no modo edição) ou pergunta os novos valores aqui
        // Abre em modo edição
        Hide();
        var formEdicao = new ViewTransacao(usuarioLogado, transacao, true);
        formEdicao.FormClosed += (s, e) =>
        {
            Show();
            Filtrar();
        };
        formEdicao.ShowDialog();
    }
    private void DeletarSelecionado()
    {
        // Garante que exista ao menos 1 linha selecionada
        if (DgvTodos.SelectedRows.Count == 0)
        {
            MessageBox.Show("Por favor, selecione uma transação para excluir.");
            return;
        }

        //  Garante que o DataBoundItem não seja nulo
        var item = DgvTodos.SelectedRows[0].DataBoundItem;
        if (item is not Transacao transacao)
        {
            MessageBox.Show("Nenhuma transação válida foi selecionada.");
            return;
        }

        // Confirma exclusão com o usuário
        var resposta = MessageBox.Show(
            $"Deseja excluir a transação \"{transacao.Descricao}\"?",
            "Confirmar exclusão",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );
        if (resposta != DialogResult.Yes)
            return;

        // Realiza exclusão e atualiza a listagp
        try
        {
            ControllerTransacao.Deletar(transacao.Id);
            AtualizarRelatorio();
            MessageBox.Show("Transação excluída com sucesso.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao excluir: {ex.Message}");
        }
    }
   

}