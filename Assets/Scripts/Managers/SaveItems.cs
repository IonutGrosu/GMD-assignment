using System;

namespace StarterAssets.Managers
{
    [Serializable]
    public class SaveItems
    {
        public Item[] Items { get; set; }
        public int Coins { get; set; }
        public int[] Counts { get; set; }

        public SaveItems(Item[] items, int coins, int[] counts)
        {
            Items = items;
            Coins = coins;
            Counts = counts;
        }
    }
}