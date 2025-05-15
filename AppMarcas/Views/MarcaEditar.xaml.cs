
using AppMarcas.Models;

namespace AppMarcas.Views;

public partial class MarcaEditar : ContentPage
{
	public MarcaEditar()
	{
		InitializeComponent();
	}

    private async void btnAlterarOnClicked(object sender, EventArgs e)
    {
        Marca p = new Marca();
        p.marId = int.Parse(txtMarId.Text);
        p.marNome = txtMarNome.Text;

        App.Db.Update(p);
        await DisplayAlert("Atenção", "Registro altereado!", "Ok");
    }
}