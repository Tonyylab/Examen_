using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Material,
    Equipable,
    Consumible
}

public enum ConsumableType
{
    Hambre,
    Sed,
    Vida
}
[CreateAssetMenu(fileName = "Item", menuName = "Data/Item", order = 1)]
public class ItemData : ScriptableObject
{
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;

    public GameObject dropPrefab;
    public GameObject equipPrefab;

    public bool Stackeable;
    public int maxStack;
    
    public int weaponDamage;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

}

[System.Serializable]
public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}
