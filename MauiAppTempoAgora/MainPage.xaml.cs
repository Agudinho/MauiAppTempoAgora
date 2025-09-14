using MauiAppTempoAgora.Models;
using MauiAppTempoAgora.Services;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MauiAppTempoAgora
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_cidade.Text))
                {
                    Tempo? t = await DataService.GetPrevisao(txt_cidade.Text);

                    if (t != null)
                    {
                        string dados_previsao = "";

                        dados_previsao =
                                         $"Descrição: {t.description}\n " +
                                         $"Velocidade do vento: {t.speed}\n " +
                                         $"Visibilidade: {t.visibility}\n" +
                                         $"Latitude: {t.lat}\n" +
                                         $"Longitude: {t.lon}\n" +
                                         $"Nascer do sol: {t.sunrise}\n" +
                                         $"Por do sol: {t.sunset}\n" +
                                         $"Temp máx: {t.temp_max}\n" +
                                         $"Temp min: {t.temp_min}\n";




                        lbl_res.Text = dados_previsao;

                    }
                    else
                    {
                        lbl_res.Text = "Sem dados de previsão";

                        await DisplayAlert("Cidade não encontradaa", "Não foi possível localizar a cidade digitada", "OK");
                    }
                }
                else
                {
                    lbl_res.Text = "Preencha a cidade";
                }
            }
            catch (HttpRequestException)
            {
                await DisplayAlert("Erro de conexão", "Verifique sua internet e tente novamente", "OK");
            }
            catch(Exception ex) 
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }

}
