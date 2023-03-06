namespace StarterAssets.Plants
{
    public class Harvest
    {
        public PlantType PlantType { get; set; }
        public int HarvestedFruits { get; set; }
        public int HarvestedSeeds { get; set; }

        public Harvest(PlantType plantType)
        {
            this.PlantType = plantType;
        }
    }
}