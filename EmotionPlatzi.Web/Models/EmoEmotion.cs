using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmoEmotion
    {
        public int Id { get; set; }
        public float Score { get; set; } //probabilidad de una emocion
        public int EmoFaceId { get; set; } //esta emocion pertenece a una cara

        public EmoEmotionEnum EmotionType { get; set; }

        public virtual EmoFace Face { get; set; }
    }
}