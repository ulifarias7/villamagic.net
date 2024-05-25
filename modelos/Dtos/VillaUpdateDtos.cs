using System.ComponentModel.DataAnnotations;
namespace Magic.curso.net.modelos.Dtos
{
    public class VillaUpdate
    {
        [Required]
        public int ID { get; set; }

        [Required]
        [MaxLength(30)]//un maximo de 30 caracteres
        public string Nombre { get; set; }

        public string Detalle {  get; set; }
        [Required]
        public double Tarifa { get; set; }
        
        [Required]
        public int Ocupante { get; set; }

        [Required]
        public int MetrosCuadrados { get; set; }

        [Required]
        public string ImagenUrl { get; set; }

        public string Amenidad { get; set; }

    }
}
