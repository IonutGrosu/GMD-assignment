using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Tags : MonoBehaviour
{
    [FormerlySerializedAs("_tags")] public List<Tag> tags;
    public List<Tag> All => tags;
    public bool HasTag(string tagName)
    {
        return tags.Exists(t => t.Name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase));
    }

    public void AddTag(Tag t)
    {
        tags.Add(t);
    }
}
