using Web_Api.Models.DTO;

namespace Web_Api.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
            {
                new VillaDTO{Id=1,Name="Pool View",Occupancy=400,Sqft=5000},
                new VillaDTO{Id=2,Name="Beach View",Occupancy=300,Sqft=3900}
            };
    }
}
