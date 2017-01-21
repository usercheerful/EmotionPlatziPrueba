﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmotionPlatziWebContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public EmotionPlatziWebContext() : base("name=EmotionPlatziWebContext")
        {
            Database.SetInitializer<EmotionPlatziWebContext>(
                // Con esta instruccion decimos que Borre y cree una nueva base de datos siempre que el modelo cambie
                //Recomendable cuando se esta en desarrollo, en produccion no.
                new DropCreateDatabaseIfModelChanges<EmotionPlatziWebContext>());
        }

        public DbSet<EmoPicture> EmoPictures { get; set; }

        public DbSet<EmoFace> EmoFaces { get; set; }

        public DbSet<EmoEmotion> EmoEmotions { get; set; }

        public System.Data.Entity.DbSet<EmotionPlatzi.Web.Models.Home> Homes { get; set; }
    }
}
