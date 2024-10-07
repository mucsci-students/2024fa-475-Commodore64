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

    public void SetItem(Inventory.InventorySlot slot)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            itemIcon.enabled = true;
            removeButton.interactable = true;
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

    public void SetEmpty()
    {
        itemIcon.enabled = false;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        quantityText.text = "";
    }
}
