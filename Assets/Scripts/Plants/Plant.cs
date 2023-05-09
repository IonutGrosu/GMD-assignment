using UnityEngine;
using UnityEngine.Serialization;

namespace StarterAssets.Plants
{
    [CreateAssetMenu(menuName = "Scriptable object/Plant")]
    public class Plant : ScriptableObject
    {
        [FormerlySerializedAs("PlantType")] public PlantType plantType;
        [FormerlySerializedAs("MinHarvestableFruits")] public int minHarvestableFruits;
        [FormerlySerializedAs("MaxHarvestableFruits")] public int maxHarvestableFruits;
        [FormerlySerializedAs("MinHarvestableSeeds")] public int minHarvestableSeeds;
        [FormerlySerializedAs("MaxHarvestableSeeds")] public int maxHarvestableSeeds;
    }
    
    public enum PlantType
    {
        Tomato,
        Eggplant
    }
}