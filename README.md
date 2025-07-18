📊 Projeto Controle Financeiro (Windows Forms)
🔍 Sobre o Projeto
Aplicativo desktop em C# (.NET) para controle financeiro pessoal, com funcionalidades de:

Cadastro/login de usuário (MySQL)

Registro, edição, exclusão e listagem de transações (entrada/saída)

Filtro por categoria, tipo, data e descrição

Relatório completo com totais e navegação entre telas

🚀 Funcionalidades principais
🛠 Estrutura das Camadas
Model: define as entidades Usuario, Transacao e o enum TipoTransacao.

Repository: comunica com o banco MySQL (CRUD de transações e usuários).

Controller: lógica de negócios (registro, cálculo de saldos, listagem).

View: formulários Windows Forms (login, cadastro, transações, relatórios).

🧩 Métodos e funções relevantes
RepositoryTransacao
Criar(Transacao): insere nova transação no banco, pega id gerado.

Listar(int usuarioId): retorna todas transações do usuário.

Alterar(...): atualiza transação existente.

Deletar(int id): remove transação por id.

ControllerTransacao
Registrar(Transacao): chama RepositoryTransacao.Criar.

Listar(int): obtém transações filtrando por usuarioId.

Alterar(...): atualiza transação via repositório.

Deletar(id): exclui transação.

ObterTotais(int): soma entradas, saídas e calcula saldo.

ViewTransacao (Formulário principal)
Registrar: valida dados, cria objeto Transacao, chama ControllerTransacao.Registrar.

Alterar: carrega transação selecionada, atualiza via Controller.

Deletar: remove a transação selecionada.

Listar: recupera as duas últimas transações e exibe no DataGridView.

ViewRelatorio
AtualizarRelatorio: carrega todas as transações do usuário, mostra totais.

Filtrar: aplica filtros por descrição, categoria, tipo e data, atualiza grid e totais.

AlterarSelecionado: redireciona ao formulário de edição com dados pré-preenchidos.

DeletarSelecionado: exclui transação com confirmação do usuário e atualiza a tela.
