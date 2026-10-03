namespace InterfaceGrafica
{
    public partial class CRUD : Form
    {
        private Panel panelMenu;
        private Panel panelConteudo;

        private Button btnCadastrar;
        private Button btnVisualizar;
        private Button btnEditar;
        private Button btnRemover;

        private TipoCadastro tipoCadastro;

        // CORES
        private Color corFundo =
            Color.FromArgb(245, 247, 250);

        private Color corMenu =
            Color.FromArgb(30, 41, 59);

        private Color corPrimaria =
            Color.FromArgb(79, 70, 229);

        private Color corTexto =
            Color.FromArgb(30, 41, 59);

        private Color corTextoSecundario =
            Color.FromArgb(100, 116, 139);


        // ============================================================
        // CONSTRUTOR
        // ============================================================

        public CRUD(TipoCadastro tipo)
        {
            InitializeComponent();

            tipoCadastro = tipo;

            ConfigurarFormulario();

            CriarInterface();

            MostrarCadastrar();
        }


        // ============================================================
        // CONFIGURAÇÃO
        // ============================================================

        private void ConfigurarFormulario()
        {
            Text = ObterTitulo();

            BackColor = corFundo;

            StartPosition =
                FormStartPosition.CenterScreen;
        }


        private string ObterTitulo()
        {
            switch (tipoCadastro)
            {
                case TipoCadastro.Impressora:
                    return "PAINEL - Impressoras";

                case TipoCadastro.Fornecedor:
                    return "PAINEL - Fornecedores";

                case TipoCadastro.MateriaPrima:
                    return "PAINEL - Matéria-Prima";

                case TipoCadastro.Produto:
                    return "PAINEL - Produtos";

                case TipoCadastro.Cliente:
                    return "PAINEL - Clientes";

                case TipoCadastro.Venda:
                    return "PAINEL - Vendas";

                default:
                    return "PAINEL";
            }
        }


        // ============================================================
        // INTERFACE
        // ============================================================

        private void CriarInterface()
        {
            CriarMenu();

            CriarPainelConteudo();

            Controls.Add(panelConteudo);

            Controls.Add(panelMenu);
        }


        // ============================================================
        // MENU
        // ============================================================

        private void CriarMenu()
        {
            panelMenu = new Panel();

            panelMenu.Dock =
                DockStyle.Left;

            panelMenu.Width = 210;

            panelMenu.BackColor =
                corMenu;


            // LOGO

            Label lblLogo = new Label();

            lblLogo.Text =
                "TechToys";

            lblLogo.ForeColor =
                Color.White;

            lblLogo.Font =
                new Font(
                    "Segoe UI",
                    18,
                    FontStyle.Bold
                );

            lblLogo.Location =
                new Point(25, 25);

            lblLogo.AutoSize = true;


            // NOME DO MÓDULO

            Label lblModulo = new Label();

            lblModulo.Text =
                ObterTitulo().ToUpper();

            lblModulo.ForeColor =
                Color.FromArgb(
                    148,
                    163,
                    184
                );

            lblModulo.Font =
                new Font(
                    "Segoe UI",
                    8,
                    FontStyle.Bold
                );

            lblModulo.Location =
                new Point(27, 62);

            lblModulo.AutoSize = true;


            // BOTÕES

            btnCadastrar =
                CriarBotaoMenu(
                    "Cadastrar",
                    110
                );

            btnVisualizar =
                CriarBotaoMenu(
                    "Visualizar",
                    165
                );

            btnEditar =
                CriarBotaoMenu(
                    "Editar",
                    220
                );

            btnRemover =
                CriarBotaoMenu(
                    "Remover",
                    275
                );


            // EVENTOS

            btnCadastrar.Click +=
                (s, e) =>
                {
                    MostrarCadastrar();
                };

            btnVisualizar.Click +=
                (s, e) =>
                {
                    MostrarVisualizar();
                };

            btnEditar.Click +=
                (s, e) =>
                {
                    MostrarEditar();
                };

            btnRemover.Click +=
                (s, e) =>
                {
                    MostrarRemover();
                };


            panelMenu.Controls.Add(lblLogo);

            panelMenu.Controls.Add(lblModulo);

            panelMenu.Controls.Add(btnCadastrar);

            panelMenu.Controls.Add(btnVisualizar);

            panelMenu.Controls.Add(btnEditar);

            panelMenu.Controls.Add(btnRemover);
        }


        private Button CriarBotaoMenu(
            string texto,
            int y)
        {
            Button botao =
                new Button();

            botao.Text =
                texto;

            botao.Location =
                new Point(15, y);

            botao.Size =
                new Size(180, 45);

            botao.BackColor =
                corMenu;

            botao.ForeColor =
                Color.White;

            botao.FlatStyle =
                FlatStyle.Flat;

            botao.FlatAppearance.BorderSize =
                0;

            botao.Font =
                new Font(
                    "Segoe UI",
                    10
                );

            botao.TextAlign =
                ContentAlignment.MiddleLeft;

            botao.Padding =
                new Padding(
                    15,
                    0,
                    0,
                    0
                );

            botao.Cursor =
                Cursors.Hand;

            return botao;
        }


        // ============================================================
        // PAINEL DE CONTEÚDO
        // ============================================================

        private void CriarPainelConteudo()
        {
            panelConteudo =
                new Panel();

            panelConteudo.Dock =
                DockStyle.Fill;

            panelConteudo.BackColor =
                corFundo;

            panelConteudo.Padding =
                new Padding(35);
        }


        private void LimparConteudo()
        {
            panelConteudo.Controls.Clear();
        }


        // ============================================================
        // TÍTULO DAS PÁGINAS
        // ============================================================

        private void CriarTituloPagina(
            string titulo,
            string subtitulo)
        {
            Label lblTitulo =
                new Label();

            lblTitulo.Text =
                titulo;

            lblTitulo.Font =
                new Font(
                    "Segoe UI",
                    22,
                    FontStyle.Bold
                );

            lblTitulo.ForeColor =
                corTexto;

            lblTitulo.Location =
                new Point(35, 30);

            lblTitulo.AutoSize = true;


            Label lblSubtitulo =
                new Label();

            lblSubtitulo.Text =
                subtitulo;

            lblSubtitulo.Font =
                new Font(
                    "Segoe UI",
                    10
                );

            lblSubtitulo.ForeColor =
                corTextoSecundario;

            lblSubtitulo.Location =
                new Point(37, 72);

            lblSubtitulo.AutoSize = true;


            panelConteudo.Controls.Add(
                lblTitulo
            );

            panelConteudo.Controls.Add(
                lblSubtitulo
            );
        }


        // ============================================================
        // CADASTRAR
        // ============================================================

        private void MostrarCadastrar()
        {
            LimparConteudo();

            CriarTituloPagina(
                "Cadastrar",
                "Preencha os dados para cadastrar um novo registro."
            );


            switch (tipoCadastro)
            {
                case TipoCadastro.Impressora:

                    CriarFormularioImpressora();

                    break;


                case TipoCadastro.Fornecedor:

                    CriarFormularioFornecedor();

                    break;


                case TipoCadastro.MateriaPrima:

                    CriarFormularioMateriaPrima();

                    break;


                case TipoCadastro.Produto:

                    CriarFormularioProduto();

                    break;


                case TipoCadastro.Cliente:

                    CriarFormularioCliente();

                    break;


                case TipoCadastro.Venda:

                    CriarFormularioVenda();

                    break;
            }
        }


        // ============================================================
        // FORMULÁRIO IMPRESSORA
        // ============================================================

        private void CriarFormularioImpressora()
        {
            Label lblNome =
                CriarLabel(
                    "Nome da impressora",
                    40,
                    130
                );


            TextBox txtNome =
                new TextBox();

            txtNome.Name =
                "txtNome";

            txtNome.Location =
                new Point(40, 155);

            txtNome.Size =
                new Size(350, 30);

            txtNome.Font =
                new Font(
                    "Segoe UI",
                    10
                );

            txtNome.PlaceholderText =
                "Ex.: Ender 3 V3";


            Label lblModelo =
                CriarLabel(
                    "Modelo",
                    40,
                    205
                );


            TextBox txtModelo =
                new TextBox();

            txtModelo.Name =
                "txtModelo";

            txtModelo.Location =
                new Point(40, 230);

            txtModelo.Size =
                new Size(350, 30);

            txtModelo.Font =
                new Font(
                    "Segoe UI",
                    10
                );

            txtModelo.PlaceholderText =
                "Ex.: Ender 3 V3 SE";


            Label lblKwh =
                CriarLabel(
                    "Consumo (KWh)",
                    40,
                    280
                );


            NumericUpDown numKwh =
                new NumericUpDown();

            numKwh.Name =
                "numKwh";

            numKwh.Location =
                new Point(40, 305);

            numKwh.Size =
                new Size(350, 30);

            numKwh.Font =
                new Font(
                    "Segoe UI",
                    10
                );

            numKwh.Minimum = 0;

            numKwh.Maximum = 10000;

            numKwh.DecimalPlaces = 2;

            numKwh.Increment = 0.1m;


            Label lblCusto =
                CriarLabel(
                    "Custo do KWh",
                    40,
                    355
                );


            Label valorKwh =
                new Label();

            valorKwh.Text =
                "R$ 0,789";

            valorKwh.Location =
                new Point(40, 380);

            valorKwh.AutoSize = true;

            valorKwh.Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                );

            valorKwh.ForeColor =
                corPrimaria;


            Button btnSalvar =
                new Button();

            btnSalvar.Text =
                "Cadastrar impressora";

            btnSalvar.Location =
                new Point(40, 430);

            btnSalvar.Size =
                new Size(220, 45);

            btnSalvar.BackColor =
                corPrimaria;

            btnSalvar.ForeColor =
                Color.White;

            btnSalvar.FlatStyle =
                FlatStyle.Flat;

            btnSalvar.FlatAppearance.BorderSize =
                0;

            btnSalvar.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold
                );

            btnSalvar.Cursor =
                Cursors.Hand;


            btnSalvar.Click +=
                (s, e) =>
                {
                    if (string.IsNullOrWhiteSpace(
                        txtNome.Text))
                    {
                        MessageBox.Show(
                            "Digite o nome da impressora.",
                            "Atenção",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        txtNome.Focus();

                        return;
                    }


                    if (string.IsNullOrWhiteSpace(
                        txtModelo.Text))
                    {
                        MessageBox.Show(
                            "Digite o modelo da impressora.",
                            "Atenção",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        txtModelo.Focus();

                        return;
                    }


                    MessageBox.Show(
                        "Impressora cadastrada!",
                        "Sucesso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                };


            panelConteudo.Controls.Add(
                lblNome
            );

            panelConteudo.Controls.Add(
                txtNome
            );

            panelConteudo.Controls.Add(
                lblModelo
            );

            panelConteudo.Controls.Add(
                txtModelo
            );

            panelConteudo.Controls.Add(
                lblKwh
            );

            panelConteudo.Controls.Add(
                numKwh
            );

            panelConteudo.Controls.Add(
                lblCusto
            );

            panelConteudo.Controls.Add(
                valorKwh
            );

            panelConteudo.Controls.Add(
                btnSalvar
            );
        }


        // ============================================================
        // OUTROS CADASTROS
        // ============================================================

        private void CriarFormularioFornecedor()
        {
            CriarLabel(
                "Nome",
                40,
                130
            );

            CriarTextBox(
                40,
                155
            );
        }


        private void CriarFormularioMateriaPrima()
        {
            CriarLabel(
                "Nome",
                40,
                130
            );

            CriarTextBox(
                40,
                155
            );
        }


        private void CriarFormularioProduto()
        {
            CriarLabel(
                "Nome",
                40,
                130
            );

            CriarTextBox(
                40,
                155
            );
        }


        private void CriarFormularioCliente()
        {
            CriarLabel(
                "Nome",
                40,
                130
            );

            CriarTextBox(
                40,
                155
            );
        }


        private void CriarFormularioVenda()
        {
            CriarLabel(
                "Venda",
                40,
                130
            );
        }


        // ============================================================
        // VISUALIZAR
        // ============================================================

        private void MostrarVisualizar()
        {
            LimparConteudo();

            CriarTituloPagina(
                "Visualizar",
                "Registros cadastrados."
            );


            DataGridView tabela =
                CriarTabela();


            PreencherTabela(
                tabela
            );


            panelConteudo.Controls.Add(
                tabela
            );
        }


        // ============================================================
        // EDITAR
        // ============================================================

        private void MostrarEditar()
        {
            LimparConteudo();

            CriarTituloPagina(
                "Editar",
                "Selecione um registro para editar."
            );


            DataGridView tabela =
                CriarTabela();


            PreencherTabela(
                tabela
            );


            panelConteudo.Controls.Add(
                tabela
            );


            Button btnEditar =
                new Button();

            btnEditar.Text =
                "Editar selecionado";

            btnEditar.Location =
                new Point(35, 550);

            btnEditar.Size =
                new Size(180, 45);

            btnEditar.BackColor =
                corPrimaria;

            btnEditar.ForeColor =
                Color.White;

            btnEditar.FlatStyle =
                FlatStyle.Flat;

            btnEditar.FlatAppearance.BorderSize =
                0;

            btnEditar.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold
                );

            btnEditar.Cursor =
                Cursors.Hand;


            btnEditar.Click +=
                (s, e) =>
                {
                    if (tabela.SelectedRows.Count == 0)
                    {
                        MessageBox.Show(
                            "Selecione um registro.",
                            "Atenção",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        return;
                    }


                    MessageBox.Show(
                        "Registro selecionado para edição.",
                        "Editar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                };


            panelConteudo.Controls.Add(
                btnEditar
            );
        }


        // ============================================================
        // REMOVER
        // ============================================================

        private void MostrarRemover()
        {
            LimparConteudo();

            CriarTituloPagina(
                "Remover",
                "Selecione um registro para excluir."
            );


            DataGridView tabela =
                CriarTabela();


            PreencherTabela(
                tabela
            );


            panelConteudo.Controls.Add(
                tabela
            );


            Button btnRemover =
                new Button();

            btnRemover.Text =
                "Remover selecionado";

            btnRemover.Location =
                new Point(35, 550);

            btnRemover.Size =
                new Size(190, 45);

            btnRemover.BackColor =
                Color.FromArgb(
                    220,
                    38,
                    38
                );

            btnRemover.ForeColor =
                Color.White;

            btnRemover.FlatStyle =
                FlatStyle.Flat;

            btnRemover.FlatAppearance.BorderSize =
                0;

            btnRemover.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold
                );

            btnRemover.Cursor =
                Cursors.Hand;


            btnRemover.Click +=
                (s, e) =>
                {
                    if (tabela.SelectedRows.Count == 0)
                    {
                        MessageBox.Show(
                            "Selecione um registro.",
                            "Atenção",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Warning
                        );

                        return;
                    }


                    DialogResult resultado =
                        MessageBox.Show(
                            "Deseja realmente remover este registro?",
                            "Confirmar remoção",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        );


                    if (resultado ==
                        DialogResult.Yes)
                    {
                        tabela.Rows.Remove(
                            tabela.SelectedRows[0]
                        );


                        MessageBox.Show(
                            "Registro removido.",
                            "Sucesso",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );
                    }
                };


            panelConteudo.Controls.Add(
                btnRemover
            );
        }


        // ============================================================
        // CRIAR TABELA
        // ============================================================

        private DataGridView CriarTabela()
        {
            DataGridView tabela =
                new DataGridView();

            tabela.Location =
                new Point(35, 120);

            tabela.Size =
                new Size(800, 400);

            tabela.BackgroundColor =
                Color.White;

            tabela.BorderStyle =
                BorderStyle.None;

            tabela.ReadOnly = true;

            tabela.AllowUserToAddRows =
                false;

            tabela.AllowUserToDeleteRows =
                false;

            tabela.SelectionMode =
                DataGridViewSelectionMode.FullRowSelect;

            tabela.MultiSelect =
                false;

            tabela.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.Fill;

            tabela.RowHeadersVisible =
                false;

            tabela.GridColor =
                Color.FromArgb(
                    226,
                    232,
                    240
                );

            tabela.Font =
                new Font(
                    "Segoe UI",
                    9
                );

            return tabela;
        }


        // ============================================================
        // PREENCHER TABELA
        // ============================================================

        private void PreencherTabela(
            DataGridView tabela)
        {
            switch (tipoCadastro)
            {
                // ----------------------------------------------------
                // IMPRESSORA
                // ----------------------------------------------------

                case TipoCadastro.Impressora:

                    tabela.Columns.Add(
                        "Id",
                        "ID"
                    );

                    tabela.Columns.Add(
                        "Nome",
                        "Nome"
                    );

                    tabela.Columns.Add(
                        "Modelo",
                        "Modelo"
                    );

                    tabela.Columns.Add(
                        "Kwh",
                        "KWh"
                    );


                    tabela.Rows.Add(
                        1,
                        "Ender 3",
                        "V3 SE",
                        "0,35"
                    );

                    tabela.Rows.Add(
                        2,
                        "Bambu Lab",
                        "A1",
                        "0,40"
                    );

                    break;


                // ----------------------------------------------------
                // FORNECEDOR
                // ----------------------------------------------------

                case TipoCadastro.Fornecedor:

                    tabela.Columns.Add(
                        "Id",
                        "ID"
                    );

                    tabela.Columns.Add(
                        "Nome",
                        "Nome"
                    );

                    tabela.Columns.Add(
                        "Email",
                        "E-mail"
                    );

                    tabela.Columns.Add(
                        "Telefone",
                        "Telefone"
                    );


                    tabela.Rows.Add(
                        1,
                        "Fornecedor Exemplo",
                        "email@email.com",
                        "(11) 99999-9999"
                    );

                    break;


                // ----------------------------------------------------
                // MATÉRIA PRIMA
                // ----------------------------------------------------

                case TipoCadastro.MateriaPrima:

                    tabela.Columns.Add(
                        "Id",
                        "ID"
                    );

                    tabela.Columns.Add(
                        "Nome",
                        "Nome"
                    );

                    tabela.Columns.Add(
                        "Peso",
                        "Peso"
                    );

                    tabela.Columns.Add(
                        "Preco",
                        "Preço"
                    );

                    tabela.Columns.Add(
                        "Estoque",
                        "Estoque"
                    );


                    tabela.Rows.Add(
                        1,
                        "PLA Branco",
                        "1 kg",
                        "R$ 90,00",
                        "10"
                    );

                    break;


                // ----------------------------------------------------
                // PRODUTO
                // ----------------------------------------------------

                case TipoCadastro.Produto:

                    tabela.Columns.Add(
                        "Id",
                        "ID"
                    );

                    tabela.Columns.Add(
                        "Nome",
                        "Nome"
                    );

                    tabela.Columns.Add(
                        "Valor",
                        "Valor de venda"
                    );

                    tabela.Columns.Add(
                        "Estoque",
                        "Estoque"
                    );

                    tabela.Columns.Add(
                        "Ativo",
                        "Ativo"
                    );


                    tabela.Rows.Add(
                        1,
                        "Cubo Decorativo",
                        "R$ 25,00",
                        "20",
                        "Sim"
                    );

                    break;


                // ----------------------------------------------------
                // CLIENTE
                // ----------------------------------------------------

                case TipoCadastro.Cliente:

                    tabela.Columns.Add(
                        "Id",
                        "ID"
                    );

                    tabela.Columns.Add(
                        "Nome",
                        "Nome"
                    );

                    tabela.Columns.Add(
                        "Telefone",
                        "Telefone"
                    );

                    tabela.Columns.Add(
                        "Instagram",
                        "Instagram"
                    );


                    tabela.Rows.Add(
                        1,
                        "João",
                        "(11) 99999-9999",
                        "@joao"
                    );

                    break;


                // ----------------------------------------------------
                // VENDA
                // ----------------------------------------------------

                case TipoCadastro.Venda:

                    tabela.Columns.Add(
                        "Id",
                        "ID"
                    );

                    tabela.Columns.Add(
                        "Data",
                        "Data/Hora"
                    );

                    tabela.Columns.Add(
                        "Cliente",
                        "Cliente"
                    );

                    tabela.Columns.Add(
                        "Produto",
                        "Produto"
                    );

                    tabela.Columns.Add(
                        "Quantidade",
                        "Quantidade"
                    );

                    tabela.Columns.Add(
                        "Embalagens",
                        "Embalagens"
                    );


                    tabela.Rows.Add(
                        1,
                        DateTime.Now.ToString(
                            "dd/MM/yyyy HH:mm"
                        ),
                        "João",
                        "Cubo Decorativo",
                        5,
                        1
                    );

                    break;
            }
        }


        // ============================================================
        // HELPERS
        // ============================================================

        private Label CriarLabel(
            string texto,
            int x,
            int y)
        {
            Label label =
                new Label();

            label.Text =
                texto;

            label.Location =
                new Point(x, y);

            label.AutoSize = true;

            label.ForeColor =
                corTexto;

            label.Font =
                new Font(
                    "Segoe UI",
                    10,
                    FontStyle.Bold
                );


            panelConteudo.Controls.Add(
                label
            );


            return label;
        }


        private TextBox CriarTextBox(
            int x,
            int y)
        {
            TextBox textBox =
                new TextBox();

            textBox.Location =
                new Point(x, y);

            textBox.Size =
                new Size(350, 30);

            textBox.Font =
                new Font(
                    "Segoe UI",
                    10
                );


            panelConteudo.Controls.Add(
                textBox
            );


            return textBox;
        }
    }
}
