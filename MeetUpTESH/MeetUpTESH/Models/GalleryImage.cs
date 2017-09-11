using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MvvmHelpers;
using Xamarin.Forms;

namespace MeetUpTESH.Models
{
    public class GalleryImage : ObservableObject
    {
        public GalleryImage()
        {
            ImageId = Guid.NewGuid();
        }

        public Guid ImageId
        {
            get;
            set;
        }

        public ImageSource Source
        {
            get;
            set;
        }

        public byte[] OrgImage
        {
            get;
            set;
        }

    }
}
