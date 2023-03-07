using StarterAssets.Plants;
using UnityEngine;

namespace StarterAssets.Utils
{
    public class HarvestUtil: MonoBehaviour
    {
        public static HarvestUtil instance;

        [SerializeField] private Item tomatoSeed;
        [SerializeField] private Item tomatoFruit;
        [SerializeField] private Item eggplantSeed;
        [SerializeField] private Item eggplantFruit;

        private void Start()
        {
            instance = this;
        }

        public void AddHarvestToInventory(Harvest harvest)
        {
            switch (harvest.PlantType)
            {
                case PlantType.Tomato:
                {
                    for (int i = 0; i < harvest.HarvestedSeeds; i++)
                    {
                        InventoryManager.instance.AddItem(tomatoSeed);
                    }

                    for (int i = 0; i < harvest.HarvestedFruits; i++)
                    {
                        InventoryManager.instance.AddItem(tomatoFruit);
                    }
                    break;
                }
                case PlantType.Eggplant:
                {
                    for (int i = 0; i < harvest.HarvestedSeeds; i++)
                    {
                        InventoryManager.instance.AddItem(eggplantSeed);
                    }

                    for (int i = 0; i < harvest.HarvestedFruits; i++)
                    {
                        InventoryManager.instance.AddItem(eggplantFruit);
                    }
                    break;
                }
            }
        }
    }
}