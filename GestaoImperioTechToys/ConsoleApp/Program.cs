using System;
using System.Runtime.InteropServices;
using TechToysDados;
using TechToysDominio;

public class Program
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
    private struct CONSOLE_FONT_INFOEX
    {
        public uint cbSize;
        public uint nFont;
        public COORD dwFontSize;
        public int FontFamily;
        public int FontWeight;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        public string FaceName;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct COORD
    {
        public short X;
        public short Y;
    }

    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern IntPtr GetStdHandle(int nStdHandle);

    [DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
    private static extern bool SetCurrentConsoleFontEx(IntPtr hConsoleOutput, bool bMaximumWindow, ref CONSOLE_FONT_INFOEX lpConsoleCurrentFontEx);

    [DllImport("kernel32.dll")]
    private static extern IntPtr GetConsoleWindow();

    [DllImport("user32.dll")]
    private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

    private const int SW_MAXIMIZE = 3;
    private const int STD_OUTPUT_HANDLE = -11;

    public static void Main()
    {
        ConfigurarCMD(titulo: "~Gestão~Tech~&~Toys~", nomeFonte: "Cascadia Code", tamanhoFonte: 33, negrito: true, corFundo: ConsoleColor.Magenta, corTexto: ConsoleColor.White);
        InicializarBancoDeDados();
        //TestarImpressora();
        //TestarProduto();
        //TestarMateriaPrima();
        //TestarVenda();
        MostrarCRUD();
        //Teste();
    }

    public static void ConfigurarCMD(string titulo, string nomeFonte, short tamanhoFonte, bool negrito, ConsoleColor corFundo, ConsoleColor corTexto)
    {
        Console.Title = titulo;
        Console.BackgroundColor = corFundo;
        Console.ForegroundColor = corTexto;
        Console.Clear();

        IntPtr hnd = GetStdHandle(STD_OUTPUT_HANDLE);

        if (hnd != IntPtr.Zero)
        {
            CONSOLE_FONT_INFOEX fontInfo = new CONSOLE_FONT_INFOEX();
            fontInfo.cbSize = (uint)Marshal.SizeOf(fontInfo);

            fontInfo.dwFontSize = new COORD { X = 0, Y = tamanhoFonte };
            fontInfo.FontFamily = 54;
            fontInfo.FontWeight = negrito ? 700 : 400;
            fontInfo.FaceName = nomeFonte;

            SetCurrentConsoleFontEx(hnd, false, ref fontInfo);
        }

        IntPtr consoleWindow = GetConsoleWindow();

        if (consoleWindow != IntPtr.Zero)
        {
            ShowWindow(consoleWindow, SW_MAXIMIZE);
        }
    }

    public static void InicializarBancoDeDados()
    {
        var repo = new RepositorioTechToys();
        //repo.LimparBanco();
        repo.CriarBanco();
        Console.Clear();
        //Console.WriteLine("Banco de dados limpo e criado!");
    }

    static public int escolha = -1;

    static public void LimparTabela(String tabela)
    {
        var repo = new RepositorioTechToys();
        repo.LimparBanco(tabela);
    }

    static public void MostrarCRUD()
    {
        while (true)
        {
            Console.Clear();
            CabecalhoPersonalizado("PAINEL", "TECH & TOYS");
            Console.WriteLine("(1) Cadastrar");
            Console.WriteLine("(2) Visualizar");
            Console.WriteLine("(3) Editar");
            Console.WriteLine("(4) Deletar");
            Console.WriteLine("(5) Visão Geral");
            Console.WriteLine("(6) Financeiro");
            Console.WriteLine("(11) Atalhos");
            Console.WriteLine("(00) Sair");
            Console.Write("\nDigite a opção: ");

            string? entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out escolha))
            {
                Console.WriteLine("\nEntrada inválida! Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                continue;
            }

            if (escolha == 0)
            {
                EncerrarConsole();
            }
            else if (escolha == 11 || escolha >= 1 && escolha <= 6)
            {
                AbrirPainel(escolha);
            }
            else
            {
                Console.WriteLine("\nOpção fora do menu! Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    static public void AbrirPainel(int escolhaCrud)
    {
        while (true)
        {
            Console.Clear();

            String nomeEscolha = "";
            int key = 0;

            switch (escolhaCrud)
            {
                case 1:
                    CabecalhoPersonalizado("CADASTRO", null);
                    break;

                case 2:
                    CabecalhoPersonalizado("VISUALIZAR", null);
                    break;

                case 3:
                    CabecalhoPersonalizado("EDITAR", null);
                    break;

                case 4:
                    CabecalhoPersonalizado("DELETAR", null);
                    break;

                case 5:
                    CabecalhoPersonalizado("VISÃO GERAL", null);

                    break;

                case 6:
                    CabecalhoPersonalizado("FINANCEIRO", null);
                    break;
                case 11:
                    CabecalhoPersonalizado("ATALHOS", null);
                    key = 1;
                    break;
            }

            if(key == 0){
                Console.WriteLine("(1) Impressoras");
                Console.WriteLine("(2) Fornecedores");
                Console.WriteLine("(3) Matéria-Primas");
                Console.WriteLine("(4) Produtos");
                Console.WriteLine("(5) Clientes");
                Console.WriteLine("(6) Embalagens");
                Console.WriteLine("(7) Vendas");
                Console.WriteLine("(00) Voltar");
                Console.Write($"\nDigite a opção que você deseja: ");
            }
            if(key == 1)
            {
                Console.WriteLine("(1) Realizar Venda");
                Console.WriteLine("(2) Visualizar Vendas");
                Console.WriteLine("(3) Cadastrar Cliente");
                Console.WriteLine("(4) Limpar Banco");
            }
            string? entrada = Console.ReadLine();

            if (!int.TryParse(entrada, out escolha))
            {
                Console.WriteLine("\nEntrada inválida! Pressione qualquer tecla para continuar...");
                Console.ReadKey();
                continue;
            }

            if (escolha == 0)
            {
                break;
            }
            else if (escolha >= 1 && escolha <= 7)
            {
                ExecutarTarefa(escolhaCrud, escolha);
            }
            else
            {
                Console.WriteLine("\nOpção fora do menu! Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }

    public static void ExecutarTarefa(int escolhaCrud, int escolha)
    {
        var repo = new RepositorioTechToys();

        string cliente_Nome = "";
        string cliente_Telefone = "";
        string cliente_Email = "";
        string cliente_Instagram = "";

        string fornecedor_Nome = "";
        string fornecedor_Email = "";
        string fornecedor_Telefone = "";
        string fornecedor_Site = "";

        string impressora_Nome = "";
        string impressora_Modelo = "";
        double impressora_Kwh = 0.0;
        double impressora_Custo_Reais_Kwh = 0.0;

        string materiaPrima_Nome = "";
        int materiaPrima_Peso_compra = 0;
        double materiaPrima_Preco_compra = 0.0;
        int materiaPrima_Fornecedor_Id = 0;
        int materiaPrima_Estoque = 1;

        string embalagem_Nome = "";
        string embalagem_Tipo = "";
        double embalagem_largura = 0.0;
        double embalagem_comprimento = 0.0;
        double embalagem_altura = 0.0;
        double embalagem_Preco = 0.0;
        int embalagem_Estoque = 0;
        Fornecedor embalagem_Fornecedor = null;
        int embalagem_Fornecedor_Id = 0;

        string produto_Nome = "";
        double produto_Peso_Impressao = 0.0;
        double produto_Tempo_Impressao = 0.0;
        double produto_largura = 0.0;
        double produto_comprimento = 0.0;
        double produto_altura = 0.0;
        double produto_Valor_Venda = 0.0;
        int produto_Estoque = 0;
        MateriaPrima produto_Materia_Prima = null;
        Impressora produto_Impressora = null;
        int produto_Materia_Prima_Id = 0;
        int produto_Impressora_Id = 0;

        DateTime venda_Data_Hora = DateTime.MinValue;
        int venda_Quantidade = 0;
        int venda_Quantidade_Embalagem = 0;
        Cliente venda_Cliente = null;
        Produto venda_Produto = null;
        Embalagem venda_Embalagem = null;
        int venda_Embalagem_Id = 0;
        int venda_Produto_Id = 0;
        int venda_Cliente_Id = 0;

        String nomeTabela = "";

        Console.Clear();

        switch (escolhaCrud)
        {
            case 1:

                switch (escolha)
                {
                    case 1:
                        CabecalhoPersonalizado("CADASTRO", "IMPRESSORA");
                        nomeTabela = "Impressora";
                        Console.WriteLine("\nInsira o NOME da impressora: ");
                        impressora_Nome = Console.ReadLine();
                        Console.WriteLine("\nInsira o MODELO da impressora: ");
                        impressora_Modelo = Console.ReadLine();
                        Console.WriteLine("\nInsira o GASTO em KWH (-.-) da impressora: ");
                        impressora_Kwh = Convert.ToDouble(Console.ReadLine());
                        repo.CadastrarImpressora(impressora_Nome, impressora_Modelo, impressora_Kwh);
                        break;

                    case 2:
                        CabecalhoPersonalizado("CADASTRO", "FORNECEDOR");
                        nomeTabela = "Fornecedor";
                        Console.WriteLine("\nInsira o NOME do fornecedor: ");
                        fornecedor_Nome = Console.ReadLine();
                        Console.WriteLine("\nInsira o EMAIL do fornecedor: ");
                        fornecedor_Email = Console.ReadLine();
                        Console.WriteLine("\nInsira o TELEFONE do fornecedor: ");
                        fornecedor_Telefone = Console.ReadLine();
                        Console.WriteLine("\nInsira o SITE do fornecedor: ");
                        fornecedor_Site = Console.ReadLine();
                        repo.CadastrarFornecedor(fornecedor_Nome, fornecedor_Email, fornecedor_Telefone, fornecedor_Site);
                        break;

                    case 3:
                        CabecalhoPersonalizado("CADASTRO", "MATÉRIA-PRIMA");
                        nomeTabela = "MateriaPrima";
                        Console.WriteLine("\nInsira o NOME da matéria-prima: ");
                        materiaPrima_Nome = Console.ReadLine();
                        Console.WriteLine("\nInsira o PESO (gramas) da matéria-prima: ");
                        materiaPrima_Peso_compra = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("\nInsira o PREÇO (R$-.-) da matéria-prima: ");
                        materiaPrima_Preco_compra = Convert.ToDouble(Console.ReadLine());

                        PrintarFornecedores();
                        Console.WriteLine("\nInsira o ID do fornecedor da matéria-prima: ");
                        materiaPrima_Fornecedor_Id = Convert.ToInt32(Console.ReadLine());

                        Fornecedor fornecedorEncontrado = repo.ObterPorId<Fornecedor>(materiaPrima_Fornecedor_Id);

                        if (fornecedorEncontrado == null)
                        {
                            Console.WriteLine("\nErro: Fornecedor com este ID não existe!");
                            Console.ReadKey();
                            break;
                        }

                        Console.WriteLine("\nInsira o ESTOQUE da matéria-prima: ");
                        materiaPrima_Estoque = Convert.ToInt32(Console.ReadLine());

                        repo.CadastrarMateriaPrima(materiaPrima_Nome, materiaPrima_Peso_compra, materiaPrima_Preco_compra, fornecedorEncontrado);

                        break;

                    case 4:
                        nomeTabela = "Produto";
                        CabecalhoPersonalizado("CADASTRO", "PRODUTO");
                        Console.WriteLine("\nInsira o NOME do produto: ");
                        produto_Nome = Console.ReadLine();
                        Console.WriteLine("\nInsira o PESO de IMPRESSÃO do produto: ");
                        produto_Peso_Impressao = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira o TEMPO de IMPRESSÃO do produto: ");
                        produto_Tempo_Impressao = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira a LARGURA(mm) do produto: ");
                        produto_largura = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira a COMPRIMENTO(mm) do produto: ");
                        produto_comprimento = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira a ALTURA(mm) do produto: ");
                        produto_altura = Convert.ToDouble(Console.ReadLine());

                        PrintarMateriasPrimas();
                        Console.WriteLine("\nInsira o ID da matéria-prima do produto: ");
                        produto_Materia_Prima_Id = Convert.ToInt32(Console.ReadLine());
                        MateriaPrima materiaPrimaEncontrada = repo.ObterPorId<MateriaPrima>(produto_Materia_Prima_Id);

                        if (materiaPrimaEncontrada == null)
                        {
                            Console.WriteLine("\nErro: Matéria-prima com este ID não existe!");
                            Console.ReadKey();
                            break;
                        }
                        produto_Materia_Prima = materiaPrimaEncontrada;

                        PrintarImpressoras();
                        Console.WriteLine("\nInsira o ID da impressora a ser utilizada: ");
                        produto_Impressora_Id = Convert.ToInt32(Console.ReadLine());

                        Impressora impressoraEncontrada = repo.ObterPorId<Impressora>(produto_Impressora_Id); //Busca impressora por ID

                        if (impressoraEncontrada == null)
                        {
                            Console.WriteLine("\nErro: Impressora com este ID não existe!");
                            Console.ReadKey();
                            break;
                        }
                        produto_Impressora = impressoraEncontrada;

                        Console.WriteLine("\nInsira o VALOR de VENDA do produto: ");
                        produto_Valor_Venda = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira o ESTOQUE do produto: ");
                        produto_Estoque = Convert.ToInt32(Console.ReadLine());
                        repo.CadastrarProduto(produto_Nome, produto_Materia_Prima, produto_Impressora, produto_Valor_Venda, produto_Peso_Impressao, produto_Tempo_Impressao, produto_largura, produto_comprimento, produto_altura, produto_Estoque);
                        break;

                    case 5:
                        nomeTabela = "Cliente";
                        CabecalhoPersonalizado("CADASTRO", "CLIENTE");

                        Console.WriteLine("\nInsira o NOME do cliente: ");
                        cliente_Nome = Console.ReadLine();
                        Console.WriteLine("\nInsira o TELEFONE do cliente: ");
                        cliente_Telefone = Console.ReadLine();
                        Console.WriteLine("\nInsira o EMAIL do cliente: ");
                        cliente_Email = Console.ReadLine();
                        Console.WriteLine("\nInsira o INSTAGRAM do cliente: ");
                        cliente_Instagram = Console.ReadLine();

                        repo.CadastrarCliente(cliente_Nome, cliente_Telefone, cliente_Email, cliente_Instagram);

                        break;

                    case 6:
                        CabecalhoPersonalizado("CADASTRO", "EMBALAGEM");

                        nomeTabela = "Embalagem";
                        Console.WriteLine("\nInsira o NOME da embalagem: ");
                        embalagem_Nome = Console.ReadLine();
                        Console.WriteLine("\nInsira o TIPO da embalagem: ");
                        embalagem_Tipo = Console.ReadLine();
                        Console.WriteLine("\nInsira a LARGURA(mm) da embalagem: ");
                        embalagem_largura = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira a COMPRIMENTO(mm) da embalagem: ");
                        embalagem_comprimento = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira a ALTURA(mm) da embalagem: ");
                        embalagem_altura = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira o PREÇO(R$) da embalagem: ");
                        embalagem_Preco = Convert.ToDouble(Console.ReadLine());
                        Console.WriteLine("\nInsira o ESTOQUE da embalagem: ");
                        embalagem_Estoque = Convert.ToInt32(Console.ReadLine());

                        Console.WriteLine();
                        PrintarFornecedores();
                        Console.WriteLine("\nInsira o ID do fornecedor da embalagem: ");
                        embalagem_Fornecedor_Id = Convert.ToInt32(Console.ReadLine());

                        fornecedorEncontrado = repo.ObterPorId<Fornecedor>(embalagem_Fornecedor_Id); //Busca fornecedor por ID

                        if (fornecedorEncontrado == null)
                        {
                            Console.WriteLine("\nErro: Fornecedor com este ID não existe!");
                            Console.ReadKey();
                            break;
                        }

                        embalagem_Fornecedor = fornecedorEncontrado;

                        repo.CadastrarEmbalagem(embalagem_Nome, embalagem_Tipo, embalagem_largura, embalagem_comprimento, embalagem_altura, embalagem_Preco, embalagem_Fornecedor, embalagem_Estoque);

                        break;

                    case 7:
                        nomeTabela = "Venda";
                        CabecalhoPersonalizado("CADASTRO", "VENDA");

                        venda_Data_Hora = DateTime.Now;

                        PrintarClientes();
                        Console.WriteLine("Digite o ID do CLIENTE da venda: ");
                        venda_Cliente_Id = Convert.ToInt32(Console.ReadLine());
                        Cliente clienteEncontrado = repo.ObterPorId<Cliente>(venda_Cliente_Id); //Busca Embalagem por ID

                        if (clienteEncontrado == null)
                        {
                            Console.WriteLine("\nErro: Cliente com este ID não existe!");
                            Console.ReadKey();
                            break;
                        }

                        venda_Cliente = clienteEncontrado;

                        PrintarProdutos();
                        Console.WriteLine("Digite o ID do PRODUTO da venda: ");
                        venda_Produto_Id = Convert.ToInt32(Console.ReadLine());
                        Produto produtoEncontrado = repo.ObterPorId<Produto>(venda_Produto_Id); //Busca Embalagem por ID

                        if (produtoEncontrado == null)
                        {
                            Console.WriteLine("\nErro: Produto com este ID não existe!");
                            Console.ReadKey();
                            break;
                        }
                        venda_Produto = produtoEncontrado;

                        Console.WriteLine($"Digite a QUANTIDADE de PRODUTOS da venda: ");
                        venda_Quantidade = Convert.ToInt32(Console.ReadLine());

                        string incluirEmbalagem;

                        while (true)
                        {
                            Console.WriteLine("Deseja incluir embalagem? (S/N): ");
                            incluirEmbalagem = Console.ReadLine().ToUpper();

                            if (incluirEmbalagem == "S")
                            {
                                PrintarEmbalagens();
                                Console.WriteLine("Digite o ID da EMBALAGEM da venda: ");
                                venda_Embalagem_Id = Convert.ToInt32(Console.ReadLine());

                                Embalagem embalagemEncontrada = repo.ObterPorId<Embalagem>(venda_Embalagem_Id);

                                if (embalagemEncontrada == null)
                                {
                                    Console.WriteLine("\nErro: Embalagem com este ID não existe!");
                                    Console.ReadKey();
                                    continue;
                                }

                                venda_Embalagem = embalagemEncontrada;

                                break;
                            }

                            if (incluirEmbalagem == "N")
                            {
                                venda_Embalagem = null;
                                venda_Quantidade_Embalagem = 0;
                                break;
                            }

                        }

                        break;
                }
                break;

            case 2:

                String XdaQuestao = "";

                switch (escolha)
                {
                    case 1:
                        PrintarImpressoras();
                        XdaQuestao = "Impressora";
                        break;

                    case 2:
                        PrintarFornecedores();
                        XdaQuestao = "Fornecedor";
                        break;

                    case 3:
                        PrintarMateriasPrimas();
                        XdaQuestao = "MateriaPrima";
                        break;

                    case 4:
                        PrintarProdutos();
                        XdaQuestao = "Produto";
                        break;

                    case 5:
                        PrintarClientes();
                        XdaQuestao = "Cliente";
                        break;

                    case 6:
                        PrintarEmbalagens();
                        XdaQuestao = "Embalagem";
                        break;

                    case 7:
                        PrintarVendas();
                        Console.WriteLine("Digite o ID para ver mais detalhes: ");
                        int idVenda = Convert.ToInt32(Console.ReadLine());
                        var venda = repo.ObterPorId<Venda>(idVenda);
                        Console.WriteLine(venda.MaisDetalhes());
                        XdaQuestao = "Venda";
                        break;
                }

                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;

            case 3:
                Console.WriteLine("\nPressione qualquer tecla para continuar...");
                Console.ReadKey();
                break;

            case 4:
                Console.WriteLine($"\nDeseja excluir a tabela {nomeTabela}? (S/N)");

                if (escolha == 7)
                {
                    Console.WriteLine("\nDeseja excluir tudo? (S/N)");
                    String entrada = Console.ReadLine();

                    if (entrada.ToUpper() == "S" || entrada.ToUpper() == "SIM")
                    {
                        repo.LimparBanco();
                    }
                    else
                    {
                        AbrirPainel(escolhaCrud);
                    }
                }
                else
                {

                }

                break;

            case 11:
                switch (escolha)
                {
                    case 1:
                        ExecutarTarefa(1, 7);
                        break;
                    case 2:
                        ExecutarTarefa(2, 7);
                        break;
                }
                break;
        }
    }

    public static void Ler<T>(List<T> lista)
    {
        if (lista == null || lista.Count == 0)
        {
            Console.WriteLine("Nenhum registro encontrado.");
            return;
        }

        foreach (T item in lista)
        {
            Console.WriteLine(item.ToString());
        }
        Console.WriteLine("\n");
    }

    public static void PrintarImpressoras()
    {
        var repo = new RepositorioTechToys();
        Console.WriteLine("\n=======LISTA DE IMPRESSORAS======");
        List<Impressora> listaImpressora = repo.ListarTodos<Impressora>();
        Ler(listaImpressora);
    }
    public static void PrintarFornecedores()
    {
        var repo = new RepositorioTechToys();
        Console.WriteLine("\n=======LISTA DE FORNECEDORES======");
        List<Fornecedor> listaFornecedor = repo.ListarTodos<Fornecedor>();
        Ler(listaFornecedor);
    }
    public static void PrintarMateriasPrimas()
    {
        var repo = new RepositorioTechToys();
        Console.WriteLine("\n=======LISTA DE MATÉRIA-PRIMAS======");
        List<MateriaPrima> listaMateriaPrima = repo.ListarTodos<MateriaPrima>();
        Ler(listaMateriaPrima);
    }
    public static void PrintarClientes()
    {
        var repo = new RepositorioTechToys();
        Console.WriteLine("\n=======LISTA DE CLIENTES======");
        List<Cliente> listaCliente = repo.ListarTodos<Cliente>();
        Ler(listaCliente);
    }
    public static void PrintarProdutos()
    {
        var repo = new RepositorioTechToys();
        Console.WriteLine("\n=======LISTA DE PRODUTOS======");
        List<Produto> listaProduto = repo.ListarTodos<Produto>();
        Ler(listaProduto);
    }

    public static void PrintarEmbalagens()
    {
        var repo = new RepositorioTechToys();
        Console.WriteLine("\n=======LISTA DE EMBALAGENS======");
        List<Embalagem> listaEmbalagem = repo.ListarTodos<Embalagem>();
        Ler(listaEmbalagem);
    }
    public static void PrintarVendas()
    {
        var repo = new RepositorioTechToys();
        Console.WriteLine("\n=======LISTA DE VENDAS======");
        List<Venda> listaVenda = repo.ListarTodos<Venda>();
        Ler(listaVenda);
    }
    public static void CabecalhoPersonalizado(String txt, String txt2)
    {
        string res = "";
        if(txt2 != null)
        {
            res = txt + " - " + txt2;
        }
        else
        {
            res = txt;
        }
        
        int tamanho = res.Length;
        String barra = "=========";
        for(int i = 0; i < tamanho; i++)
        {
            barra += "=";
        }
        barra += "=========";

        Console.WriteLine(barra);
        Console.WriteLine($"||==>>:  {res}  :<<==||");
        barra += "\n";
        Console.WriteLine(barra);

    }
    public static void EncerrarConsole()
    {
        Environment.Exit(0);
    }


    public static void Teste()
    {
        var repo = new RepositorioTechToys();

        repo.CriarBanco();
        repo.LimparBanco();

        var cli = repo.CadastrarCliente("Carlos", "11988887777", "abc@gmail.com", "@carlinhos");
        var imp = repo.CadastrarImpressora("Ender 3", "FDM", 0.15);

        var clienteBuscado = repo.ObterPorId<Cliente>(cli.Id);
        Console.WriteLine(clienteBuscado.Nome);

        clienteBuscado.Telefone = "11999990000";
        repo.Atualizar(clienteBuscado);

        repo.DeletarPorId<Cliente>(cli.Id);

        var todasImpressoras = repo.ListarTodos<Impressora>();
    }
}