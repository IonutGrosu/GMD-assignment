using UnityEngine;

namespace StarterAssets
{
    [CreateAssetMenu(menuName = "Scriptable object/Item")]
    public class Item : ScriptableObject
    {
        public Sprite image;
        public ItemType type;
        public ActionType actionType;
        public bool stackable;
        public int sellPrice;
        public bool sellable;
    }

    public enum ItemType
    {
        Tool,
        Seed
    }

    public enum ActionType
    {
        Dig,
        Plant,
        CutTree
    }
}