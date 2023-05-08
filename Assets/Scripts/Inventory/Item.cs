using System;
using UnityEngine;

namespace StarterAssets
{
    [Serializable]
    [CreateAssetMenu(menuName = "Scriptable object/Item")]
    public class Item : ScriptableObject
    {
        public Sprite image;
        public ItemType type;
        public bool stackable;
        public int sellPrice;
        public bool sellable;
    }

    public enum ItemType
    {
        Tool,
        Seed,
        Fruit
    }

    public enum ActionType
    {
        Dig,
        Plant,
        CutTree
    }
}