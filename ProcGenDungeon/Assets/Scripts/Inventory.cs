using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Inventory
{
    [System.Serializable]
    public class InventorySlot
    {
        public ItemType type;
        public int count;
        public int maxAllowed;
        public Sprite icon;

        public InventorySlot()
        {
            type = ItemType.NONE;
            count = 0;
            maxAllowed = 99;
        }

        // Checks if inventory slot is full
        public bool CanAddItem()
        {
            if (count < maxAllowed)
            {
                return true;
            }

            return false;

        }

        public void AddItem(Item item)
        {
            this.type = item.type;
            this.icon = item.icon;
            count++;
            item.enabled = true;
        }

        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;
                if (count == 0)
                {
                    icon = null;
                    type = ItemType.NONE;
                }
            }
        }
    }

    public List<InventorySlot> slots = new List<InventorySlot>();
    public Inventory(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            InventorySlot slot = new InventorySlot();
            slots.Add(slot);
        }
    }

    public void Add(Item newItem)
    {
        // If something matches current inventory
        foreach (InventorySlot slot in slots)
        {
            if (slot.type == newItem.type && slot.CanAddItem() && slot.icon == newItem.icon)
            {
                slot.AddItem(newItem);
                return;
            }
        }

        // Else add item to new slot and set max values by type
        foreach (InventorySlot slot in slots)
        {
            switch (newItem.type)
            {
                case ItemType.WEAPON:
                    slot.maxAllowed = 1;
                    break;
                case ItemType.ARMOR:
                    slot.maxAllowed = 1;
                    break;
                case ItemType.COLLECTABLE:
                    slot.maxAllowed = 10;
                    break;
                case ItemType.CONSUMABLE:
                    slot.maxAllowed = 10;
                    break;
                default:
                    break;
            }
            if (slot.type == ItemType.NONE && slot.icon == null)
            {
                slot.AddItem(newItem);
                return;
            }
        }
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }
}