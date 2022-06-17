using System.Windows.Forms;

namespace Легушка
{
    public partial class Form1 : Form
    {
        Game game;
        ImageList imgl;
        public Form1()
        {
            InitializeComponent();
            game = new Game(this);
        }
    }
}