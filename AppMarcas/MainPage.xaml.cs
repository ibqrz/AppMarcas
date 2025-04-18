
using AppMarcas.Models;

namespace AppMarcas
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        private void btnInserirOnClicked(object sender, EventArgs e)
        {
            Marca mar = new Marca();
            mar.marNome = txtMarNome.Text;
            mar.marObservacoes = txtMarObs.Text;

            App.Db.Insert(mar);
            DisplayAlert("Sucesso!", "Registro inserido.", "OK");
        }
    }
}
