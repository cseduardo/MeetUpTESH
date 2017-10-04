using Microsoft.ProjectOxford.Vision;
using Microsoft.ProjectOxford.Vision.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeetUpTESH.Class
{
    public class ServiceComputerVision
    {
            static string key = "3edc1097a9a2409295479f279147c60e";

            public static async Task<AnalysisResult> GetDescriptionImage(Stream imageStream)
            {
                VisionServiceClient client = new VisionServiceClient(key);
                VisualFeature[] features =
                {
                VisualFeature.Tags,
                VisualFeature.Categories,
                VisualFeature.Description,
                VisualFeature.Adult,
                VisualFeature.ImageType,
                VisualFeature.Color,
                VisualFeature.Faces
            };

                return await client.AnalyzeImageAsync(imageStream, features.ToList(), null);
            }
    }
}
