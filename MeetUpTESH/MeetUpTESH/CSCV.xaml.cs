using MeetUpTESH.Class;
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
    public partial class CSCV : ContentPage
    {
        static Stream streamCopy;
        public CSCV()
        {
            InitializeComponent();
        }
        async void btnPicture_Clicked(object sender, EventArgs e)
        {
            var useCam = ((Button)sender).Text.Contains("Cámara");

            var file = await ServiceImage.TakePicture(useCam);
            imgPicture.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                streamCopy = new MemoryStream();
                stream.CopyTo(streamCopy);
                stream.Seek(0, SeekOrigin.Begin);
                file.Dispose();
                return stream;
            });
        }
        async void btnAnalysisImage_Clicked(object sender, EventArgs e)
        {
            if (streamCopy != null)
            {
                streamCopy.Seek(0, SeekOrigin.Begin);
                var vision = await ServiceComputerVision.GetDescriptionImage(streamCopy);

                var adult = vision.Adult;
                lblAdult.Text = String.Format("Contenido Adulto: {0} ({1})", adult.IsAdultContent, adult.AdultScore.ToString("P4"));
                lblRacist.Text = String.Format("Contenido Racista: {0} ({1})", adult.IsRacyContent, adult.RacyScore.ToString("P4"));

                var categorys = vision.Categories;
                lblCategorys.Text = "Categorias: ";
                foreach (var cat in categorys.ToList())
                {
                    lblCategorys.Text = lblCategorys.Text + String.Format("{0} ({1}), ", cat.Name, cat.Score.ToString("P4"));
                }

                var color = vision.Color;
                lblColor.Text = String.Format("Accent Color: {0}\nColor dominante:\nFondo: {1}\tFrente: {2}\n¿Es Blanco y Negro? {3}\nColores dominantes: ",
                    color.AccentColor, color.DominantColorBackground,
                    color.DominantColorForeground, color.IsBWImg);

                foreach (var x in color.DominantColors.ToList())
                { lblColor.Text = lblColor.Text + x + ", "; }

                var description = vision.Description;
                lblTags.Text = "Tags: ";
                lblCaptions.Text = "Captions: ";

                foreach (var tag in vision.Description.Tags.ToList())
                { lblTags.Text = lblTags.Text + tag + ", "; }

                //vision.Description.Captions.ToList().ForEach(cap => lblCaptions.Text = lblCaptions.Text + String.Format("{0} ({1}), ", cap.Text, cap.Confidence.ToString("P4")));
                foreach (var cap in vision.Description.Captions.ToList())
                {
                    lblCaptions.Text = lblCaptions.Text + String.Format("{0} ({1}), ", cap.Text, cap.Confidence.ToString("P4"));
                }

                var faces = vision.Faces;
                lblFace.Text = "Caras: ";
                //caras.ToList().ForEach(cara => lblCaras.Text = lblCaras.Text + String.Format("{0} ({1}), ", cara.Gender, cara.Age));
                foreach (var face in faces.ToList())
                {
                    lblFace.Text = lblFace.Text + String.Format("{0} ({1}), ", face.Gender, face.Age);
                }

                var tags = vision.Tags;
                lblTags2.Text = "Tags 2: ";

                //tags.ToList().ForEach(tag => lblTags2.Text = lblTags2.Text + String.Format("{0} - {1} ({2}), ", tag.Name, tag.Hint, tag.Confidence.ToString("P4")));

                foreach (var tag in tags.ToList())
                {
                    lblTags2.Text = lblTags2.Text + String.Format("{0} - {1} ({2}), ", tag.Name, tag.Hint, tag.Confidence.ToString("P4"));
                }
            }
        }
    }
}