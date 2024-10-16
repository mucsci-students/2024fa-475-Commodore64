using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
        public TextMeshProUGUI damage;
        public TextMeshProUGUI armor;
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
    public GameObject[] swords; // list of swords in scene
    public GameObject[] hammers; // list of hammers in scene
    public GameObject[] armors; // list of armors in scene
    public GameObject[] player; // list of player(s) in scene
    public GameObject[] healthPotions; // list of healthPotions in scene
    public GameObject[] energyPotions; // list of energyPotions in scene
    public GameObject[] healthEnergyPotions; // list of healthEnergyPotions in scene
    public GameObject[] keys; // list of keys in scene

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
                case ItemType.HEALTH:
                    slot.maxAllowed = 10;
                    break;
                case ItemType.ENERGY:
                    slot.maxAllowed = 10;
                    break;
                case ItemType.HEALTHENERGY:
                    slot.maxAllowed = 10;
                    break;
                default:
                    break;
            }
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
            // check item's type to determine maxAllowed
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
                case ItemType.HEALTH:
                    slot.maxAllowed = 10;
                    break;
                case ItemType.ENERGY:
                    slot.maxAllowed = 10;
                    break;
                case ItemType.HEALTHENERGY:
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
        // if weapon is equipped when remove is called, unequip weapon
        if (slots[index].isEquippedWeapon && slots[18].isEquippedWeapon)
        {
            UnequipWeapon();
            slots[index].isEquippedWeapon = false;
            slots[18].isEquippedWeapon = false;

        }

        // if armor is equipped when remove is called, unequip armor
        if (slots[index].isEquippedArmor && slots[19].isEquippedArmor)
        {
            UnequipArmor();
            slots[index].isEquippedArmor = false;
            slots[19].isEquippedArmor = false;

        }

        // remove the item
        slots[index].RemoveItem();
    }

    public void Equip(int index)
    {
        // find all equipable items
        swords = GameObject.FindGameObjectsWithTag("Sword");
        hammers = GameObject.FindGameObjectsWithTag("Hammer");
        armors = GameObject.FindGameObjectsWithTag("Armor");
        player = GameObject.FindGameObjectsWithTag("Player");
        int damage = 0;
        int armor = 0;

        // if it is a sword
        if (slots[index].type == ItemType.SWORD)
        {
            // check items in scene to determine if it exists
            if (swords.Length > 0)
            {
                foreach (var sword in swords)
                {
                    //get weapon's damage
                    damage = sword.GetComponent<EquipableItem>().damage;
                }
            }

            // set the slot's data and the equip slot's data
            slots[18].icon = slots[index].icon;
            slots[18].type = slots[index].type;
            slots[20].icon = slots[18].icon;
            slots[20].type = slots[18].type;
            slots[index].isEquippedWeapon = true;
            slots[18].isEquippedWeapon = true;
            player[0].GetComponent<Player>().damage = damage; // apply damage
            slots[18].damage.text = "Damage: " + damage + " Dmg"; // change visual
        }

        // if it is a hammer
        if (slots[index].type == ItemType.HAMMER)
        {
            // check items in scene to determine if it exists
            if (hammers.Length > 0)
            {
                foreach (var hammer in hammers)
                {
                    //get weapon's damage
                    damage = hammer.GetComponent<EquipableItem>().damage;
                }
            }

            // set the slot's data and the equip slot's data
            slots[18].icon = slots[index].icon;
            slots[18].type = slots[index].type;
            slots[20].icon = slots[18].icon;
            slots[20].type = slots[18].type;
            slots[index].isEquippedWeapon = true;
            slots[18].isEquippedWeapon = true;
            player[0].GetComponent<Player>().damage = damage; // apply damage
            slots[18].damage.text = "Damage: " + damage + " Dmg"; // change visual
        }

        // if it is armor
        if (slots[index].type == ItemType.ARMOR)
        {
            // check items in scene to determine if it exists
            if (armors.Length > 0)
            {
                foreach (var armorItem in armors)
                {
                    // get armor's armor value
                    armor = armorItem.GetComponent<EquipableItem>().armor;
                }
            }

            // set the slot's data
            slots[19].icon = slots[index].icon;
            slots[19].type = slots[index].type;
            slots[index].isEquippedArmor = true;
            slots[19].isEquippedArmor = true;
            player[0].GetComponent<Player>().armor = armor; // apply armor
            slots[19].armor.text = "Armor: " + armor + " Def";
        }
    }

    public void UnequipWeapon()
    {
        // empty slots for the weapon and reduce damage to base
        player = GameObject.FindGameObjectsWithTag("Player");
        player[0].GetComponent<Player>().damage = 20;
        slots[18].icon = null;
        slots[18].type = ItemType.NONE;
        slots[20].icon = null;
        slots[20].type = ItemType.NONE;
        slots[18].damage.text = "Damage: 20 Dmg";
    }
    public void UnequipArmor()
    {
        // empty slot for the armor and reduce armor to base
        player = GameObject.FindGameObjectsWithTag("Player");
        player[0].GetComponent<Player>().armor = 10;
        slots[19].icon = null;
        slots[19].type = ItemType.NONE;
        slots[19].armor.text = "Armor: 10 Def";
    }

    public void UseConsumable(int index)
    {
        // find all consumables in scene
        healthPotions = GameObject.FindGameObjectsWithTag("HealthPotion");
        energyPotions = GameObject.FindGameObjectsWithTag("EnergyPotion");
        healthEnergyPotions = GameObject.FindGameObjectsWithTag("HealthEnergy");
        int healthGain = 10;
        int energyGain = 0;

        // gather icons for the corresponding potions
        Sprite healthIcon;
        healthIcon = healthPotions[0].GetComponent<ConsumableItem>().icon;
        Sprite energyIcon;
        energyIcon = energyPotions[0].GetComponent<ConsumableItem>().icon;
        Sprite healthEnergyIcon;
        healthEnergyIcon = healthEnergyPotions[0].GetComponent<ConsumableItem>().icon;

        // check for health potion and if it can be used
        if (slots[index].type == ItemType.HEALTH && player[0].GetComponent<Player>().currentHealth != player[0].GetComponent<Player>().maxHealth && slots[index].icon == healthIcon)
        {
            if (healthPotions.Length > 0)
            {
                foreach (var healthPotion in healthPotions)
                {
                    healthGain = healthPotion.GetComponent<ConsumableItem>().healthGain;
                }
            }

            // add health to the player
            player[0].GetComponent<Player>().currentHealth = System.Math.Min(player[0].GetComponent<Player>().currentHealth + healthGain, player[0].GetComponent<Player>().maxHealth);
            player[0].GetComponent<Player>().healthBar.SetHealth(player[0].GetComponent<Player>().currentHealth);
            Remove(index);
        }

        // check for energy potion and if it can be used
        if (slots[index].type == ItemType.ENERGY && player[0].GetComponent<Player>().currentEnergy != player[0].GetComponent<Player>().maxEnergy && slots[index].icon == energyIcon)
        {
            if (energyPotions.Length > 0)
            {
                foreach (var energyPotion in energyPotions)
                {
                    energyGain = energyPotion.GetComponent<ConsumableItem>().energyGain;
                }
            }

            // add energy to the player
            player[0].GetComponent<Player>().currentEnergy = System.Math.Min(player[0].GetComponent<Player>().currentEnergy + energyGain, player[0].GetComponent<Player>().maxEnergy);
            player[0].GetComponent<Player>().energyBar.SetEnergy(player[0].GetComponent<Player>().currentEnergy);
            Remove(index);
        }

        // check for healthEnergy potion and if it can be used
        if (slots[index].type == ItemType.HEALTHENERGY && player[0].GetComponent<Player>().currentHealth != player[0].GetComponent<Player>().maxHealth || player[0].GetComponent<Player>().currentEnergy != player[0].GetComponent<Player>().maxEnergy && slots[index].icon == healthEnergyIcon)
        {
            if (healthEnergyPotions.Length > 0)
            {
                foreach (var healthEnergyPotion in healthEnergyPotions)
                {
                    healthGain = healthEnergyPotion.GetComponent<ConsumableItem>().healthGain;
                    energyGain = healthEnergyPotion.GetComponent<ConsumableItem>().energyGain;
                }
            }

            // add health and energy to the player
            player[0].GetComponent<Player>().currentHealth = System.Math.Min(player[0].GetComponent<Player>().currentHealth + healthGain, player[0].GetComponent<Player>().maxHealth);
            player[0].GetComponent<Player>().currentEnergy = System.Math.Min(player[0].GetComponent<Player>().currentEnergy + energyGain, player[0].GetComponent<Player>().maxEnergy);
            player[0].GetComponent<Player>().healthBar.SetHealth(player[0].GetComponent<Player>().currentHealth);
            player[0].GetComponent<Player>().energyBar.SetEnergy(player[0].GetComponent<Player>().currentEnergy);
            Remove(index);
        }
    }
}