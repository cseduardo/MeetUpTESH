using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media.Abstractions;
using System.Globalization;
using Plugin.Media;

namespace MeetUpTESH.Class
{
    public class ServiceImage
    {
        public static async Task<MediaFile> TakePicture(bool useCam)//se crea un metodo publico estatico y asincrono y va a retornar un valor falso o verdadero a la variable useCam
        {
            await CrossMedia.Current.Initialize(); //como el metodo creado es asincrono se tiene que utlizar el await y lo q va hacer es inicializar la camara

            if (useCam)
            {
                //un solo signo de admiracion es una negacion
                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) //si la camara no está disponible o diferente de que no puede tomar una foto (en general lo que hace es verificar q esté disponible la camara)
                {
                    return null; //que regrese un valor nulo
                }
            }
            var timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss", CultureInfo.InvariantCulture);//se crea una variable llamada timestamp para que las fotos que se tomen no se vayan a reescribir y tengan diferente nombre utilizando la hora y la fecha
            var file = useCam //crea una variable llamada useCam y va hacer un dato nulo hasta que se tome una foto, con el signo de interrogacion indica lo del dato nulo
                ? await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions //se indica que cuando se tome la foto la va a guardar en cierto lugar del dispositivo con un nombre
                {
                    Directory = "Cognitive", //está es la carpeta que va a crear y donde va a guardar la foto tomada
                    Name = "Emociones "+timestamp+".jpg" //va a tomar el nombre q le digamos nosotros
                })
                : await CrossMedia.Current.PickPhotoAsync();//en esta variable vamos a guardar las fotos que ya tenemos y/o que tomamos antes para mostrarlas

            return file;
        }
    }
}
