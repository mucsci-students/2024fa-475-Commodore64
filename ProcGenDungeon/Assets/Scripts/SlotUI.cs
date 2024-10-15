using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SlotUI : MonoBehaviour
{
    public Image itemIcon;
    public TextMeshProUGUI quantityText;
    public Button removeButton;

    // Putting Item in corresponding slot
    public void SetItem(Inventory.InventorySlot slot)
    {
        // Make sure that slot has an element
        if (slot != null)
        {
            // enable that icon, and make the remove button interactable
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            itemIcon.enabled = true;
            removeButton.interactable = true;

            // set counter in the corner of the item box
            if (slot.count == 0)
            {
                quantityText.text = "";
            }
            else
            {
                quantityText.text = slot.count.ToString();
            }
        }
    }

    // Empty Slots setting
    public void SetEmpty()
    {
        // disable that icon, and make the remove button not interactable
        itemIcon.enabled = false;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = ""; // empty counter in the corner of the item box
        removeButton.interactable = false;
    }
}
