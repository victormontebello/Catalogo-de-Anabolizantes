using domain.Interface;

namespace ListaDeAnabolizante
{
    public partial class Form1 : Form
    {
        private IAnabolizanteRepositorio _repositorio;

        public Form1()
        {
            InitializeComponent();
        }

        public Form1(IAnabolizanteRepositorio repositorio)
        {
            _repositorio = repositorio;
            InitializeComponent();
        }
    }
}