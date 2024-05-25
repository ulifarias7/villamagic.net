using Microsoft.EntityFrameworkCore;

namespace Magic.curso.net.modelos.Dtos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) :base(options) //mandamos la configuracion por medio de configuracion de dependencia lo que tengamos en el servicio
        {

        }

        public DbSet<Villa> villas {  get; set; }  //modelos q se crean en la base de datos (con todos los atributos de la clase VIlla)


        protected override void OnModelCreating(ModelBuilder modelBuilder)//onmodelcreating : se utiliza para configurar el modelo de datos
        {
            modelBuilder.Entity<Villa>().HasData( // configurar datos de la base de datos
                new Villa()
                {
                    ID = 1,
                    Nombre = "villa real",
                    Detalle = "detalle de la villa ",
                    ImagenUrl = "",
                    Ocupante = 5,
                    MetrosCuadrados = 50,
                    Tarifa = 200,
                    Amenidad = "",
                    Fechacreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                },
                new Villa()
                {
                    ID = 2,
                    Nombre = "villa premiun vista a la piscina",
                    Detalle = "detalle de la villa con vista a la piscina",
                    ImagenUrl = "",
                    Ocupante = 4,
                    MetrosCuadrados = 40,
                    Tarifa = 250,
                    Amenidad = "",
                    Fechacreacion = DateTime.Now,
                    FechaActualizacion = DateTime.Now,
                }
                );
        }

    }
}














//PM> add-migration AgregarBaseDatos :  se utuliza para crear el modelo de atributos de tu clase de entidades en la base de datos si es q no la tenes realizada ,
//te crea un directorio llamado (migration y el archivo con la estructura de la base de datos (tabla))

//para q se ejecute ese scrips donde aparece la estructura de la base de datos y se vea actualizada o reflejada en el sqlserver se hace este comando "update-databese", si salta el error q no es confiable el servidor TrustServerCertificate=true