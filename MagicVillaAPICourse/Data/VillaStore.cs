using MagicVillaAPICourse.Models.Dto;

namespace MagicVillaAPICourse.Data
{
    public class VillaStore
    {
        public static List<VillaDto> villalist = new List<VillaDto>
            {
                new VillaDto { Id = 1, Name="Pool View", Sqft=100, Occupancy=4},
                new VillaDto { Id = 2, Name="Beach View", Sqft=300, Occupancy=3}
            };
    }
}
