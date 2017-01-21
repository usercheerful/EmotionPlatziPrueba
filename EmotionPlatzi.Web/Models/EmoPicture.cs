using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmoPicture
    {
        public int Id { get; set; }

        [Display(Name="Nombre")] //Cambiamos el texto que se mostraria en el formulario

        public string Name { get; set; }

       /* [Required]
        [MaxLength(10)] //Validamos que el tamaño maximo del Path*/
        //[MaxLength(10,ErrorMessage ="La ruta supera el tamaño establecido")] //Se comento por error que genera despues de implementacion, verificar para corregir
        public string Path { get; set; }

        //propiedades de navegacion: propiedades que estan en el modelo 
        //pero no equivalen a un cambpo real, Entity lo toma para determinar la 
        //relacion entre una tabla y otra -> Para ello marcar como VIRTUAL

        //aqui hay una relacion de uno a muchos -> una fotografia puede tener varios
        //rostros(por ello se marca como coleccion)
        public virtual ObservableCollection<EmoFace> Faces { get; set; }
    }
}