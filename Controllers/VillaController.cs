using Magic.curso.net.Datos;
using Magic.curso.net.modelos;
using Magic.curso.net.modelos.Dtos;//importaciones de dtos
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Magic.curso.net.Controllers
{
    [Route("api/[controller]")]          
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger; //variable privada(readonly) que hace referencia a iloger y citamos la clase que usamos 
        // ILogger : esta interfaz define un conjunti de metodos para regristar info en varias categorias de eventos (como info,errores y advertencias)

        private readonly ApplicationDbContext _db;
        public VillaController(ILogger<VillaController> _logger ,ApplicationDbContext _db) //contructor para services de logger
        {

            _logger = _logger;
            _db = _db;
       
        
        }





        //empoint (metodos)

        [HttpGet("GetVillas")]
        public ActionResult<IEnumerable<VillaDtos>> GetVillas()
        {
            _logger.LogInformation("obtener las villas ");
            return Ok(_db.villas.ToList()); // se esta forma es como que estariamos haciendo un ""Select * from villa ; " 

        }





        [HttpGet("ID:int", Name = "GetVilla")] //este metodo nos va a traer la lista q se encuentre en el id que estamos buscando (devuelve un solo object)
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public ActionResult<VillaDtos> GetVilla(int ID)

        {
            if (ID == 0)
            {
                _logger.LogError("error al traer villa con Id" + ID );
                return BadRequest();//es cuando se hace una mala solicitud , te tira un statuscode.
            }

           // var villa = VillaStore.villalist.FirstOrDefault(v => v.ID == ID);
           var villa = _db.villas.FirstOrDefault(v => v.ID == ID);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(villa);

        }





        [HttpPost("CrearVilla")] //se agrega la nueva villa
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        //frombody : nos indica que vamos a recibir datos  y los datos que vamos a recibir es el de villadtos
        public ActionResult<VillaDtos> Crearvilla([FromBody] VillaDtos villaDtos)
        {
            if (!ModelState.IsValid) //! el signo es por si no esta valido
            {
                return BadRequest(ModelState);
            }

            //validacion personalizada 
            if (_db.villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDtos.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "la Villa con ese Nombre existe!");
                return BadRequest(ModelState);
            }

            if (villaDtos == null) //verificamos si nos esta mandando datos 
            {
                return BadRequest(villaDtos);
            }

            if (villaDtos.ID > 0) //si hay un error interno que salte ese estatu internal server
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            //villaDtos.ID = VillaStore.villalist.OrderByDescending(v => v.ID).FirstOrDefault().ID + 1;
            //VillaStore.villalist.Add(villaDtos);

            Villa modelo = new()
            {
                //no se le agrega di por q se autoincrementa de manera automatica.
                Nombre = villaDtos.Nombre,
                Detalle = villaDtos.Detalle,
                Tarifa = villaDtos.Tarifa,
                Ocupante = villaDtos.Ocupante,
                MetrosCuadrados = villaDtos.MetrosCuadrados,
                ImagenUrl = villaDtos.ImagenUrl,
                Amenidad = villaDtos.Amenidad

            };

            _db.villas.Add(modelo);
            _db.SaveChanges(); // para que los cambios se vean reflejados en la base de datos


            return CreatedAtRoute("GetVilla", new { id = villaDtos.ID }, villaDtos);
        }





        [HttpDelete("{ID:int}")] // se elimina la villa por ID
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public IActionResult Delete(int ID)
        {
            if (ID == 0)
            {
                return BadRequest();
            }
            var villa = _db.villas.FirstOrDefault(v => v.ID == ID);
            if (villa != null)
            {
                return NotFound();
            }
            //VillaStore.villalist.Remove(villa);
            _db.villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public IActionResult UpdateVilla(int ID, [FromBody] VillaDtos villaDtos) 
        { 
            if(villaDtos==null || ID!= villaDtos.ID) //si el objeto que recibe villadtos lo recibe nulo(osea nada) y podemos complementarlo con las || y el id que estoy recibiendo es diferrente al de villadtos

            {
                return BadRequest();
            }

            //var villa = VillaStore.villalist.FirstOrDefault(v => v.ID == ID);
            //villa.Nombre = villaDtos.Nombre;
            //villa.Ocupantes = villaDtos.Ocupantes;
            //villa.MetrosCuadrados = villaDtos.MetrosCuadrados;
            //return NoContent();

            Villa modelo = new Villa()
            {
                ID = villaDtos.ID,
                Nombre = villaDtos.Nombre,
                Detalle = villaDtos.Detalle,
                Tarifa = villaDtos.Tarifa,
                Ocupante = villaDtos.Ocupante,
                MetrosCuadrados=villaDtos.MetrosCuadrados,
                ImagenUrl = villaDtos.ImagenUrl,
                Amenidad = villaDtos.Amenidad

            };

            _db.villas.Update(modelo);
            _db.SaveChanges();
            return NoContent() ;
        }


        [HttpPatch("{id:int}")] // se utiliza el metodo para cambiar una sola propiedad que en este caso seria el ID 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialVilla(int ID, JsonPatchDocument<VillaDtos> PatchDtos)
        {
            if (PatchDtos == null || ID == 0)

            {
                return BadRequest();
            }
            var villa = _db.villas.FirstOrDefault(v => v.ID == ID);
            VillaDtos villaDtos = new()
            {
                ID = villa.ID,
                Nombre = villa.Nombre,
                Detalle = villa.Detalle,
                Tarifa = villa.Tarifa,
                Ocupante = villa.Ocupante,
                MetrosCuadrados = villa.MetrosCuadrados,
                ImagenUrl = villa.ImagenUrl,
                Amenidad = villa.Amenidad

            };

            if (villa == null ) return BadRequest();




            PatchDtos.ApplyTo(villaDtos,ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);// le mandamos el model states para q nos ubique donde esta el error

            }

            Villa modelo = new()
            {
                ID = villaDtos.ID,
                Nombre = villaDtos.Nombre,
                Detalle = villaDtos.Detalle,
                Tarifa = villaDtos.Tarifa,
                Ocupante = villaDtos.Ocupante,
                MetrosCuadrados = villaDtos.MetrosCuadrados,
                ImagenUrl = villaDtos.ImagenUrl,
                Amenidad = villaDtos.Amenidad

            };

            _db.villas.Update(modelo);
            _db.SaveChanges();
            return NoContent();
        }

    }
}
                                                                                                