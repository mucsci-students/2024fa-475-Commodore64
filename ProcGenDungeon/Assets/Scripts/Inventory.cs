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
        public ItemType type; // type of item
        public int count; // number of items in the slot
        public int maxAllowed; // max items per slot
        public Sprite icon; // sprite image

        public InventorySlot()
        {
            // empty slot
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
            this.type = item.type; // slot type is item's type
            this.icon = item.icon; // slot image is item's image
            count++;               // increase number of items in that slot
            item.enabled = true;
        }

        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;
                if (count == 0)
                {
                    // empty slot and change to NONE type
                    icon = null;
                    type = ItemType.NONE;
                }
            }
        }
    }

    public List<InventorySlot> slots = new List<InventorySlot>(); // list of slots
    public Inventory(int numSlots)
    {
        // make a number of slots and add them to the slot list
        for (int i = 0; i < numSlots; i++)
        {
            InventorySlot slot = new InventorySlot();
            slots.Add(slot);
        }
    }

    public void Add(Item newItem)
    {
        foreach (InventorySlot slot in slots)
        {
            // If something matches current inventory and the slot is not full of that item
            if (slot.type == newItem.type && slot.CanAddItem() && slot.icon == newItem.icon)
            {
                // Add the item to the existing slot
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
                // Add the first item to the slot
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