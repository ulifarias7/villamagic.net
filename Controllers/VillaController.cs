using AutoMapper;
using Magic.curso.net.Datos;
using Magic.curso.net.modelos;
using Magic.curso.net.modelos.Dtos;//importaciones de dtos
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Magic.curso.net.Controllers
{
    [Route("api/[controller]")]          
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger; //variable privada(readonly) que hace referencia a iloger y citamos la clase que usamos 
        // ILogger : esta interfaz define un conjunti de metodos para regristar info en varias categorias de eventos (como info,errores y advertencias)

        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;


        public VillaController(ILogger<VillaController> logger ,ApplicationDbContext db,IMapper mapper) //contructor para services de logger
        {

            _logger = logger;
            _db = db;
            _mapper = mapper;
       
        
        }





        //empoint (metodos)

        [HttpGet("GetVillas")]
        public async Task< ActionResult<IEnumerable<VillaDtos>>> GetVillas() //metodo asincrono 
        {
            _logger.LogInformation("obtener las villas ");
            IEnumerable<Villa> villalist = await _db.villas.ToListAsync();// se esta forma es como que estariamos haciendo un ""Select * from villa ; "
            return Ok(_mapper.Map<IEnumerable<VillaDtos>>(villalist)); //aplicamos lo del mapper y retorna un inumerable de tipo villa dtos y trae datos de mi villalist
        }






        [HttpGet("ID:int", Name = "GetVilla")] //este metodo nos va a traer la lista q se encuentre en el id que estamos buscando (devuelve un solo object)
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<ActionResult<VillaDtos>> GetVilla(int ID)

        {
            if (ID == 0)
            {
                _logger.LogError("error al traer villa con Id" + ID );
                return BadRequest();//es cuando se hace una mala solicitud , te tira un statuscode.
            }

           // var villa = VillaStore.villalist.FirstOrDefault(v => v.ID == ID);
           var villa = await _db.villas.FirstOrDefaultAsync(v => v.ID == ID);
            if (villa == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<VillaDtos>(villa));

        }






        [HttpPost("CrearVilla")] //se agrega la nueva villa
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]


        //frombody : nos indica que vamos a recibir datos  y los datos que vamos a recibir es el de villadtos
        public async Task< ActionResult<VillaDtos>> Crearvilla([FromBody] VillaCreateDtos CreateDtos)
        {
            if (!ModelState.IsValid) //! el signo es por si no esta valido
            {
                return BadRequest(ModelState);
            }

            //validacion personalizada 
            if (await _db.villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == CreateDtos.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "la Villa con ese Nombre existe!");
                return BadRequest(ModelState);
            }

            if (CreateDtos == null) //verificamos si nos esta mandando datos 
            {
                return BadRequest(CreateDtos);
            }

            //if (villaDtos.ID > 0) //si hay un error interno que salte ese estatu internal server
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}
            //villaDtos.ID = VillaStore.villalist.OrderByDescending(v => v.ID).FirstOrDefault().ID + 1;
            //VillaStore.villalist.Add(villaDtos);

            Villa modelo = _mapper.Map<Villa>(CreateDtos);

            //Villa modelo = new()
            //{
            //    //no se le agrega di por q se autoincrementa de manera automatica.
            //    Nombre = CreateDtos.Nombre,
            //    Detalle = CreateDtos.Detalle,
            //    Tarifa = CreateDtos.Tarifa,
            //    Ocupante = CreateDtos.Ocupante,
            //    MetrosCuadrados = CreateDtos.MetrosCuadrados,
            //    ImagenUrl = CreateDtos.ImagenUrl,
            //    Amenidad = CreateDtos.Amenidad

            //};

            await _db.villas.AddAsync(modelo);
            await _db.SaveChangesAsync(); // para que los cambios se vean reflejados en la base de datos


            return CreatedAtRoute("GetVilla", new { id = modelo.ID }, modelo);
        }





        [HttpDelete("{ID:int}")] // se elimina la villa por ID
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task <IActionResult>Delete(int ID)
        {
            if (ID == 0)
            {
                return BadRequest();
            }
            var villa = await _db.villas.FirstOrDefaultAsync(v => v.ID == ID);
            if (villa != null)
            {
                return NotFound();
            }
            //VillaStore.villalist.Remove(villa);
            _db.villas.Remove(villa);
            await _db.SaveChangesAsync();
            return NoContent();
        }



        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
       
        public async Task<IActionResult> UpdateVilla(int ID, [FromBody] VillaUpdateDtos UpdateDtos) 
        { 
            if(UpdateDtos==null || ID!= UpdateDtos.ID) //si el objeto que recibe villadtos lo recibe nulo(osea nada) y podemos complementarlo con las || y el id que estoy recibiendo es diferrente al de villadtos

            {
                return BadRequest();
            }

            //var villa = VillaStore.villalist.FirstOrDefault(v => v.ID == ID);
            //villa.Nombre = villaDtos.Nombre;
            //villa.Ocupantes = villaDtos.Ocupantes;
            //villa.MetrosCuadrados = villaDtos.MetrosCuadrados;
            //return NoContent();

            Villa modelo = _mapper.Map<Villa>(UpdateDtos);
                

            //Villa modelo = new Villa()
            //{
            //    ID = UpdateDtos.ID,
            //    Nombre = UpdateDtos.Nombre,
            //    Detalle = UpdateDtos.Detalle,
            //    Tarifa = UpdateDtos.Tarifa,
            //    Ocupante = UpdateDtos.Ocupante,
            //    MetrosCuadrados=UpdateDtos.MetrosCuadrados,
            //    ImagenUrl = UpdateDtos.ImagenUrl,
            //    Amenidad = UpdateDtos.Amenidad

            //};

            _db.villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent() ;
        }


        [HttpPatch("{id:int}")] // se utiliza el metodo para cambiar una sola propiedad que en este caso seria el ID 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task< IActionResult> UpdatePartialVilla(int ID, JsonPatchDocument<VillaUpdateDtos> PatchDtos)
        {
            if (PatchDtos == null || ID == 0)

            {
                return BadRequest();
            }
            var villa = await _db.villas.AsNoTracking().FirstOrDefaultAsync(v => v.ID == ID); // asnotracking me permite consultar un registro del dbcontext
            VillaUpdateDtos villaDtos = _mapper.Map<VillaUpdateDtos>(villa);
            
            //VillaUpdateDtos villaDtos = new()
            //{
            //    ID = villa.ID,
            //    Nombre = villa.Nombre,
            //    Detalle = villa.Detalle,
            //    Tarifa = villa.Tarifa,
            //    Ocupante = villa.Ocupante,
            //    MetrosCuadrados = villa.MetrosCuadrados,
            //    ImagenUrl = villa.ImagenUrl,
            //    Amenidad = villa.Amenidad

            //};

            if (villa == null ) return BadRequest();




            PatchDtos.ApplyTo(villaDtos,ModelState);

            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);// le mandamos el model states para q nos ubique donde esta el error

            }

            Villa modelo = _mapper.Map<Villa>(villaDtos);

            //Villa modelo = new()
            //{
            //    ID = villaDtos.ID,
            //    Nombre = villaDtos.Nombre,
            //    Detalle = villaDtos.Detalle,
            //    Tarifa = villaDtos.Tarifa,
            //    Ocupante = villaDtos.Ocupante,
            //    MetrosCuadrados = villaDtos.MetrosCuadrados,
            //    ImagenUrl = villaDtos.ImagenUrl,
            //    Amenidad = villaDtos.Amenidad

            //};

            _db.villas.Update(modelo);
            await _db.SaveChangesAsync();
            return NoContent();
        }

    }
}
                   

// minuto en que me quede del video 3:00:29