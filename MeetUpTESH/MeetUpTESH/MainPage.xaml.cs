using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms; 

namespace MeetUpTESH
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        async void invitadoClicked(object sender, EventArgs args)
        {
            if (Plugin.Connectivity.CrossConnectivity.Current.IsConnected)
            {
                await DisplayAlert("", "Conectado", "OK");
                await Navigation.PushModalAsync(new Todo());
                return;
            }
            else
            {
                await DisplayAlert("", "No estas conectado", "OK");
            }

        }
    }
}
