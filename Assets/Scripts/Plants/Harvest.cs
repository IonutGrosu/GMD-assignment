namespace StarterAssets.Plants
{
    public class Harvest
    {
        public PlantType PlantType { get; }
        public int HarvestedFruits { get; set; }
        public int HarvestedSeeds { get; set; }

        public Harvest(PlantType plantType)
        {
            PlantType = plantType;
        }
    }
}