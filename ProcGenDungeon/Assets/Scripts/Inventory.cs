using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one inventory instance");
            return;
        }
        instance = this;
    }
    public List<Item> items = new List<Item>();
    public void Add(Item item)
    {
        if (!item.isDefaultItem)
        {
            items.Add(item);
        }
    }

    public void Remove(Item item)
    {
        items.Remove(item);
    }
}
