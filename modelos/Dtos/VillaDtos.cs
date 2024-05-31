using System.ComponentModel.DataAnnotations;
namespace Magic.curso.net.modelos.Dtos
{
    public class VillaDtos
    {
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]//un maximo de 30 caracteres
        public string Nombre { get; set; }

        public string Detalle {  get; set; }
        [Required]
        public double Tarifa { get; set; }

        public int Ocupante { get; set; }
        public int MetrosCuadrados { get; set; }
        
        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }

    }
}
