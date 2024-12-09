using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData data;

    public string ItemName;
    public Sprite ItemSprite;
    public ItemType ItemType;
    public string ItemDescription;

    public int stackAmount;

    public float hungerRestore;
    public float thirstRestore;
    public float HPRestore;
    public float damage;

    private void Awake()
    {
        ItemName = data.displayName;
        ItemSprite = data.icon;
        ItemType = data.type;
        ItemDescription = data.description;
    }
}
