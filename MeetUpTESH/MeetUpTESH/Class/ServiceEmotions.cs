using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion;
using System.IO;

namespace MeetUpTESH.Class
{
    class ServiceEmotions
    {
        static string key = "76c25ce68b5b47269f2592261457b075"; //Aqui se va aingresar la lleve que se obtuvo de la activacion de los servicios cognitivos en Azure
        public static async Task<Dictionary<string, float>> GetEmotions(Stream stream)
        {
            EmotionServiceClient cliente = new EmotionServiceClient(key);
            var emotions = await cliente.RecognizeAsync(stream);

            if (emotions == null || emotions.Count() == 0)
                return null;

            return emotions[0].Scores.ToRankedList().ToDictionary(x => x.Key, x => x.Value);
        }
    }
}
