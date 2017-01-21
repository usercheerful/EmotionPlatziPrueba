using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmoFace
    {
        //Atributos referentes a los rostros en la fotografia
        public int Id { get; set; }
        public int EmoPictureId { get; set; }
        #region
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #endregion

        //Cada face pertenece a un Picture
        public virtual EmoPicture Picture { get; set; }

        //Cada face puede tener un conjunto de emociones,cada una con alguna probabilidad
        public virtual ObservableCollection<EmoEmotion> Emotions { get; set; }
    }
}