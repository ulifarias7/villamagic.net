using Magic.curso.net.modelos.Dtos;

namespace Magic.curso.net.Datos
{
    public  static class VillaStore
    {
        public static List<VillaDtos> villalist = new List<VillaDtos>
        {
            new VillaDtos{ID =1, Nombre="vista a la piscina",Ocupantes=3, MetrosCuadrados=50},
            new VillaDtos{ID =2, Nombre="vista a la playa",Ocupantes=3, MetrosCuadrados=80}
            
        };
    }
}
