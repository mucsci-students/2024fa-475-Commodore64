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
        public bool isEquippedWeapon = false;
        public bool isEquippedArmor = false;

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
                count--; // decrease number of items in that slot
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
        if (slots[index].isEquippedWeapon && slots[19].isEquippedWeapon)
        {
            UnequipWeapon();
            slots[index].isEquippedWeapon = false;
            slots[19].isEquippedWeapon = false;

        }
        if (slots[index].isEquippedArmor && slots[18].isEquippedArmor)
        {
            UnequipArmor();
            slots[index].isEquippedArmor = false;
            slots[18].isEquippedArmor = false;

        }
        slots[index].RemoveItem();
    }

    public void Equip(int index)
    {
        if (slots[index].type == ItemType.WEAPON)
        {
            slots[19].icon = slots[index].icon;
            slots[19].type = slots[index].type;
            slots[index].isEquippedWeapon = true;
            slots[19].isEquippedWeapon = true;
        }
        if (slots[index].type == ItemType.ARMOR)
        {
            slots[18].icon = slots[index].icon;
            slots[18].type = slots[index].type;
            slots[index].isEquippedArmor = true;
            slots[18].isEquippedArmor = true;
        }
    }

    public void UnequipWeapon()
    {
        slots[19].icon = null;
        slots[19].type = ItemType.NONE;
    }
    public void UnequipArmor()
    {
        slots[18].icon = null;
        slots[18].type = ItemType.NONE;
    }
}