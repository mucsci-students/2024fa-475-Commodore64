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
    public GameObject[] swords;
    public GameObject[] hammers;
    public GameObject[] armors;
    public GameObject[] player;
    public GameObject[] healthPotions;
    public GameObject[] energyPotions;
    public GameObject[] healthEnergyPotions;
    public GameObject[] keys;

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
                case ItemType.SWORD:
                    slot.maxAllowed = 1;
                    break;
                case ItemType.HAMMER:
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
        if (slots[index].isEquippedWeapon && slots[18].isEquippedWeapon)
        {
            UnequipWeapon();
            slots[index].isEquippedWeapon = false;
            slots[18].isEquippedWeapon = false;

        }
        if (slots[index].isEquippedArmor && slots[19].isEquippedArmor)
        {
            UnequipArmor();
            slots[index].isEquippedArmor = false;
            slots[19].isEquippedArmor = false;

        }
        slots[index].RemoveItem();
    }

    public void Equip(int index)
    {
        swords = GameObject.FindGameObjectsWithTag("Sword");
        hammers = GameObject.FindGameObjectsWithTag("Hammer");
        armors = GameObject.FindGameObjectsWithTag("Armor");
        player = GameObject.FindGameObjectsWithTag("Player");
        int damage = 0;
        int armor = 0;
        if (slots[index].type == ItemType.SWORD)
        {
            if (swords.Length > 0)
            {
                foreach (var sword in swords)
                {
                    damage = sword.GetComponent<EquipableItem>().damage;
                }
            }

            slots[18].icon = slots[index].icon;
            slots[18].type = slots[index].type;
            slots[20].icon = slots[18].icon;
            slots[20].type = slots[18].type;
            slots[index].isEquippedWeapon = true;
            slots[18].isEquippedWeapon = true;
            player[0].GetComponent<Player>().damage = damage;
        }

        if (slots[index].type == ItemType.HAMMER)
        {
            if (hammers.Length > 0)
            {
                foreach (var hammer in hammers)
                {
                    damage = hammer.GetComponent<EquipableItem>().damage;
                }
            }

            slots[18].icon = slots[index].icon;
            slots[18].type = slots[index].type;
            slots[20].icon = slots[18].icon;
            slots[20].type = slots[18].type;
            slots[index].isEquippedWeapon = true;
            slots[18].isEquippedWeapon = true;
            player[0].GetComponent<Player>().damage = damage;
        }

        if (slots[index].type == ItemType.ARMOR)
        {
            if (armors.Length > 0)
            {
                foreach (var armorItem in armors)
                {
                    armor = armorItem.GetComponent<EquipableItem>().armor;
                }
            }
            slots[19].icon = slots[index].icon;
            slots[19].type = slots[index].type;
            slots[index].isEquippedArmor = true;
            slots[19].isEquippedArmor = true;
            player[0].GetComponent<Player>().armor = armor;
        }
    }

    public void UnequipWeapon()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        player[0].GetComponent<Player>().damage = 20;
        slots[18].icon = null;
        slots[18].type = ItemType.NONE;
        slots[20].icon = null;
        slots[20].type = ItemType.NONE;
    }
    public void UnequipArmor()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        player[0].GetComponent<Player>().armor = 10;
        slots[19].icon = null;
        slots[19].type = ItemType.NONE;
    }

    public void UseConsumable(int index)
    {
        healthPotions = GameObject.FindGameObjectsWithTag("HealthPotion");
        energyPotions = GameObject.FindGameObjectsWithTag("EnergyPotion");
        healthEnergyPotions = GameObject.FindGameObjectsWithTag("HealthEnergy");
        int healthGain = 10;
        int energyGain = 0;

        if (slots[index].type == ItemType.CONSUMABLE && player[0].GetComponent<Player>().currentHealth != player[0].GetComponent<Player>().maxHealth)
        {
            if (healthPotions.Length > 0)
            {
                foreach (var healthPotion in healthPotions)
                {
                    healthGain = healthPotion.GetComponent<ConsumableItem>().healthGain;
                }
            }
            player[0].GetComponent<Player>().currentHealth = System.Math.Min(player[0].GetComponent<Player>().currentHealth + healthGain, player[0].GetComponent<Player>().maxHealth);
            player[0].GetComponent<Player>().healthBar.SetHealth(player[0].GetComponent<Player>().currentHealth);
            Remove(index);
        }

        if (slots[index].type == ItemType.CONSUMABLE && player[0].GetComponent<Player>().currentEnergy != player[0].GetComponent<Player>().maxEnergy)
        {
            if (energyPotions.Length > 0)
            {
                foreach (var energyPotion in energyPotions)
                {
                    energyGain = energyPotion.GetComponent<ConsumableItem>().energyGain;
                }
            }
            player[0].GetComponent<Player>().currentEnergy = System.Math.Min(player[0].GetComponent<Player>().currentEnergy + energyGain, player[0].GetComponent<Player>().maxEnergy);
            player[0].GetComponent<Player>().energyBar.SetEnergy(player[0].GetComponent<Player>().currentEnergy);
            Remove(index);
        }

        if (slots[index].type == ItemType.CONSUMABLE && player[0].GetComponent<Player>().currentHealth != player[0].GetComponent<Player>().maxHealth || player[0].GetComponent<Player>().currentEnergy != player[0].GetComponent<Player>().maxEnergy)
        {
            if (healthEnergyPotions.Length > 0)
            {
                foreach (var healthEnergyPotion in healthEnergyPotions)
                {
                    healthGain = healthEnergyPotion.GetComponent<ConsumableItem>().healthGain;
                    energyGain = healthEnergyPotion.GetComponent<ConsumableItem>().energyGain;
                }
            }
            player[0].GetComponent<Player>().currentHealth = System.Math.Min(player[0].GetComponent<Player>().currentHealth + healthGain, player[0].GetComponent<Player>().maxHealth);
            player[0].GetComponent<Player>().currentEnergy = System.Math.Min(player[0].GetComponent<Player>().currentEnergy + energyGain, player[0].GetComponent<Player>().maxEnergy);
            player[0].GetComponent<Player>().healthBar.SetHealth(player[0].GetComponent<Player>().currentHealth);
            player[0].GetComponent<Player>().energyBar.SetEnergy(player[0].GetComponent<Player>().currentEnergy);
            Remove(index);
        }
    }
}