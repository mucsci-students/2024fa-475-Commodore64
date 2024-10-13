using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippableItem : Item
{
    public int damage;
    public int armor;
    public EquipmentType equipmentType;
}

public enum EquipmentType { ARMOR, SWORD, HAMMER }