using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/* This object manages the inventory UI. */

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public Player player;
    public List<SlotUI> slots = new List<SlotUI>();

    void Start()
    {
        inventoryUI.SetActive(false); // Click F to bring up inventory
        equipmentUI.SetActive(false); // and equipment menus
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory")) // When F is pressed
        {
            // if the game is not paused, activate menus
            if (Time.timeScale != 0)
            {
                inventoryUI.SetActive(!inventoryUI.activeSelf);
                equipmentUI.SetActive(!equipmentUI.activeSelf);
            }
        }
        Refresh();
    }

    // Updates the Slots UI in real time, displaying the items
    void Refresh()
    {
        // if the amount of slots matches the player's inventory size
        if (slots.Count == player.inventory.slots.Count)
        {
            // for each slot
            for (int i = 0; i < slots.Count; i++)
            {
                // check if the slot is not empty
                if (player.inventory.slots[i].type != ItemType.NONE)
                {
                    slots[i].SetItem(player.inventory.slots[i]); // set item's data in the slot
                }
                else
                {
                    slots[i].SetEmpty(); // keep other empty slots empty to be able to add to
                }
            }
        }
    }

    public void Remove(int slotIndex)
    {
        player.inventory.Remove(slotIndex);
        Refresh();
    }
    public void Equip(int slotIndex)
    {
        player.inventory.Equip(slotIndex);
        Refresh();
    }
    public void UnequipWeapon(int slotIndex)
    {
        player.inventory.UnequipWeapon();
        Refresh();
    }
    public void UnequipArmor(int slotIndex)
    {
        player.inventory.UnequipArmor();
        Refresh();
    }
    public void UseConsumable(int slotIndex)
    {
        player.inventory.UseConsumable(slotIndex);
        Refresh();
    }
}