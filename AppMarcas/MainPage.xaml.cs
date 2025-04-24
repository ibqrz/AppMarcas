
using System.Collections.ObjectModel;
using AppMarcas.Models;

namespace AppMarcas
{
    public partial class MainPage : ContentPage
    {
        ObservableCollection<Marca> lista = new ObservableCollection<Marca>();
        
        
        /* ou - trabalha de maneira não assincrona:  
          List<Marca> lista = new List<Marca>(); */

        public MainPage()
        {
            InitializeComponent();

            listMarcas.ItemsSource = lista;
        }

        protected override async void OnAppearing()
        {
           await carregarListaMarcas();
        }

        private async Task carregarListaMarcas()
        {
            List<Marca> tmp = await App.Db.GetAll();

            lista.Clear();

            foreach (Marca marca in tmp) 
            {
                lista.Add(marca);
            }
        }

        private async void btnInserirOnClicked(object sender, EventArgs e)
        {
            Marca mar = new Marca();
            mar.marNome = txtMarNome.Text;

            await App.Db.Insert(mar);
            await DisplayAlert("Sucesso!", "Registro inserido.", "OK");

            OnAppearing();
        }

        private async void txtSearchTextChanged(object sender, TextChangedEventArgs e)
        {
            string q = e.NewTextValue;

            lista.Clear();

            List<Marca> tmp = await App.Db.Search(q);

            foreach (Marca marca in tmp)
            {
                lista.Add(marca);
            }
        }
    }
}
