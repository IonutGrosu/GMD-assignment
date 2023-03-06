using UnityEngine;

namespace StarterAssets.Plants
{
    [CreateAssetMenu(menuName = "Scriptable object/Plant")]
    public class Plant : ScriptableObject
    {
        public PlantType PlantType;
        public int MinHarvestableFruits;
        public int MaxHarvestableFruits;
        public int MinHarvestableSeeds;
        public int MaxHarvestableSeeds;
    }
    
    public enum PlantType
    {
        Tomato,
        Eggplant
    }
}