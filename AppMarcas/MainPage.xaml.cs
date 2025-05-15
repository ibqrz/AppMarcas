
using System.Collections.ObjectModel;
using AppMarcas.Models;
using AppMarcas.Views;

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

        private async void btnRemover(object sender, EventArgs e)
        {
            MenuItem selecionado = sender as MenuItem;
            Marca p = selecionado.BindingContext as Marca;

            bool confirma = await DisplayAlert("Atenção", "Confirma a remoção?", "Sim", "Não");

            if (confirma == true)
            {
                await App.Db.Delete(p.marId);
                await DisplayAlert("Aviso", "Registro removido!", "Ok");

                await carregarListaMarcas();
            }
        }
        private void btnLimparOnClicked(object sender, EventArgs e)
        {
            txtMarId.Text = String.Empty;
            txtMarNome.Text = String.Empty;
        }

        private void listMarcasItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Marca p = e.SelectedItem as Marca;

            txtMarId.Text = p.marId.ToString();
            txtMarNome.Text = p.marNome.ToString();
        }

        private void btnAlterar(object sender, EventArgs e)
        {
            MenuItem selecionado = sender as MenuItem;
            Marca p = selecionado.BindingContext as Marca;

            Navigation.PushAsync(new Views.MarcaEditar { BindingContext = p });
        }
    }
}
