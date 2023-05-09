using System;
using StarterAssets.Plants;
using UnityEngine;

namespace StarterAssets.Utils
{
    public class HarvestUtil: MonoBehaviour
    {
        public static HarvestUtil Instance;

        [SerializeField] private Item tomatoSeed;
        [SerializeField] private Item tomatoFruit;
        [SerializeField] private Item eggplantSeed;
        [SerializeField] private Item eggplantFruit;

        private void Start()
        {
            Instance = this;
        }

        public void AddHarvestToInventory(Harvest harvest)
        {
            switch (harvest.PlantType)
            {
                case PlantType.Tomato:
                {
                    for (var i = 0; i < harvest.HarvestedSeeds; i++)
                    {
                        InventoryManager.Instance.AddItem(tomatoSeed);
                    }

                    for (int i = 0; i < harvest.HarvestedFruits; i++)
                    {
                        InventoryManager.Instance.AddItem(tomatoFruit);
                    }
                    break;
                }
                case PlantType.Eggplant:
                {
                    for (var i = 0; i < harvest.HarvestedSeeds; i++)
                    {
                        InventoryManager.Instance.AddItem(eggplantSeed);
                    }

                    for (int i = 0; i < harvest.HarvestedFruits; i++)
                    {
                        InventoryManager.Instance.AddItem(eggplantFruit);
                    }
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}