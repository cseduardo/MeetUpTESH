using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetUpTESH.Class;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;

namespace MeetUpTESH
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CSE : ContentPage
	{
        static Stream streamCopy;
        public CSE ()
		{
			InitializeComponent ();
		}
        async void btnPicture_Clicked(object sender, EventArgs e) //crea un evento asincrono y vacio
        {
            var useCam = ((Button)sender).Text.Contains("Cámara"); //crea la variable useCam va a enviar datos y va a contener el texto "Camara"
            var file = await ServiceImage.TakePicture(useCam); // crea una variable para guardar la foto que se está tomando y está contenida en useCam
            Results.Children.Clear();
            lblResult.Text = "---";

            imgPicture.Source = ImageSource.FromStream(() => {
                var stream = file.GetStream();
                streamCopy = new MemoryStream();
                stream.CopyTo(streamCopy);
                stream.Seek(0, SeekOrigin.Begin);
                file.Dispose();
                return stream;
            });
        }

        async void btnAnalysisEmotions_Clicked(object sender, EventArgs e)
        {
            if (streamCopy != null)
            {
                streamCopy.Seek(0, SeekOrigin.Begin);
                var emotions = await ServiceEmotions.GetEmotions(streamCopy);

                if (emotions != null)
                {
                    lblResult.Text = "---Análisis de Emociones---";
                    DrawResults(emotions);
                }
                else lblResult.Text = "---No se detectó una cara---";
            }
            else lblResult.Text = "---No has seleccionado una imagen---";
        }

        void DrawResults(Dictionary<string, float> emotions)
        {
            Results.Children.Clear();

            foreach (var emotion in emotions)
            {
                Label lblEmotion = new Label()
                {
                    Text = emotion.Key,
                    TextColor = Color.Blue,
                    WidthRequest = 90
                };

                BoxView box = new BoxView()
                {
                    Color = Color.Lime,
                    WidthRequest = 150 * emotion.Value,
                    HeightRequest = 30,
                    HorizontalOptions = LayoutOptions.StartAndExpand
                };

                Label lblPercent = new Label()
                {
                    Text = emotion.Value.ToString("P4"),
                    TextColor = Color.Maroon
                };

                StackLayout stack = new StackLayout()
                {
                    Orientation = StackOrientation.Horizontal
                };

                stack.Children.Add(lblEmotion);
                stack.Children.Add(box);
                stack.Children.Add(lblPercent);

                Results.Children.Add(stack);
            }
        }
    }
}