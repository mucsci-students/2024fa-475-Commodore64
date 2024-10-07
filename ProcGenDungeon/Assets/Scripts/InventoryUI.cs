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
        inventoryUI.SetActive(false); // Click F to bring up inventory (can change)
        equipmentUI.SetActive(false);
    }

    // Check to see if we should open/close the inventory
    void Update()
    {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
            equipmentUI.SetActive(!equipmentUI.activeSelf);
            Setup();
        }
    }

    void Setup()
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
}