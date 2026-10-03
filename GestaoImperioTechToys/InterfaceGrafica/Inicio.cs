using System.Diagnostics;

namespace InterfaceGrafica
{
    public partial class Inicio : Form
    {
        public int largura = Screen.PrimaryScreen.Bounds.Width;
        public int altura = Screen.PrimaryScreen.Bounds.Height;
        public Inicio()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            this.WindowState = FormWindowState.Maximized;
        }

        private void item1_click(object sender, EventArgs e)
        {
            CRUD crud = new CRUD(TipoCadastro.Impressora);
            crud.ShowDialog();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
