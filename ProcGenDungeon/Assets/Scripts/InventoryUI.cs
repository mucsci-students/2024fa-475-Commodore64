using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

/* This object manages the inventory UI. */

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    public GameObject equipmentUI;
    public Player player;
    public Button removeButton;
    public List<SlotUI> slots = new List<SlotUI>();

    void Start()
    {
        inventoryUI.SetActive(false); // Click F to bring up inventory
        equipmentUI.SetActive(false);
    }

    void Update()
    {
        if (Input.GetButtonDown("Inventory")) // When F is pressed
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
        Refresh();
    }

    // Updates the Slots UI in real time
    void Refresh()
    {
        if (slots.Count == player.inventory.slots.Count)
        {
            for (int i = 0; i < slots.Count; i++)
            {
                if (player.inventory.slots[i].type != ItemType.NONE)
                {
                    slots[i].SetItem(player.inventory.slots[i]);
                }
                else
                {
                    slots[i].SetEmpty();
                }
            }
        }
    }

    public void Remove(int slotIndex)
    {
        player.inventory.Remove(slotIndex);
        Refresh();
    }
}