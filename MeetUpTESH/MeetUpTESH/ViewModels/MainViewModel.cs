﻿using MeetUpTESH.Models;
using MeetUpTESH.Services;
using MvvmHelpers;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MeetUpTESH.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        ICommand _cameraCommand, _previewImageCommand = null;
        ObservableCollection<GalleryImage> _images = new ObservableCollection<GalleryImage>();
        ImageSource _previewImage = null;
        string uploadedFilename;

        public MainViewModel()
        {
        }

        public ObservableCollection<GalleryImage> Images
        {
            get { return _images; }
        }

        public ImageSource PreviewImage
        {
            get { return _previewImage; }
            set
            {
                SetProperty(ref _previewImage, value);
            }
        }

        public ICommand CameraCommand
        {
            get { return _cameraCommand ?? new Command(async () => await ExecuteCameraCommand(), () => CanExecuteCameraCommand()); }
        }

        public bool CanExecuteCameraCommand()
        {
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                return false;
            }
            return true;
        }

        public async Task ExecuteCameraCommand()
        {
            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions { PhotoSize = PhotoSize.Small });

            if (file == null)
                return;


            byte[] imageAsBytes = null;
            using (var memoryStream = new MemoryStream())
            {
                
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                imageAsBytes = memoryStream.ToArray();
            }

            var resizer = DependencyService.Get<IImageResize>();

            imageAsBytes = resizer.ResizeImage(imageAsBytes, 1080, 1080);

            var imageSource = ImageSource.FromStream(() => new MemoryStream(imageAsBytes));

            _images.Add(new GalleryImage { Source = imageSource, OrgImage = imageAsBytes });

            //activityIndicator.IsRunning = true;

            uploadedFilename = await AzureStorage.UploadFileAsync(ContainerType.Image, new MemoryStream(imageAsBytes));

            //uploadButton.IsEnabled = false;
            //downloadButton.IsEnabled = true;
            //activityIndicator.IsRunning = false;
            return;
        }

        public ICommand PreviewImageCommand
        {
            get
            {
                return _previewImageCommand ?? new Command<Guid>((img) => {

                    var image = _images.Single(x => x.ImageId == img).OrgImage;

                    PreviewImage = ImageSource.FromStream(() => new MemoryStream(image));

                });
            }
        }
    }
}
