using EmotionPlatzi.Web.Models;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Collections.ObjectModel;

using System.Reflection;
using System.Threading.Tasks;

namespace EmotionPlatzi.Web.Util
{
    public class EmotionHelper
    {

        public EmotionServiceClient emoClient;
        public EmotionHelper(string key)
        {
            //inicializamos el ServiceClient
            emoClient = new EmotionServiceClient(key);
        }

        public async Task<EmoPicture> DetectAndExtractFacesAsync(Stream imageStream)
        {
            //traemos un array de emociones
           Emotion[] emotions= await emoClient.RecognizeAsync(imageStream);

            var emoPicture = new EmoPicture();
            emoPicture.Faces = ExtractFaces(emotions, emoPicture);

            return emoPicture;
        }

        private ObservableCollection<EmoFace> ExtractFaces(Emotion[] emotions, EmoPicture emoPicture)
        {
            //ObservableCollection es un tipo de coleccion especial q emite notificaciones
            //cada vez q se agrega o se eliminan miembros, eso le sirve al Entity Framework
            //para realizar disparos de procedimientos actualizacion o borrado en cascada
            var listaFaces = new ObservableCollection<EmoFace>();

            foreach (var emotion in emotions)
            {
                var emoface = new EmoFace()
                {
                    X = emotion.FaceRectangle.Left,
                    Y = emotion.FaceRectangle.Top,
                    Width = emotion.FaceRectangle.Width,
                    Height = emotion.FaceRectangle.Height,
                    Picture = emoPicture
                };

                emoface.Emotions = ProcessEmotion(emotion.Scores, emoface);

                listaFaces.Add(emoface);
            }

            return listaFaces;
        }

        private ObservableCollection<EmoEmotion> ProcessEmotion(Scores scores,EmoFace emoface)
        {
            var emotionList = new ObservableCollection<EmoEmotion>();
            //Leemos dinamicamente el contenido de scores, leemos las propiedades que sean publicas y q sean parte de una instancia
            var properties = scores.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            //traemos solo las propiedades de tipo float

            var filterProperty = from p in properties
                                 where p.PropertyType == typeof(float)
                                 select p;
            //OTRA FORMA DE EXPRESAR LA LINEA ANTERIOR
            //var filterProperty = properties.Where(p => p.PropertyType == typeof(float));

            var emotype = EmoEmotionEnum.Undetermined;

            foreach (var prop in filterProperty)
            {
                //obtenemos el nombre de la propiedad y lo convertimos al emotype
                if (!Enum.TryParse<EmoEmotionEnum>(prop.Name, out emotype))
                {
                    emotype= EmoEmotionEnum.Undetermined;
                }
                
                var emoEmotion = new EmoEmotion();
                emoEmotion.Score = (float)prop.GetValue(scores);
                emoEmotion.EmotionType = emotype;
                emoEmotion.Face = emoface;
                emotionList.Add(emoEmotion);
            }

            return emotionList;
        }
    }
}