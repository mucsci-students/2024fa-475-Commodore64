using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipableItem : Item
{
    public int damage;
    public int armor;
    public EquipmentType equipmentType;
}

public enum EquipmentType { NONE, ARMOR, SWORD, HAMMER }