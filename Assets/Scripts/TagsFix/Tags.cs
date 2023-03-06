using System;
using System.Collections.Generic;
using UnityEngine;

public class Tags : MonoBehaviour
{
    public List<Tag> _tags;
    public List<Tag> All => _tags;

    public bool HasTag(Tag t)
    {
        return _tags.Contains(t);
    }

    public bool HasTag(string tagName)
    {
        return _tags.Exists(t => t.Name.Equals(tagName, StringComparison.InvariantCultureIgnoreCase));
    }

    public void AddTag(Tag t)
    {
        _tags.Add(t);
    }
}
