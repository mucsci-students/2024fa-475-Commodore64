using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EquipmentUI : MonoBehaviour
{
    public Image itemIcon;
    public Button removeButton;

    // Putting Item in corresponding slot
    public void SetItem(Equipment.EquipmentSlot slot)
    {
        if (slot != null)
        {
            itemIcon.sprite = slot.icon;
            itemIcon.color = new Color(1, 1, 1, 1);
            itemIcon.enabled = true;
            removeButton.interactable = true;
        }
    }

    // Empty Slots setting
    public void SetEmpty()
    {
        itemIcon.enabled = false;
        itemIcon.sprite = null;
        itemIcon.color = new Color(1, 1, 1, 0);
        removeButton.interactable = false;
    }
}