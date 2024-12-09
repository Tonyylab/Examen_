using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftingRecipe", menuName = "Data/CraftingRecipe", order = 2)]
public class CraftingRecipe : ScriptableObject

{
    public ItemData itemToCraft;
    public ResourceCost[] cost;

}

[System.Serializable]
public class ResourceCost
{
    public ItemData item;
    public int quantity;
}
