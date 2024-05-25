using Magic.curso.net.modelos.Dtos;

namespace Magic.curso.net.Datos
{
    public  static class VillaStore
    {
        public static List<VillaUpdateDtos> villalist = new List<VillaUpdateDtos>
        {
            new VillaUpdateDtos{ID =1, Nombre="vista a la piscina",Ocupantes=3, MetrosCuadrados=50},
            new VillaUpdateDtos{ID =2, Nombre="vista a la playa",Ocupantes=3, MetrosCuadrados=80}
            
        };
    }
}
