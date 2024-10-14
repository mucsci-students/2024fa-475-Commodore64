using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Equipment
{
    [System.Serializable]
    public class EquipmentSlot
    {
        public EquipmentType type;
        public int count;
        public Sprite icon;

        public EquipmentSlot()
        {
            type = EquipmentType.NONE;
            count = 0;
        }

        // Checks if Equipment slot is full
        public bool CanAddItem()
        {
            if (count == 0)
            {
                return true;
            }

            return false;

        }

        public void AddItem(EquipableItem item)
        {
            this.type = item.equipmentType;
            this.icon = item.icon;
            count++;
            item.enabled = true;
        }

        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;
                icon = null;
                type = EquipmentType.NONE;
            }
        }
    }

    public List<EquipmentSlot> slots = new List<EquipmentSlot>();
    public Equipment(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            EquipmentSlot slot = new EquipmentSlot();
            slots.Add(slot);
        }
    }

    public void Add(EquipableItem newItem)
    {
        // If something matches current Equipment
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.type == newItem.equipmentType && slot.CanAddItem() && slot.icon == newItem.icon)
            {
                slot.AddItem(newItem);
                return;
            }
        }

        // Else add item to new slot and set max values by type
        foreach (EquipmentSlot slot in slots)
        {
            if (slot.type == EquipmentType.NONE && slot.icon == null)
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