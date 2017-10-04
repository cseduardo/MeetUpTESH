using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MeetUpTESH;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MeetUpTESH
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoMaster : ContentPage
    {
        public ListView ListView { get { return listView; } }

        public TodoMaster()
        {
            InitializeComponent();
            //BindingContext = new TodoMasterViewModel();
            var masterPageItems = new List<TodoMenuItem>();
            masterPageItems.Add(new TodoMenuItem
            {
                Title = "Imágenes",
                //IconSource = "contacts.png",
                TargetType = typeof(Imagenes)
            });
            //masterPageItems.Add(new TodoMenuItem
            //{
            //    Title = "Videos",
            //    //IconSource = "todo.png",
            //    TargetType = typeof(Videos)
            //});
            //if (Device.OS != TargetPlatform.Windows)
            //{
                masterPageItems.Add(new TodoMenuItem
                {
                    Title = "Fotos",
                    //IconSource = "todo.png",
                    TargetType = typeof(Camara_dos)
                });
            //}
            masterPageItems.Add(new TodoMenuItem
            {
                Title = "Emociones",
                //IconSource = "reminders.png",
                TargetType = typeof(CSE)
            });
            masterPageItems.Add(new TodoMenuItem
            {
                Title = "Computer Vision",
                //IconSource = "reminders.png",
                TargetType = typeof(CSCV)
            });
            masterPageItems.Add(new TodoMenuItem
            {
                Title = "Otros",
                //IconSource = "reminders.png",
                TargetType = typeof(Otros)
            });
            masterPageItems.Add(new TodoMenuItem
            {
                Title = "Acerca de",
                //IconSource = "reminders.png",
                TargetType = typeof(Acerca)
            });
            listView.ItemsSource = masterPageItems;
        }
    }
}