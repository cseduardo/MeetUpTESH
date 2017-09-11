using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetUpTESH
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Imagenes : ContentPage
    {
        string uploadedFilename;
        string fileName;
        byte[] imageData;
        public Imagenes()
        {
            InitializeComponent();
        }
        //async void OnDownloadImageButtonClicked(object sender, EventArgs e)
        //{

        //    if (!string.IsNullOrWhiteSpace(uploadedFilename))
        //    {
        //        activityIndicator.IsRunning = true;

        //        var imageData = await AzureStorage.GetFileAsync(ContainerType.Image, uploadedFilename);
        //        imagenesblobl.Source = ImageSource.FromStream(() => new MemoryStream(imageData));

        //        activityIndicator.IsRunning = false;
        //    }
        //}
        async void OnDownloadImageButtonClicked(object sender, EventArgs e)
        {
            var fileList = await AzureStorage.GetFilesListAsync(ContainerType.Image);
            //imagenesblobl.Source = ImageSource.FromStream(() => new MemoryStream(imageData));//agre
            listView.ItemsSource = fileList;
            imagenesblobl.AutomationId= string.Empty;
            //deleteButton.IsEnabled = false;
        }

        async void OnListViewItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            fileName = e.SelectedItem.ToString();
            var byteData = await AzureStorage.GetFileAsync(ContainerType.Image, fileName);
            imagenesblobl.Source = ImageSource.FromStream(() => new MemoryStream(byteData));
            
            //deleteButton.IsEnabled = true;
        }
    }
}