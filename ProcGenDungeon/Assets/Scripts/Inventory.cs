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

        public void ClearSlot()
        {
            // item = null;
            // icon.sprite = null;
            // icon.enabled = false;
            // removeButton.interactable = false;
        }

        public void OnRemoveButton()
        {
            //Inventory.instance.Remove(item);
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
        foreach (InventorySlot slot in slots)
        {
            if (slot.type == newItem.type && slot.CanAddItem())
            {
                slot.AddItem(newItem);
                return;
            }
        }

        foreach (InventorySlot slot in slots)
        {
            if (slot.type == ItemType.NONE)
            {
                slot.AddItem(newItem);
                return;
            }
        }
    }

}