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
    }

    public enum ItemType
    {
        Tool
    }

    public enum ActionType
    {
        Dig
    }
}