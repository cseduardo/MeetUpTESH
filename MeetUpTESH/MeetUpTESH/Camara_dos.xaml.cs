using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetUpTESH
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Camara_dos : ContentPage
    {
        string uploadedFilename;
        byte[] byteData;
        public Camara_dos()
        {
            InitializeComponent();
        }

         private async void tomarFotoOnClicked(object sender, EventArgs e)
         {
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            var optionstorage = new StoreCameraMediaOptions()
            {
                SaveToAlbum = true,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
                Directory = "MeetUps",
                Name = "MeetUp"+ timestamp +".jpg"
            };
            var foto = await CrossMedia.Current.TakePhotoAsync(optionstorage);
            image.Source = ImageSource.FromStream(()=>
            {
                var stream = foto.GetStream();
                foto.Dispose();
                return stream;
            });

            //byteData = Convert.ToByteArray();
            //uploadedFilename = await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(byteData));
            //if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            //{
            //    await DisplayAlert("No Camara", ":( Camara No Disponible.", "OK");
            //    return;
            //}
            //var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);
            //var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions()
            //{

            //    PhotoSize = Plugin.Media.Abstractions.PhotoSize.Full,
            //    Directory = "MeetUps",
            //    Name = "MeetUp"+ timestamp +".jpg"
            //});
            //if (file == null)
            //    return;

            //await DisplayAlert("File Location", file.Path, "OK");

            //image.Source = ImageSource.FromStream(() =>
            //{
            //    var stream = file.GetStream();
            //    file.Dispose();
            //    return stream;
            //});
        }
    }
}