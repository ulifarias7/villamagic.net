using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magic.curso.net.modelos
{
    public class Villa // esta clase es como la representacion de la base de datos 

    {
        [Key]  // este seria el primery key de la tabla de la base de datos 
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // da a entender que se va a incrementar de a uno en nuestra base de datos

        public int ID { get; set; }
        public string  Nombre { get; set; }

        public string Detalle{ get; set; }

        [Required]

        public double Tarifa { get; set; }


        public int Ocupante { get; set; }

        public int MetrosCuadrados { get; set; }

        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }
        public DateTime Fechacreacion { get; set; }

        public DateTime FechaActualizacion { get; set; }

    }
}
